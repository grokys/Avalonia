namespace Avalonia.Utils
{
    interface IListenPropertyChanged
    {
        void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e);
    }
}
