using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Test
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            object o = XamlServices.Load("Test.xaml");

            this.Content = o;
            this.Width = 500;
            this.Height = 500;

            this.MouseLeftButtonDown += (s, e) =>
            {
                this.Background = new SolidColorBrush(Colors.Green);
            };
        }
    }
}
