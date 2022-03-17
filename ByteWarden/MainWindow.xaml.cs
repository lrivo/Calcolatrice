using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;

namespace ByteWarden
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Stack<string> history = new Stack<string>();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void OperationBtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string content = btn.Content.ToString();
            if (content == "+" || content == "-" || content == "x" || content == "/")
            {
                OperationLabel.Content += " " + content + " ";
            }
            else
            {
                OperationLabel.Content += content;
            }
        }

        private void EqualsBtnClick(object sender, RoutedEventArgs e)
        {
            string[] operand = OperationLabel.Content.ToString().Split(" ");
            double result;
            try
            {
                result = Convert.ToDouble(operand[0]);
            }
            catch (System.FormatException)
            {
                return;
            }

            for (int i = 1; i < operand.Length; i++)
            {
                try
                {
                    if (operand[i] == "+") result += Convert.ToDouble(operand[i + 1]);
                    else if (operand[i] == "-") result -= Convert.ToDouble(operand[i + 1]);
                    else if (operand[i] == "x") result *= Convert.ToDouble(operand[i + 1]);
                    else if (operand[i] == "/") result /= Convert.ToDouble(operand[i + 1]);
                }
                catch (System.FormatException)
                {
                    return;
                }
            }
            history.Push(OperationLabel.Content.ToString());
            OperationLabel.Content = result.ToString();
        }

        private void DelBtnClick(object sender, RoutedEventArgs e)
        {
            string s = OperationLabel.Content.ToString();
            try
            {
                if (s[s.Length - 1] == ' ')
                    OperationLabel.Content = s.Remove(s.Length - 3);
                else
                    OperationLabel.Content = s.Remove(s.Length - 1);
            }
            catch (Exception)
            {

            }
        }

        private void ACBtnClick(object sender, RoutedEventArgs e)
        {
            OperationLabel.Content = "";
        }

        private void UndoBtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OperationLabel.Content = history.Pop();
            }
            catch (Exception)
            {
                history.Push("");
                return;
            }
        }
    }
}
