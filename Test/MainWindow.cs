using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace Test
{
    public class MainWindow : Window
    {
        Border border;
        TextBlock textBlock;

        public MainWindow()
        {
            InitializeComponent();

            this.textBlock.Style = (Style)this.Resources["Yellow"];
        }

        private void InitializeComponent()
        {
            Application.LoadComponent(this, new Uri("MainWindow.xaml", UriKind.Relative));
            this.border = (Border)this.FindName("border");
            this.textBlock = (TextBlock)this.FindName("textBlock");
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Console.WriteLine(e.GetPosition(this.textBlock));
        }
    }
}
