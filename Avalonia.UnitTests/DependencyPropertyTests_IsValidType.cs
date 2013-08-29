namespace Avalonia.UnitTests
{
#if TEST_WPF
    using System.Windows;
#endif

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
#if TEST_WPF    
    public class Wpf_DependencyPropertyTests_IsValidType
#else
    public class DependencyPropertyTests_IsValidType
#endif
    {
        [TestMethod]
        public void Null_Is_Valid_Value_For_Reference_Type()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Null_Is_Valid_Value_For_Reference_Type",
                typeof(object),
                typeof(TestClass1));

            Assert.IsTrue(dp.IsValidType(null));
        }

        [TestMethod]
        public void Null_Is_Invalid_Value_For_Reference_Type()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Null_Is_Invalid_Value_For_Reference_Type",
                typeof(int),
                typeof(TestClass1));

            Assert.IsFalse(dp.IsValidType(null));
        }

        [TestMethod]
        public void Null_Is_Valid_Value_For_Nullable_Type()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Null_Is_Valid_Value_For_Nullable_Type",
                typeof(int?),
                typeof(TestClass1));

            Assert.IsTrue(dp.IsValidType(null));
        }

        [TestMethod]
        public void Matching_Value_Types_Are_Valid()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Matching_Types_Are_Valid",
                typeof(int),
                typeof(TestClass1));

            Assert.IsTrue(dp.IsValidType(1));
        }

        [TestMethod]
        public void Matching_Reference_Types_Are_Valid()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Matching_Reference_Types_Are_Valid",
                typeof(UIElement),
                typeof(TestClass1));

            Assert.IsTrue(dp.IsValidType(new UIElement()));
        }

        [TestMethod]
        public void Inherited_Reference_Types_Are_Valid()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Inherited_Reference_Types_Are_Valid",
                typeof(UIElement),
                typeof(TestClass1));

            Assert.IsTrue(dp.IsValidType(new FrameworkElement()));
        }

        [TestMethod]
        public void Implicitly_Castable_Types_Are_Not_Valid()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Implicitly_Castable_Types_Are_Not_Valid",
                typeof(double),
                typeof(TestClass1));

            Assert.IsFalse(dp.IsValidType(1));
        }

        [TestMethod]
        public void Conversions_From_String_Are_Not_Valid()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Conversions_From_String_Are_Not_Valid",
                typeof(int),
                typeof(TestClass1));

            Assert.IsFalse(dp.IsValidType("1"));
        }

        private class TestClass1 : DependencyObject
        {
        }
    }
}
