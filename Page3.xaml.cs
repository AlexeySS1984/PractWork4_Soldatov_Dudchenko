using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace PractWork4_Soldatov_Dudchenko
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Параметры по варианту 5
                double x0 = 4.6;
                double xk = 5.8;
                double dx = 0.2;
                double b = 1.3;

                // Создаём коллекции для результатов и графика
                var values = new ChartValues<ObservablePoint>();
                var results = new StringBuilder();
                
                results.AppendLine("x\t\ty");
                results.AppendLine("─────────────────────────────");

                // Вычисление функции в цикле
                for (double x = x0; x <= xk; x += dx)
                {
                    // y = x⁴ + cos(2 + x³ - b)
                    double x4 = Math.Pow(x, 4);
                    double x3 = Math.Pow(x, 3);
                    double cosArg = 2 + x3 - b;
                    double y = x4 + Math.Cos(cosArg);

                    // Добавляем в результаты
                    results.AppendLine($"{x:F2}\t\t{y:F4}");
                    
                    // Добавляем точку для графика
                    values.Add(new ObservablePoint(x, y));
                }

                // Выводим результаты в текстовое поле
                txtResults.Text = results.ToString();

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
            txtResults.Clear();
            Chart.Series = null;
        }
    }
}
