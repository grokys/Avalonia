namespace Avalonia.UnitTests
{
#if TEST_WPF
    using System.Windows;
    using System.Windows.Controls;
#else
    using Avalonia.Controls;
#endif

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
#if TEST_WPF    
    public class Wpf_DependencyObjectTests_Inheritance
#else
    public class DependencyObjectTests_Inheritance
#endif
    {
        private static Action<object, string, string> propertyChangedCallback;

        [TestInitialize]
        public void TestInitialize()
        {
            propertyChangedCallback = null;
        }

        [TestMethod]
        public void Adding_Child_Should_Not_Call_PropertyChanged_If_Parent_Value_Unchanged()
        {
            bool called = false;
            string oldValue = null;
            string newValue = null;

            ParentControl parent = new ParentControl();

            ChildControl child = new ChildControl();

            propertyChangedCallback = (s, o, n) =>
            {
                called = s == child;
                oldValue = o;
                newValue = n;
            };

            parent.Children.Add(child);

            Assert.IsFalse(called);
        }

        [TestMethod]
        public void Adding_Child_Should_Call_PropertyChanged_If_Parent_Value_Changed()
        {
            bool called = false;
            string oldValue = null;
            string newValue = null;

            ParentControl parent = new ParentControl();
            parent.Test = "bar";

            ChildControl child = new ChildControl();

            propertyChangedCallback = (s, o, n) =>
            {
                called = s == child;
                oldValue = o;
                newValue = n;
            };

            parent.Children.Add(child);

            Assert.IsTrue(called);
            Assert.AreEqual(oldValue, "foo");
            Assert.AreEqual(newValue, "bar");
        }

        [TestMethod]
        public void Setting_Parent_Value_Should_Call_PropertyChanged_If_Child_Value_Unset()
        {
            bool called = false;
            string oldValue = null;
            string newValue = null;

            ParentControl parent = new ParentControl();
            ChildControl child = new ChildControl();
            parent.Children.Add(child);

            propertyChangedCallback = (s, o, n) =>
            {
                called = s == child;
                oldValue = o;
                newValue = n;
            };

            parent.Test = "bar";

            Assert.IsTrue(called);
            Assert.AreEqual(oldValue, "foo");
            Assert.AreEqual(newValue, "bar");
        }

        [TestMethod]
        public void Setting_Parent_Value_Should_Not_Call_PropertyChanged_If_Child_Value_Set()
        {
            bool called = false;
            string oldValue = null;
            string newValue = null;

            ParentControl parent = new ParentControl();
            ChildControl child = new ChildControl();
            child.Test = "bar";
            parent.Children.Add(child);

            propertyChangedCallback = (s, o, n) =>
            {
                called = s == child;
                oldValue = o;
                newValue = n;
            };

            parent.Test = "bar";

            Assert.IsFalse(called);
        }

        private static void TestPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (propertyChangedCallback != null)
            {
                propertyChangedCallback(sender, (string)e.OldValue, (string)e.NewValue);
            }
        }

        private class ParentControl : StackPanel
        {
            public static readonly DependencyProperty TestProperty =
                DependencyProperty.Register(
                    "TestProperty",
                    typeof(string),
                    typeof(UIElement),
                    new FrameworkPropertyMetadata(
                        "foo",
                        FrameworkPropertyMetadataOptions.Inherits,
                        TestPropertyChanged));

            public string Test
            {
                get { return (string)this.GetValue(TestProperty); }
                set { this.SetValue(TestProperty, value); }
            }
        }

        private class ChildControl : FrameworkElement
        {
            public static readonly DependencyProperty TestProperty =
                ParentControl.TestProperty.AddOwner(typeof(ChildControl));

            public string Test
            {
                get { return (string)this.GetValue(TestProperty); }
                set { this.SetValue(TestProperty, value); }
            }
        }
    }
}
