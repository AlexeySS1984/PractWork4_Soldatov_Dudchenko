using System;
using System.Windows;
using System.Windows.Controls;

namespace PractWork4_Soldatov_Dudchenko
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private double CalculateFx(double x)
        {
            if (rbSinh.IsChecked == true)
            {
                // sh(x) = (e^x - e^(-x))/2
                return Math.Sinh(x);
            }
            else if (rbSquare.IsChecked == true)
            {
                // x²
                return x * x;
            }
            else if (rbExp.IsChecked == true)
            {
                // e^x
                return Math.Exp(x);
            }
            return 0;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей
                if (string.IsNullOrWhiteSpace(txtX.Text) || 
                    string.IsNullOrWhiteSpace(txtI.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", 
                                  "Ошибка ввода", 
                                  MessageBoxButton.OK, 
                                  MessageBoxImage.Warning);
                    return;
                }

                // Парсинг значений
                double x = double.Parse(txtX.Text.Replace(',', '.'), 
                                      System.Globalization.CultureInfo.InvariantCulture);
                int i = int.Parse(txtI.Text);

                // Вычисление f(x)
                double fx = CalculateFx(x);

                double result = 0;

                // Определяем, является ли i чётным или нечётным
                bool isOdd = (i % 2 != 0);

                // Вычисление по условиям:
                // i√f(x), если i - нечётное, x > 0
                // i/2·√|f(x)|, если i - чётное, x < 0
                // √|if(x)|, иначе

                if (isOdd && x > 0)
                {
                    // i√f(x)
                    if (fx < 0)
                    {
                        MessageBox.Show("f(x) < 0 при условии 'i нечётное и x > 0'. Невозможно извлечь корень!", 
                                      "Ошибка вычисления", 
                                      MessageBoxButton.OK, 
                                      MessageBoxImage.Warning);
                        return;
                    }
                    result = i * Math.Sqrt(fx);
                }
                else if (!isOdd && x < 0)
                {
                    // i/2·√|f(x)|
                    result = (i / 2.0) * Math.Sqrt(Math.Abs(fx));
                }
                else
                {
                    // √|if(x)|
                    result = Math.Sqrt(Math.Abs(i * fx));
                }

                // Проверка на NaN и Infinity
                if (double.IsNaN(result) || double.IsInfinity(result))
                {
                    MessageBox.Show("Результат вычисления некорректен!", 
                                  "Ошибка вычисления", 
                                  MessageBoxButton.OK, 
                                  MessageBoxImage.Error);
                    return;
                }

                txtResult.Text = result.ToString("F10");
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения!", 
                              "Ошибка формата", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", 
                              "Ошибка", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Error);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtX.Clear();
            txtI.Clear();
            txtResult.Clear();
            rbSinh.IsChecked = true;
            txtX.Focus();
        }
    }
}
