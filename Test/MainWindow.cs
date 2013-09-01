using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;

namespace Test
{
    public class MainWindow : Window
    {
        private Button button;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel { Name = "Data", Description = "Template" };
            this.button.Click += button_Click;
        }

        private void InitializeComponent()
        {
            Application.LoadComponent(this, new Uri("MainWindow.xaml", UriKind.Relative));
            this.button = (Button)this.FindName("button");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ((ViewModel)this.DataContext).Description = "Changed";
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private string name;
        private string description;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name 
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                if (this.description != value)
                {
                    this.description = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Description"));
                    }
                }
            }
        }
    }
}
