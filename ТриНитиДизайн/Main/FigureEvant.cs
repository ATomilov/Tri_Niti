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

        private void FigureMainButtonEvant(object sender, RoutedEventArgs e)
        {
            if (tabControl2.Visibility == Visibility.Visible)
                tabControl2.Visibility = Visibility.Hidden;
            else if (tabControl2.Visibility == Visibility.Hidden)
                tabControl2.Visibility = Visibility.Visible;
        }

        private void ChepochkaButtonEvent(object sender, RoutedEventArgs e)
        {


        }

        private void GladButtonEvent(object sender, RoutedEventArgs e)
        {


        }
        private void TatamiButtonEvent(object sender, RoutedEventArgs e)
        {


        }
        private void StagkiButtonEvent(object sender, RoutedEventArgs e)
        {
            Ctezhki(ListFigure[IndexFigure].Shapes, new Point(), new Point(), 1, 0,MainCanvas);

        }
        private void RisuiButtonEvent(object sender, RoutedEventArgs e)
        {


        }



    }
}
