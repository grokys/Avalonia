namespace Avalonia.UnitTests.Controls
{
#if TEST_WPF
    using System.Windows;
    using System.Windows.Controls;
#else
    using Avalonia.Controls;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
#if TEST_WPF    
    public class Wpf_ContentControlTests
#else
    public class ContentControlTests
#endif
    {
        [TestMethod]
        public void Setting_Content_Should_Initialize_Control()
        {
            ContentControl target = new ContentControl();
            TextBlock child = new TextBlock();

            Assert.IsFalse(target.IsInitialized);
            target.Content = child;
            Assert.IsTrue(target.IsInitialized);
        }
    }
}
