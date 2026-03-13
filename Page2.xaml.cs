using System;
using System.Windows;
using System.Windows.Controls;
using PractWork4_Soldatov_Dudchenko.BusinessLogic;

namespace PractWork4_Soldatov_Dudchenko
{
    /// <summary>
    /// Страница 2 - Вычисление кусочно-заданной функции e
    /// </summary>
    public partial class Page2 : Page
    {
        /// <summary>
        /// Конструктор страницы
        /// </summary>
        public Page2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Определяет тип функции f(x) на основе выбранной радиокнопки
        /// </summary>
        /// <returns>Тип функции</returns>
        private MathFunctions.FunctionType GetSelectedFunctionType()
        {
            if (rbSinh.IsChecked == true)
                return MathFunctions.FunctionType.HyperbolicSine;
            else if (rbSquare.IsChecked == true)
                return MathFunctions.FunctionType.Square;
            else if (rbExp.IsChecked == true)
                return MathFunctions.FunctionType.Exponential;
            else
                return MathFunctions.FunctionType.HyperbolicSine; // По умолчанию
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

                // Получаем выбранный тип функции
                var functionType = GetSelectedFunctionType();

                // Вычисление через бизнес-логику
                double result = MathFunctions.CalculateFunctionE(x, i, functionType);

                txtResult.Text = result.ToString("F10");
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
            txtI.Clear();
            txtResult.Clear();
            rbSinh.IsChecked = true;
            txtX.Focus();
        }
    }
}
