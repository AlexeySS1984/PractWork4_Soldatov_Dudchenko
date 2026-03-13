using System;
using System.Collections.Generic;

namespace PractWork4_Soldatov_Dudchenko.BusinessLogic
{
    /// <summary>
    /// Класс для вычисления математических функций
    /// </summary>
    public static class MathFunctions
    {
        #region Функция A (Страница 1)

        /// <summary>
        /// Вычисляет функцию a = ln(y^(-√|x|)) · (x - y/2) + sin²(arctg(z))
        /// </summary>
        /// <param name="x">Параметр x</param>
        /// <param name="y">Параметр y (должен быть больше 0)</param>
        /// <param name="z">Параметр z</param>
        /// <returns>Результат вычисления функции a</returns>
        /// <exception cref="ArgumentException">Выбрасывается, если y <= 0</exception>
        public static double CalculateFunctionA(double x, double y, double z)
        {
            // Проверка ограничений
            if (y <= 0)
            {
                throw new ArgumentException("Значение y должно быть больше 0", nameof(y));
            }

            // Вычисление: a = ln(y^(-√|x|)) · (x - y/2) + sin²(arctg(z))
            double sqrtAbsX = Math.Sqrt(Math.Abs(x));
            double yPower = Math.Pow(y, -sqrtAbsX);

            if (yPower <= 0)
            {
                throw new InvalidOperationException("Промежуточное значение для логарифма получилось неположительным");
            }

            double lnPart = Math.Log(yPower);
            double term1 = lnPart * (x - y / 2.0);
            double arctgZ = Math.Atan(z);
            double sinSqr = Math.Pow(Math.Sin(arctgZ), 2);

            double result = term1 + sinSqr;

            // Проверка на NaN и Infinity
            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                throw new InvalidOperationException("Результат вычисления некорректен");
            }

            return result;
        }

        #endregion

        #region Функция E (Страница 2)

        /// <summary>
        /// Типы функции f(x) для кусочно-заданной функции
        /// </summary>
        public enum FunctionType
        {
            /// <summary>
            /// Гиперболический синус sh(x)
            /// </summary>
            HyperbolicSine,

            /// <summary>
            /// Квадрат x²
            /// </summary>
            Square,

            /// <summary>
            /// Экспонента e^x
            /// </summary>
            Exponential
        }

        /// <summary>
        /// Вычисляет функцию f(x) в зависимости от выбранного типа
        /// </summary>
        /// <param name="x">Параметр x</param>
        /// <param name="functionType">Тип функции</param>
        /// <returns>Значение f(x)</returns>
        private static double CalculateFx(double x, FunctionType functionType)
        {
            switch (functionType)
            {
                case FunctionType.HyperbolicSine:
                    return Math.Sinh(x);
                case FunctionType.Square:
                    return x * x;
                case FunctionType.Exponential:
                    return Math.Exp(x);
                default:
                    throw new ArgumentException("Неизвестный тип функции", nameof(functionType));
            }
        }

        /// <summary>
        /// Вычисляет кусочно-заданную функцию e:
        /// - i√f(x), если i - нечётное, x > 0
        /// - i/2·√|f(x)|, если i - чётное, x &lt; 0
        /// - √|if(x)|, иначе
        /// </summary>
        /// <param name="x">Параметр x</param>
        /// <param name="i">Параметр i (целое число для определения чётности)</param>
        /// <param name="functionType">Тип функции f(x)</param>
        /// <returns>Результат вычисления функции e</returns>
        public static double CalculateFunctionE(double x, int i, FunctionType functionType)
        {
            // Вычисление f(x)
            double fx = CalculateFx(x, functionType);

            double result;

            // Определяем, является ли i чётным или нечётным
            bool isOdd = (i % 2 != 0);

            // Вычисление по условиям
            if (isOdd && x > 0)
            {
                // i√f(x)
                if (fx < 0)
                {
                    throw new InvalidOperationException("f(x) < 0 при условии 'i нечётное и x > 0'. Невозможно извлечь корень");
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
                throw new InvalidOperationException("Результат вычисления некорректен");
            }

            return result;
        }

        #endregion

        #region Функция Y (Страница 3)

        /// <summary>
        /// Результат вычисления функции Y с табличными данными
        /// </summary>
        public class FunctionYResult
        {
            /// <summary>
            /// Список значений X
            /// </summary>
            public List<double> XValues { get; set; }

            /// <summary>
            /// Список значений Y
            /// </summary>
            public List<double> YValues { get; set; }

            /// <summary>
            /// Конструктор
            /// </summary>
            public FunctionYResult()
            {
                XValues = new List<double>();
                YValues = new List<double>();
            }
        }

        /// <summary>
        /// Вычисляет функцию y = x⁴ + cos(2 + x³ - b) для одного значения x
        /// </summary>
        /// <param name="x">Значение x</param>
        /// <param name="b">Константа b</param>
        /// <returns>Значение y</returns>
        public static double CalculateFunctionY(double x, double b)
        {
            double x4 = Math.Pow(x, 4);
            double x3 = Math.Pow(x, 3);
            double cosArg = 2 + x3 - b;
            double y = x4 + Math.Cos(cosArg);

            return y;
        }

        /// <summary>
        /// Вычисляет функцию y = x⁴ + cos(2 + x³ - b) для диапазона значений
        /// </summary>
        /// <param name="x0">Начальное значение x</param>
        /// <param name="xk">Конечное значение x</param>
        /// <param name="dx">Шаг изменения x</param>
        /// <param name="b">Константа b</param>
        /// <returns>Результат с массивами x и y</returns>
        /// <exception cref="ArgumentException">Выбрасывается при некорректных параметрах</exception>
        public static FunctionYResult CalculateFunctionYRange(double x0, double xk, double dx, double b)
        {
            if (dx <= 0)
            {
                throw new ArgumentException("Шаг dx должен быть положительным", nameof(dx));
            }

            if (x0 > xk)
            {
                throw new ArgumentException("Начальное значение x0 должно быть меньше или равно конечному xk");
            }

            var result = new FunctionYResult();

            // Вычисление функции в цикле
            for (double x = x0; x <= xk; x += dx)
            {
                double y = CalculateFunctionY(x, b);
                result.XValues.Add(x);
                result.YValues.Add(y);
            }

            return result;
        }

        #endregion
    }
}
