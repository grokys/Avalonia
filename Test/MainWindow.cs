using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ViewModel> items = new ObservableCollection<ViewModel>
        {
            new ViewModel { Name = "Item", Description = "1" },
            new ViewModel { Name = "Item", Description = "2" },
            new ViewModel { Name = "Item", Description = "3" },
        };

        private TextBox textBox;
        private ItemsControl itemsControl;
        private Button add;
        private Button remove;
        int index = 1;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = items;
            this.add.Click += add_Click;
            this.remove.Click += remove_Click;
        }

        private void InitializeComponent()
        {
            Application.LoadComponent(this, new Uri("MainWindow.xaml", UriKind.Relative));
            this.textBox = (TextBox)this.FindName("textBox");
            this.itemsControl = (ItemsControl)this.FindName("itemsControl");
            this.add = (Button)this.FindName("add");
            this.remove = (Button)this.FindName("remove");
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            this.items.Add(new ViewModel 
            { 
                Name = textBox.Text, 
                Description = index++.ToString(),
            });
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            this.items.RemoveAt(0);
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
