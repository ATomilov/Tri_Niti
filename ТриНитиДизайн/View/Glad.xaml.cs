using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ТриНитиДизайн.View
{
    /// <summary>
    /// Логика взаимодействия для Glad.xaml
    /// </summary>
    public partial class Glad : Window
    {
        public Glad()
        {
            InitializeComponent();
            checkbox1.IsChecked = OptionGlad.PoChisluProkolov;
            checkbox2.IsChecked = OptionGlad.PoLenthStezhka;
            textbox1.Text = OptionGlad.LenthStep.ToString();
            textbox2.Text = OptionGlad.Rasshirenie.ToString();
            textbox3.Text = OptionGlad.NumberOfStezhkov.ToString();
            textbox4.Text = OptionGlad.Otstup.ToString();
            textbox5.Text = OptionGlad.NumberOfProkolov.ToString();
            textbox6.Text = OptionGlad.StartLenthStezhok.ToString();
            textbox7.Text = OptionGlad.EndLenthStezhok.ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OptionGlad.PoChisluProkolov = checkbox1.IsChecked ?? false;
            OptionGlad.PoLenthStezhka = checkbox2.IsChecked ?? false;
            try
            {
                OptionGlad.LenthStep = int.Parse(textbox1.Text);
            }
            catch when((int.Parse(textbox1.Text) < OptionGlad.MinLenthStep) || (int.Parse(textbox1.Text) > OptionGlad.MaxLenthStep))
            {
                System.Windows.MessageBox.Show("Длина шага должна быть от " + OptionGlad.MinLenthStep.ToString() + " до " + OptionGlad.MaxLenthStep.ToString(), "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                OptionGlad.Rasshirenie = int.Parse(textbox2.Text);
            }
            catch when ((int.Parse(textbox2.Text) < OptionGlad.MinRasshirenie) || (int.Parse(textbox2.Text) > OptionGlad.MaxRasshirenie))
            {
                System.Windows.MessageBox.Show("Расширение должно быть от " + OptionGlad.MinRasshirenie.ToString() + " до " + OptionGlad.MaxRasshirenie.ToString(), "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                OptionGlad.NumberOfStezhkov = int.Parse(textbox3.Text);
            }
            catch when ((int.Parse(textbox3.Text) < OptionGlad.MinNumberOfStezhkov) || (int.Parse(textbox3.Text) > OptionGlad.MaxNumberOfStezhkov))
            {
                System.Windows.MessageBox.Show("Количество опорных стежков должно быть от " + OptionGlad.MinNumberOfStezhkov.ToString() + " до " + OptionGlad.MaxNumberOfStezhkov.ToString(), "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                OptionGlad.Otstup = int.Parse(textbox4.Text);
            }
            catch when ((int.Parse(textbox4.Text) < OptionGlad.MinOtstup) || (int.Parse(textbox4.Text) > OptionGlad.MaxOtstup))
            {
                System.Windows.MessageBox.Show("Расстояние от края должно быть от " + OptionGlad.MinOtstup.ToString() + " до " + OptionGlad.MaxOtstup.ToString(), "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                OptionGlad.NumberOfProkolov = int.Parse(textbox5.Text);
            }
            catch when ((int.Parse(textbox5.Text) < OptionGlad.MinNumberOfProkolov) || (int.Parse(textbox5.Text) > OptionGlad.MaxNumberOfProkolov))
            {
                System.Windows.MessageBox.Show("Количество проколов должно быть от " + OptionGlad.MinNumberOfProkolov.ToString() + " до " + OptionGlad.MaxNumberOfProkolov.ToString(), "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                OptionGlad.StartLenthStezhok = int.Parse(textbox6.Text);
            }
            catch when ((int.Parse(textbox6.Text) < OptionGlad.MinLehthStezhok) || (int.Parse(textbox6.Text) > OptionGlad.MaxLenthStezhok))
            {
                System.Windows.MessageBox.Show("Длина шага должна быть от " + OptionGlad.MinLehthStezhok.ToString() + " до " + OptionGlad.MaxLenthStezhok.ToString(), "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                OptionGlad.EndLenthStezhok = int.Parse(textbox7.Text);
            }
            catch when ((int.Parse(textbox7.Text) < OptionGlad.MinLehthStezhok) || (int.Parse(textbox7.Text) > OptionGlad.MaxLenthStezhok))
            {
                System.Windows.MessageBox.Show("Длина шага должна быть от " + OptionGlad.MinLehthStezhok.ToString() + " до " + OptionGlad.MaxLenthStezhok.ToString(), "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
