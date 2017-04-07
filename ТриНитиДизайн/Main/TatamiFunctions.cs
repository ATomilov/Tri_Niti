using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Path = System.Windows.Shapes.Path;

namespace ТриНитиДизайн
{
    public partial class MainWindow : Window
    {
        int oldHits = 2;

        public void FindControlLine(Figure StartLines, Figure ConLine, Canvas CurCanvas)
        {
            Point a = ConLine.Points[0];
            Point b = ConLine.Points[1];

            if(!(a.X == b.X && a.Y == b.Y))
            {
                CurCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
            }

            bool success;
            success = CheckForIntersection(a, b, StartLines, ConLine, CurCanvas, true, false);
            if (success)
            {
                double x = b.X - a.X;
                double y = b.Y - a.Y;
                Vector line = new Vector(x, y);
                line.Normalize();
                line *= 1000;
                double x1 = a.X + line.X;
                double y1 = a.Y + line.Y;
                double x2 = a.X - line.X;
                double y2 = a.Y - line.Y;
                CheckForIntersection(new Point(x1, y1), new Point(x2, y2), StartLines, ConLine, CurCanvas, false, true);
            }
        }

        public bool CheckForIntersection(Point a, Point b, Figure StartLines, Figure ConLine, Canvas CurCanvas, bool firstCheck, bool secondCheck)              //проверка на пересечение задающих прямых и начальных отрезков
        {
            List<Point> pts = new List<Point>();

            double A1 = a.Y - b.Y,                                                  //коэффициенты A B C задающей прямой
                    B1 = b.X - a.X,
                    C1 = -A1 * a.X - B1 * a.Y;

            int hits = 0;                                                           //сколько раз была пересечена прямая
            bool success = false;
            for (int i = 0; i < StartLines.Points.Count-1; i++)                                 //перебираем все начальные отрезки
            {
                Point c, d;
                c = StartLines.Points[i];
                d = StartLines.Points[i + 1];

                double A2 = c.Y - d.Y,                              //коэффициенты A B C начального отрезка
                    B2 = d.X - c.X,
                    C2 = -A2 * c.X - B2 * c.Y;
                double zn = FindDeterminator(A1, B1, A2, B2);
                if (zn != 0)
                {
                    double x = -FindDeterminator(C1, B1, C2, B2) / zn;          //нахождение координат пересечения
                    double y = -FindDeterminator(A1, C1, A2, C2) / zn;
                    if (IsDotOnLine(a.X, b.X, x) && IsDotOnLine(a.Y, b.Y, y)            //находятся ли координаты пересечения на отрезках задающей прямой и начального отрезка
                        && IsDotOnLine(c.X, d.X, x) && IsDotOnLine(c.Y, d.Y, y))
                    {
                        pts.Add(new Point(x, y));                                   //если да, то добавляем точки пересечения в лист
                        hits++;

                    }
                }
            }
            if (hits > 0)                                                       //если пересечения для задающей прямой произошли, то рисуем их и сортируем по порядку
            {
                if (secondCheck)                                                 //отрисовка линии между первой и последней точкой пересечения, как в программе
                {
                    OrganizeDots(pts, ConLine, b, hits);
                    SetLine(ConLine.Points[2], ConLine.Points[ConLine.Points.Count - 1], "dash", CurCanvas);
                }
                success = true;
            }

            if (firstCheck)
            {
                if (success)
                {
                    return true;
                }
            }
            return false;
        }

        private void OrganizeDots(List<Point> pts, Figure ConLine, Point c, int hits)        //сортировка точек пересечения в отдельные листы фигур                    
        {
            double[] distance = new double[pts.Count];
            int[] numbers = new int[pts.Count];
            for (int i = 0; i < pts.Count; i++)
            {
                numbers[i] = i;
            }

            for (int i = 0; i < pts.Count; i++)                             //находим расстояния от прямой, перпендикулярной задающей прямой
            {
                distance[i] = FindLength(pts[i], c);
            }

            for (int i = 0; i < pts.Count - 1; i++)             //сортировка точек в массиве по порядку расстояния
            {
                int min = i;
                for (int j = i + 1; j < pts.Count; j++)
                {
                    if (distance[j] < distance[min])
                    {
                        min = j;
                    }
                }
                double dummy = distance[i];
                distance[i] = distance[min];
                distance[min] = dummy;

                int dummy1 = numbers[i];
                numbers[i] = numbers[min];
                numbers[min] = dummy1;
            }

            if (oldHits != hits)                //если на следующей задающей прямой лежит больше точек, чем на предыдущей, то следующие точки заносим в следующие листы фигур
            {
                /*
                for(int i = figureCount; i < (figureCount + (hits / 2) + 1);i++)            //неплохо бы динамически создавать листы фигур
                {
                    controlPointsList.Add(new List<Point>());
                    tatamiPoints.Add(new List<Point>());
                }
                 * */
                TatamiShapesCount += (oldHits / 2);
                oldHits = hits;
            }
            for (int i = 0; i < hits; i += 2)                              //добалвение точек в листы фигур
            {
                ConLine.Points.Add(pts[numbers[i]]);
                ConLine.Points.Add(pts[numbers[i + 1]]);
            }
        }


        private double FindLength(Point a, Point b)                  //ф-ла длины отрезка по координатам
        {
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
        }

        private double FindDeterminator(double a, double b, double c, double d)          //определитель для нахождения точки пересечения
        {
            return a * d - b * c;
        }

        private bool IsDotOnLine(double a, double b, double c)
        {
            return (Math.Min(a, b) <= c) && c <= (Math.Max(a, b));
        }

        public void SetLine(Point point1, Point point2, string type,Canvas CurCanvas)                //отрисовка линии, dash - через черту, red - красная, blue - синяя
        {
            Line shape = new Line();
            shape.Stroke = OptionColor.ColorDraw;
            shape.StrokeThickness = 1;
            if (type.Equals("dash"))
            {
                DoubleCollection dashes = new DoubleCollection();
                dashes.Add(2);
                dashes.Add(2);
                shape.StrokeDashArray = dashes;
            }
            if (type.Equals("red"))
            {
                shape.Stroke = System.Windows.Media.Brushes.Red;
            }
            if (type.Equals("blue"))
            {
                shape.StrokeThickness = 1;
                shape.Stroke = System.Windows.Media.Brushes.Blue;
            }
            shape.X1 = point1.X;
            shape.Y1 = point1.Y;
            shape.X2 = point2.X;
            shape.Y2 = point2.Y;
            CurCanvas.Children.Add(shape);
        }

    }
}
