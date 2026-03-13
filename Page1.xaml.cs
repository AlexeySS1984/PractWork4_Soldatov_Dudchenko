using System;
using System.Windows;
using System.Windows.Controls;
using PractWork4_Soldatov_Dudchenko.BusinessLogic;

namespace PractWork4_Soldatov_Dudchenko
{
    /// <summary>
    /// Страница 1 - Вычисление функции a
    /// </summary>
    public partial class Page1 : Page
    {
        /// <summary>
        /// Конструктор страницы
        /// </summary>
        public Page1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычислить"
        /// </summary>
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

                // Вычисление через бизнес-логику
                double result = MathFunctions.CalculateFunctionA(x, y, z);

                txtResult.Text = result.ToString("F10");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, 
                              "Ошибка значения", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, 
                              "Ошибка вычисления", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
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

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить"
        /// </summary>
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
