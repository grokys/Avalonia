namespace Avalonia.UnitTests.Controls
{
#if TEST_WPF
    using System.Windows.Controls;
#else
    using Avalonia.Controls;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ItemsControlTests
    {
        [TestMethod]
        public void Items_Should_Initially_Wrap_Itself()
        {
            ItemsControl target = new ItemsControl();
            Assert.AreSame(target.Items, target.Items.SourceCollection);
        }

        [TestMethod]
        public void Items_Should_Not_Change_Instance()
        {
            ItemsControl target = new ItemsControl();
            ItemCollection before = target.Items;

            target.ItemsSource = new int[1];

            Assert.AreSame(before, target.Items);
        }

        [TestMethod]
        public void Items_Should_Be_Writable_Initially()
        {
            ItemsControl target = new ItemsControl();
            target.Items.Add(1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Items_Should_Be_Readonly_When_ItemsSource_Assigned()
        {
            ItemsControl target = new ItemsControl();
            target.ItemsSource = new int[1];
            target.Items.Add(1);
        }

        [TestMethod]
        public void Items_Should_Be_Readonly_When_ItemsSource_Nulled()
        {
            ItemsControl target = new ItemsControl();
            target.ItemsSource = new int[1];
            target.ItemsSource = null;
            target.Items.Add(1);
        }

        [TestMethod]
        public void ItemsSource_Set_Assigns_Items()
        {
            ItemsControl target = new ItemsControl();
            int[] collection = new int[1];

            target.ItemsSource = collection;

            Assert.AreSame(collection, target.Items.SourceCollection);
        }

        [TestMethod]
        public void ItemsSource_Null_Unassigns_Items()
        {
            ItemsControl target = new ItemsControl();
            int[] collection = new int[1];

            target.ItemsSource = collection;
            target.ItemsSource = null;

            Assert.AreSame(target.Items, target.Items.SourceCollection);
        }
    }
}
