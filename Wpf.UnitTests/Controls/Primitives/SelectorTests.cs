namespace Avalonia.UnitTests.Controls.Primitives
{
#if TEST_WPF
    using System.Windows;
    using System.Windows.Controls.Primitives;
#else
    using Avalonia.Controls.Primitives;
#endif

    using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

    [TestClass]
#if TEST_WPF    
    public class Wpf_SelectorTests
#else
    public class SelectorTests
#endif
    {
        [TestMethod]
        public void SelectedIndex_Should_Initially_Be_Minus_1()
        {
            TestSelector target = new TestSelector();

            Assert.AreEqual(-1, target.SelectedIndex);
        }

        [TestMethod]
        public void SelectedItem_Should_Initially_Be_Null()
        {
            TestSelector target = new TestSelector();

            Assert.IsNull(target.SelectedItem);
        }

        [TestMethod]
        public void SelectedValue_Should_Initially_Be_Null()
        {
            TestSelector target = new TestSelector();

            Assert.IsNull(target.SelectedValue);
        }

        [TestMethod]
        public void Setting_SelectedIndex_Should_Set_SelectedItem()
        {
            TestSelector target = new TestSelector();

            target.SelectedIndex = 1;

            Assert.AreEqual(target.TestItems[1], target.SelectedItem);
        }

        [TestMethod]
        public void Setting_SelectedIndex_Should_Set_SelectedValue()
        {
            TestSelector target = new TestSelector();

            target.SelectedIndex = 1;

            Assert.AreEqual(target.TestItems[1], target.SelectedValue);
        }

        [TestMethod]
        public void Setting_SelectedIndex_With_SelectedValuePath_Should_Set_SelectedValue()
        {
            TestSelector target = new TestSelector();

            target.SelectedValuePath = "Caption";
            target.SelectedIndex = 1;

            Assert.AreEqual("Bar", target.SelectedValue);
        }

        [TestMethod]
        public void Setting_SelectedIndex_With_Invalid_SelectedValuePath_Should_()
        {
            TestSelector target = new TestSelector();

            target.SelectedValuePath = "Invalid";
            target.SelectedIndex = 1;

            Assert.IsNull(target.SelectedValue);
        }

        [TestMethod]
        public void Setting_SelectedIndex_To_Minus_1_Should_Clear_SelectedItem()
        {
            TestSelector target = new TestSelector();

            target.SelectedIndex = 1;
            target.SelectedIndex = -1;

            Assert.IsNull(target.SelectedItem);
        }

        [TestMethod]
        public void Setting_SelectedIndex_To_Minus_1_Should_Clear_SelectedValue()
        {
            TestSelector target = new TestSelector();

            target.SelectedIndex = 1;
            target.SelectedIndex = -1;

            Assert.IsNull(target.SelectedValue);
        }

        [TestMethod]
        public void Setting_SelectedIndex_Out_Of_Range_Should_Leave_SelectedIndex_Unchanged()
        {
            TestSelector target = new TestSelector();

            target.SelectedIndex = 1;
            target.SelectedIndex = 4;

            Assert.AreEqual(1, target.SelectedIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setting_SelectedIndex_To_Invalid_Negative_Value_Should_Throw_Exception()
        {
            TestSelector target = new TestSelector();

            target.SelectedIndex = 1;
            target.SelectedIndex = -2;
        }

        [TestMethod]
        public void Setting_SelectedItem_Should_Set_SelectedIndex()
        {
            TestSelector target = new TestSelector();

            target.SelectedItem = target.TestItems[1];

            Assert.AreEqual(1, target.SelectedIndex);
        }

        [TestMethod]
        public void Setting_SelectedItem_Should_Set_SelectedValue()
        {
            TestSelector target = new TestSelector();

            target.SelectedItem = target.TestItems[1];

            Assert.AreEqual(target.TestItems[1], target.SelectedValue);
        }

        [TestMethod]
        public void Setting_SelectedItem_With_SelectedValuePath_Should_Set_SelectedValue()
        {
            TestSelector target = new TestSelector();

            target.SelectedValuePath = "Caption";
            target.SelectedItem = target.TestItems[1];

            Assert.AreEqual("Bar", target.SelectedValue);
        }

        [TestMethod]
        public void Setting_SelectedItem_To_Invalid_Value_Should_Leave_SelectedIndex_Unchanged()
        {
            TestSelector target = new TestSelector();

            target.SelectedIndex = 1;
            target.SelectedItem = "Invalid";

            Assert.AreEqual(1, target.SelectedIndex);
        }

        private class TestSelector : Selector
        {
            public TestSelector()
            {
                foreach (object o in this.TestItems)
                {
                    this.Items.Add(o);
                }
            }

            public readonly object[] TestItems = new[]
            {
                new { Caption = "Foo" },
                new { Caption = "Bar" },
                new { Caption = "Baz" },
            };
        }
    }
}
