using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using Calculator;

namespace UI_Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        

        private void AddSymbol(object sender, RoutedEventArgs e)
        {
            Button AdderButton = (Button)sender;
            Debug.Write(AdderButton.Content);
            Calculation.Text += AdderButton.Content.ToString();
        }

        private void DeleteSymbol(object sender, RoutedEventArgs e)
        {

            int length = Calculation.Text.Length;

            if (length == 0)
            {
                return;
            }

            int last_symbol = length - 1;
            Calculation.Text = Calculation.Text.Remove(last_symbol);
        }

        private void ClearCalculation(object sender, RoutedEventArgs e)
        {
            Calculation.Text = "";
        }

        private void StartCalculations(object sender, RoutedEventArgs e)
        {
            try
            {
                Tokenizer tokenizer = new Tokenizer(Calculation.Text);
                Parser parser = new Parser(tokenizer.Tokens);
                Calculation.Text = parser.Value.ToString();
            }
            catch { }
        }
    }
}
