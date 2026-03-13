using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PractWork4_Soldatov_Dudchenko.BusinessLogic;

namespace PractWork4_Soldatov_Dudchenko
{
    /// <summary>
    /// Страница 3 - Вычисление функции y с построением графика
    /// </summary>
    public partial class Page3 : Page
    {
        /// <summary>
        /// Конструктор страницы
        /// </summary>
        public Page3()
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
                // Параметры по варианту 5
                double x0 = 4.6;
                double xk = 5.8;
                double dx = 0.2;
                double b = 1.3;

                // Вычисление через бизнес-логику
                var result = MathFunctions.CalculateFunctionYRange(x0, xk, dx, b);

                // Создаём коллекцию для графика
                var values = new ChartValues<ObservablePoint>();
                var resultText = new StringBuilder();

                resultText.AppendLine("x\t\ty");
                resultText.AppendLine("─────────────────────────────");

                // Заполняем результаты и график
                for (int i = 0; i < result.XValues.Count; i++)
                {
                    double x = result.XValues[i];
                    double y = result.YValues[i];

                    resultText.AppendLine($"{x:F2}\t\t{y:F4}");
                    values.Add(new ObservablePoint(x, y));
                }

                // Выводим результаты в текстовое поле
                txtResults.Text = resultText.ToString();

                // Строим график
                Chart.Series = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "y = x⁴ + cos(2 + x³ - b)",
                        Values = values,
                        PointGeometry = DefaultGeometries.Circle,
                        PointGeometrySize = 8,
                        Fill = System.Windows.Media.Brushes.Transparent
                    }
                };
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, 
                              "Ошибка параметров", 
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
            txtResults.Clear();
            Chart.Series = null;
        }
    }
}
