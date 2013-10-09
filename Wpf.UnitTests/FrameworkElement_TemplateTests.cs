namespace Avalonia.UnitTests
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FrameworkElement_TemplateTests
    {
        [TestMethod]
        public void TemplatedParent_Should_Be_Set_On_Template_Root()
        {
            Window target = new Window();
            Application app = new Application();
            bool passed = false;

            target.Loaded += (s, e) =>
            {
                FrameworkElement templateRoot = VisualTreeHelper.GetChild(target, 0) as FrameworkElement;
                passed = templateRoot.TemplatedParent == target;
                app.Shutdown();
            };

            app.Run(target);

            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void TemplatedParent_Should_Be_Set_On_Template_Child()
        {
            Window target = new Window();
            Application app = new Application();
            bool passed = false;

            target.Loaded += (s, e) =>
            {
                FrameworkElement templateRoot = VisualTreeHelper.GetChild(target, 0) as FrameworkElement;
                FrameworkElement templateChild = VisualTreeHelper.GetChild(templateRoot, 0) as FrameworkElement;
                templateChild = VisualTreeHelper.GetChild(templateChild, 0) as FrameworkElement;
                passed = templateChild.TemplatedParent == target;
                app.Shutdown();
            };

            app.Run(target);

            Assert.IsTrue(passed);
        }
    }
}
