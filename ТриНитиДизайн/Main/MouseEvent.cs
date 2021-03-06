﻿using System;
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

        private void CanvasTest_MouseRightButtonDown(object sender, MouseButtonEventArgs e)         //при нажатии на праую кнопку мыши
        {
            Mouse.Capture(MainCanvas);
            if (CurrentRegim == Regim.RegimTatami)
            {
                if(ControlLine.Points.Count > 2)
                {
                    MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
                }
                ControlLine.Points.Clear();
                ControlLine.Points.Add(e.GetPosition(MainCanvas));
            }
        }

        private void CanvasTest_MouseMove(object sender, MouseEventArgs e)
        {
            MousePositionX = e.GetPosition(MainCanvas).X;
            MousePositionY = e.GetPosition(MainCanvas).Y;
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (CurrentRegim == Regim.RegimLomanaya)
                {
                    if (ListFigure[IndexFigure].Points.Count > 0)
                    {
                        MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
                        Line line = ListFigure[IndexFigure].GetLine(ListFigure[IndexFigure].PointEnd, e.GetPosition(MainCanvas));
                        line.StrokeThickness = 1;
                        line.Stroke = OptionColor.ColorDraw;
                        MainCanvas.Children.Add(line);
                        MainCanvas.UpdateLayout();
                    }
                }
                if (CurrentRegim == Regim.RegimTatami)
                {
                    if (ControlLine.Points.Count > 1)
                    {
                        MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
                        ControlLine.Points.RemoveAt(ControlLine.Points.Count - 1);
                    }
                    Line line = ControlLine.GetLine(ControlLine.Points[0], e.GetPosition(MainCanvas));
                    DoubleCollection dashes = new DoubleCollection();
                    dashes.Add(2);
                    dashes.Add(2);
                    line.StrokeDashArray = dashes;
                    line.StrokeThickness = 1;
                    line.Stroke = OptionColor.ColorSelection;
                    MainCanvas.Children.Add(line);
                    MainCanvas.UpdateLayout();
                    ControlLine.Points.Add(e.GetPosition(MainCanvas));
                }
            }
        }

        private void CanvasTest_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            if (CurrentRegim == Regim.RegimLomanaya)
            {
                if (MainCanvas.Children.Count > 1)
                {
                    MainCanvas.Children.RemoveAt(MainCanvas.Children.Count - 1);
                }
                Point point = FindClosestDot(e.GetPosition(MainCanvas));
                ListFigure[IndexFigure].AddPoint(point);
            }

            if (CurrentRegim == Regim.RegimTatami)
            {
                if(ControlLine.Points.Count == 1)
                {
                    ControlLine.Points.Add(e.GetPosition(MainCanvas));
                }
                FindControlLine(ListFigure[IndexFigure], ControlLine, MainCanvas);
            }

        }


        void CanvasTest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)          //левая кнопка мыши
        {
            if (CurrentRegim == Regim.RegimLomanaya)
            {
                ListFigure.Add(new Figure(MainCanvas));
                IndexFigure++;
                for (int i = 0; i < ListFigure.Count; i++)
                {
                    if (i != IndexFigure)
                    {
                        foreach (Shape sh in ListFigure[i].Shapes)
                        {
                            sh.Stroke = OptionColor.ColorSelection;
                        }
                    }
                }
            }
            if(CurrentRegim == Regim.RegimStegki)
            {
                if (e.OriginalSource is Line || e.OriginalSource is Path)                      //выделение части татами
                {
                    double x;
                    double y;
                    if (e.OriginalSource is Line)                                       //если мы нажали на линию, то находим одну из точек линии
                    {
                        Line clickedLine = (Line)e.OriginalSource;
                        x = clickedLine.X1;
                        y = clickedLine.Y1;
                    }
                    else
                    {
                        Path clickedPath = (Path)e.OriginalSource;                      //если мы нажали на точку, то находим координаты
                        EllipseGeometry geom = (EllipseGeometry)clickedPath.Data;
                        x = geom.Center.X;
                        y = geom.Center.Y;
                    }
                    for (int i = 0; i < 128; i++)                           //находим номер фигуры, которую хотим выделить
                    {
                        for (int j = 0; j < TatamiFigures[i].Points.Count; j++)
                        {
                            if (TatamiFigures[i].Points[j].X == x && TatamiFigures[i].Points[j].Y == y)
                            {
                                DrawTatami(TatamiFigures, i, MainCanvas);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
