using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Test
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            Border border = new Border();
            border.Background = new SolidColorBrush(Colors.Red);
            border.CornerRadius = new CornerRadius(8);
            border.Margin = new Thickness(8);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Hello World!";
            border.Child = textBlock;

            this.Content = border;
            this.Width = 500;
            this.Height = 500;

            this.MouseLeftButtonDown += (s, e) =>
            {
                this.Background = new SolidColorBrush(Colors.Green);
            };
        }
    }
}
