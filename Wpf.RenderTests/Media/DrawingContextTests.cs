namespace Avalonia.RenderTests.Media
{
#if TEST_WPF
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
#else
    using Avalonia.Controls;
    using Avalonia.Media;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DrawingContextTests : TestBase
    {
        public DrawingContextTests()
            : base(@"Media\DrawingContext")
        {
        }

        [TestMethod]
        public void DrawRectangle_Stroke_1px()
        {
            UserControl userControl = new UserControl();
            userControl.Width = 200;
            userControl.Height = 200;

            TestControl testControl = new TestControl();
            userControl.Content = testControl;

            this.RenderToFile(userControl);
            this.CompareImages();
        }

        private class TestControl : FrameworkElement
        {
            protected override void OnRender(DrawingContext drawingContext)
            {
                drawingContext.DrawRectangle(
                    null,
                    new Pen(new SolidColorBrush(Colors.Red), 1),
                    new Rect(10, 10, 180, 180.5));
            }
        }
    }
}
