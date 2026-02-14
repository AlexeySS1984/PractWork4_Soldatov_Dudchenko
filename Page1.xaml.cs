using System;
using System.Windows;
using System.Windows.Controls;

namespace PractWork4_Soldatov_Dudchenko
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей
                if (string.IsNullOrWhiteSpace(txtX.Text) || 
                    string.IsNullOrWhiteSpace(txtY.Text) || 
                    string.IsNullOrWhiteSpace(txtZ.Text))
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
                double y = double.Parse(txtY.Text.Replace(',', '.'), 
                                      System.Globalization.CultureInfo.InvariantCulture);
                double z = double.Parse(txtZ.Text.Replace(',', '.'), 
                                      System.Globalization.CultureInfo.InvariantCulture);

                // Проверка ограничений
                if (y <= 0)
                {
                    MessageBox.Show("Значение y должно быть больше 0!", 
                                  "Ошибка значения", 
                                  MessageBoxButton.OK, 
                                  MessageBoxImage.Warning);
                    return;
                }

                // Вычисление: a = ln(y^(-√|x|)) · (x - y/2) + sin²(arctg(z))
                double sqrtAbsX = Math.Sqrt(Math.Abs(x));
                double yPower = Math.Pow(y, -sqrtAbsX);
                
                if (yPower <= 0)
                {
                    MessageBox.Show("Промежуточное значение для логарифма получилось неположительным!", 
                                  "Ошибка вычисления", 
                                  MessageBoxButton.OK, 
                                  MessageBoxImage.Warning);
                    return;
                }

                double lnPart = Math.Log(yPower);
                double term1 = lnPart * (x - y / 2.0);
                double arctgZ = Math.Atan(z);
                double sinSqr = Math.Pow(Math.Sin(arctgZ), 2);
                
                double result = term1 + sinSqr;

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
            txtY.Clear();
            txtZ.Clear();
            txtResult.Clear();
            txtX.Focus();
        }
    }
}
