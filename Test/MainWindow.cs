using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
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
        private Popup popup;
        int index = 1;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = items;

            BitmapDecoder d = BitmapDecoder.Create(
                new Uri("/github_icon.png", UriKind.Relative),
                BitmapCreateOptions.None,
                BitmapCacheOption.None);
            
            int width = d.Frames[0].PixelWidth;
            int height = d.Frames[0].PixelHeight;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            Grid templateRoot = (Grid)this.FindName("templateRoot");
            Border checkBoxBorder = (Border)this.FindName("checkBoxBorder");
            Grid markGrid = (Grid)this.FindName("markGrid");
        }

        private void InitializeComponent()
        {
            Application.LoadComponent(this, new Uri("/Test;component/MainWindow.xaml", UriKind.Relative));
            this.textBox = (TextBox)this.FindName("textBox");
            this.popup = (Popup)this.FindName("popup");
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

        private void showPopupButton_Click(object sender, RoutedEventArgs e)
        {
            this.popup.IsOpen = !this.popup.IsOpen;
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
