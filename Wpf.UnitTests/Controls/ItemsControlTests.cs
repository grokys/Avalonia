namespace Avalonia.UnitTests.Controls
{
#if TEST_WPF
    using System.Windows.Controls;
#else
    using Avalonia.Controls;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

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
        public void Items_Should_Not_Change_Instance_When_ItemsSource_Set()
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
        public void Items_Not_Should_Be_Readonly_When_ItemsSource_Nulled()
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

        [TestMethod]
        public void Items_Propogates_CollectionChanged_Events()
        {
            ItemsControl target = new ItemsControl();
            ObservableCollection<int> collection = new ObservableCollection<int>();
            object sender = null;

            target.ItemsSource = collection;
            ((INotifyCollectionChanged)target.Items).CollectionChanged += (s, e) => sender = s;
            collection.Add(1);

            Assert.AreSame(target.Items, sender);
        }
    }
}
