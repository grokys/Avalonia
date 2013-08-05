namespace Test
{
    using Avalonia;
    using Avalonia.Direct2D1;
    using Avalonia.Threading;

    public class App : Application
    {
        public static void Main()
        {
            App app = new App();
            app.MainWindow = new MainWindow();
            app.Run();
        }
    }
}
