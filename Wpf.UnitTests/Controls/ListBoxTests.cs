namespace Avalonia.UnitTests.Controls
{
#if TEST_WPF
    using System.Windows;
    using System.Windows.Controls;
#else
    using Avalonia;
    using Avalonia.Controls;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    [TestClass]
#if TEST_WPF    
    public class Wpf_ListBoxTests
#else
    public class ListBoxTests
#endif
    {
        [TestMethod]
        public void GetContainerForItemOverride_Should_Return_ListBoxItem()
        {
            TestListBox target = new TestListBox();
            DependencyObject result = target.GetContainerForItemOverride();

            Assert.AreEqual(typeof(ListBoxItem), result.GetType());
        }

        private class TestListBox : ListBox
        {
            public new DependencyObject GetContainerForItemOverride()
            {
                return base.GetContainerForItemOverride();
            }
        }
    }
}
