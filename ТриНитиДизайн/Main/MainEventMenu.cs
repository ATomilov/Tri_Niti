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
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog op = new SaveFileDialog();
            op.Filter = "Проект Три Нити Дизайн| *.tri";
            op.ShowDialog();
        }


        private void NewProject(object sender, RoutedEventArgs e)
        {
           
        }
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Проект Три Нити Дизайн| *.tri";
            op.ShowDialog();
        }
        private void OpenWindowsColor(object sender, RoutedEventArgs e)
        {
            WindowColors window = new WindowColors();
            window.Show();
        }


        private void OpenSetting(object sender, RoutedEventArgs e)
        {
            Setting set = new Setting();
            set.ShowInTaskbar = false;
            set.ShowDialog();
        }


        private void OpenAbout(object sender, RoutedEventArgs e)
        {
            About ab = new About();
            ab.ShowInTaskbar = false;
            ab.ShowDialog();
        }
    }
}
