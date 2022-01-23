using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp20test1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> OperationDict; // Используем словарь для операнда и оператора
        string num2Str; // Переменная для хранения второго числа

        public MainWindow()
        {
            InitializeComponent();

            calcWindow.Text = "0";

            AC.Click += ClearC;
            equal.Click += Equal;
            backspace.Click += RemoveAt;
            onedivideby.Click += OneDivideby;
            tosquare.Click += Square;
            squareroot.Click += SquareRoot;
            plusminus.Click += PlusMinus;
            pi.Click += Pi;
            percent.Click += Percent;
            divide.Click += new RoutedEventHandler(Operator);
            multi.Click += new RoutedEventHandler(Operator);
            minus.Click += new RoutedEventHandler(Operator);
            plus.Click += new RoutedEventHandler(Operator);
            dot.Click += new RoutedEventHandler(Numbers);
            num9.Click += new RoutedEventHandler(Numbers);
            num8.Click += new RoutedEventHandler(Numbers);
            num7.Click += new RoutedEventHandler(Numbers);
            num6.Click += new RoutedEventHandler(Numbers);
            num5.Click += new RoutedEventHandler(Numbers);
            num4.Click += new RoutedEventHandler(Numbers);
            num3.Click += new RoutedEventHandler(Numbers);
            num2.Click += new RoutedEventHandler(Numbers);
            num1.Click += new RoutedEventHandler(Numbers);
            num0.Click += new RoutedEventHandler(Numbers);
            OperationDict = new Dictionary<string, string>();

        }

        private void Numbers(object sender, RoutedEventArgs e) // Событие нажатия кнопок
        {
            var num = sender as Button;
            string value = "";

            if (!OperationDict.TryGetValue("Operator", out value)) // Если оператор пуст, сохраненный номер является первым
            {
                if (calcWindow.Text == "0")
                {
                    OperationDict.Clear();
                    calcWindow.Text = num.Content.ToString();
                    OperationDict.Add("a", num.Content.ToString());
                }
                else
                {
                    calcWindow.Text += num.Content.ToString();
                    OperationDict["a"] = calcWindow.Text;
                }
            }
            else // Если оператор не пуст, сохраненный номер является вторым
            {
                if (num2Str == "")
                {
                    calcWindow.Text += num.Content.ToString();
                    num2Str += num.Content.ToString();
                    OperationDict.Add("b", num.Content.ToString());
                }
                else
                {
                    calcWindow.Text += num.Content.ToString();
                    num2Str += num.Content.ToString();
                    OperationDict["b"] = num2Str;
                }
            }
        }

        private void Operator(object sender, RoutedEventArgs e) // Событие нажатия оператора
        {
            try
            {
                var opr = sender as Button;
                if (calcWindow.Text == "0")
                    return;

                switch (opr.Content.ToString())
                {
                    case "+": OperationDict.Add("Operator", "+"); break;
                    case "-": OperationDict.Add("Operator", "-"); break;
                    case "x": OperationDict.Add("Operator", "*"); break;
                    case "÷": OperationDict.Add("Operator", "/"); break;

                }
                calcWindow.Text += opr.Content.ToString();
            }
            catch
            {

            }
        }

        private void Equal(object sender, RoutedEventArgs e) // равно событию нажатия клавиши
        {
            try
            {
                string str1 = "", str2 = "", opr = "";
                if (OperationDict.TryGetValue("a", out str1) && OperationDict.TryGetValue("Operator", out opr) && OperationDict.TryGetValue("b", out str2))
                // Если два операнда и оператора в словаре не пусты, то выполнить операцию
                {
                    double a = double.Parse(str1);
                    double b = double.Parse(str2);
                    
                    switch (opr)
                    {
                        case "+": calcWindow.Text = (a + b).ToString(); break;
                        case "-": calcWindow.Text = (a - b).ToString(); break;
                        case "*": calcWindow.Text = (a * b).ToString(); break;
                        case "/": if (b != 0) calcWindow.Text = (a / b).ToString(); else calcWindow.Text = "Нельзя делить на ноль"; break;
                    }
                    OperationDict.Clear();
                    num2Str = "";
                    OperationDict.Add("a", calcWindow.Text);
                }
                else
                {
                    return;
                }
            }
            catch
            {
                calcWindow.Text = "Ошибка!";
            }
        }

        private void ClearC(object sender, RoutedEventArgs e) // Очистить событие нажатия клавиши
        {
            OperationDict.Clear();
            calcWindow.Text = "0";
            num2Str = "";
        }

        private void RemoveAt(object sender, RoutedEventArgs e) // Убираем последний символ в строке
        {
            try
            {
                string number = calcWindow.Text;
                calcWindow.Text = number.Remove(number.Length - 1);
                OperationDict.Clear();
                num2Str = "";
                OperationDict.Add("a", calcWindow.Text);
                if (calcWindow.Text == "")
                {
                    OperationDict.Clear();
                    calcWindow.Text = "0";
                    num2Str = "";
                }
            }
            catch
            {
                OperationDict.Clear();
                calcWindow.Text = "0";
                num2Str = "";
            }
        }

        private void OneDivideby(object sender, RoutedEventArgs e) // Еденица делиться на текущее значение
        {
            try
            {
                string number = calcWindow.Text;
                if (number == "0" || number == "Нельзя делить на ноль")
                {
                    OperationDict.Clear();
                    calcWindow.Text = "Нельзя делить на ноль";
                    num2Str = "";
                }
                else if (number != "0")
                {
                    calcWindow.Text = (1 / double.Parse(number)).ToString();
                    OperationDict.Clear();
                    num2Str = "";
                    OperationDict.Add("a", calcWindow.Text);
                }
            }
            catch
            {
                calcWindow.Text = "Ошибка!";
            }
            
        }    
 
        private void Square(object sender, RoutedEventArgs e) // Вычисляем квадрат числа
        {
            try
            {
                double number = double.Parse(calcWindow.Text);
                calcWindow.Text = (number * number).ToString();
                OperationDict.Clear();
                num2Str = "";
                OperationDict.Add("a", calcWindow.Text);
            }
            catch
            {
                calcWindow.Text = "Ошибка!";
            }
        }

        private void SquareRoot(object sender, RoutedEventArgs e) // Вычисляем квадратный корень числа
        {
            try
            {
                double number = double.Parse(calcWindow.Text);
                calcWindow.Text = Math.Sqrt(number).ToString();
                OperationDict.Clear();
                num2Str = "";
                OperationDict.Add("a", calcWindow.Text);
            }
            catch
            {
                calcWindow.Text = "Ошибка!";
            }
        }

        private void PlusMinus(object sender, RoutedEventArgs e) // Положительное или отрицательное число
        {
            try
            {
                double numberA = double.Parse(calcWindow.Text);
                if (numberA > 0 || numberA < 0)
                {
                    calcWindow.Text = (numberA * (-1)).ToString();
                    OperationDict.Clear();
                    num2Str = "";
                    OperationDict.Add("a", calcWindow.Text);
                }
            }
            catch
            {
                calcWindow.Text = "Ошибка!";
            }
        }

        private void Pi(object sender, RoutedEventArgs e) // Число Пи
        {
            string calc = calcWindow.Text;
            string value = "";

            if (!OperationDict.TryGetValue("Operator", out value)) // Если оператор пуст, сохраненный номер является первым
            {
                if (calc == "0" || calc == "Нельзя делить на ноль" || calc == "Ошибка!")
                {
                    OperationDict.Clear();
                    calcWindow.Text = Math.PI.ToString();
                    OperationDict.Add("a", Math.PI.ToString());
                }
                else
                {
                    calcWindow.Text = Math.PI.ToString();
                    OperationDict["a"] = calcWindow.Text;
                }
            }
            else // Если оператор не пуст, сохраненный номер является вторым
            {
                if (num2Str == "")
                {
                    calcWindow.Text += Math.PI.ToString();
                    num2Str += Math.PI.ToString();
                    OperationDict.Add("b", Math.PI.ToString());
                }
                else
                {
                    calcWindow.Text += Math.PI.ToString();
                    num2Str += Math.PI.ToString();
                    OperationDict["b"] = num2Str;
                }
            }
        }

        private void Percent(object sender, RoutedEventArgs e) // Вычисляем процент от числа
        {
                string str1 = "", str2 = "", opr = "";
                if (OperationDict.TryGetValue("a", out str1) && OperationDict.TryGetValue("Operator", out opr) && OperationDict.TryGetValue("b", out str2))
                // Если два операнда и оператора в словаре не пусты, то выполнить операцию
                {
                    double a = double.Parse(str1);
                    double b = double.Parse(str2);
                    double percent = (b * a) / 100;

                    switch (opr)
                    {
                        case "+": calcWindow.Text = (a + percent).ToString(); break;
                        case "-": calcWindow.Text = (a - percent).ToString(); break;
                        case "*": calcWindow.Text = percent.ToString(); break;
                        case "/": if (b != 0) calcWindow.Text = (a / percent).ToString(); else calcWindow.Text = "Нельзя делить на ноль"; break;

                    }
                    OperationDict.Clear();
                    num2Str = "";
                    OperationDict.Add("a", calcWindow.Text);
                }
                else
                {
                    return;
                }
        }
    }
}

