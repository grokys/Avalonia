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
        Border border;

        public MainWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (s, e) =>
            {
                this.Background = new SolidColorBrush(Colors.Green);
            };
        }

        private void InitializeComponent()
        {
            Application.LoadComponent(this, new Uri("MainWindow.xaml", UriKind.Relative));
            this.border = (Border)this.FindName("border");
        }
    }
}
