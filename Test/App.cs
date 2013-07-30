namespace Test
{
    using Avalonia;
    using Avalonia.Direct2D1;
    using Avalonia.Threading;

    public class App : Application
    {
        public static void Main()
        {
            Dispatcher.PlatformDispatcher = new WindowMessageDispatcher();

            App app = new App();
            app.PresentationSourceType = typeof(HwndSource);
            app.MainWindow = new MainWindow();
            app.Run();
        }
    }
}
