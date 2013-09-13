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
    public class Wpf_DependencyPropertyTests_GetMetadata
#else
    public class DependencyPropertyTests_GetMetadata
#endif
    {
        [TestMethod]
        public void Registered_Class_Should_Return_Metadata_If_None_Supplied()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Registered_Class_Should_Return_Metadata_If_None_Supplied",
                typeof(string),
                typeof(TestClass1));

            PropertyMetadata result = dp.GetMetadata(typeof(TestClass1));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Derived_Class_Should_Return_Registered_Metadata_If_None_Supplied()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Derived_Class_Should_Return_Registered_Metadata_If_None_Supplied",
                typeof(string),
                typeof(TestClass1));

            PropertyMetadata result1 = dp.GetMetadata(typeof(TestClass1));
            PropertyMetadata result2 = dp.GetMetadata(typeof(TestClass2));

            Assert.AreSame(result1, result2);
        }

        [TestMethod]
        public void Foreign_Class_Should_Return_Registered_Metadata_If_None_Supplied()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "Foreign_Class_Should_Return_Registered_Metadata_If_None_Supplied",
                typeof(string),
                typeof(TestClass1));

            PropertyMetadata result1 = dp.GetMetadata(typeof(TestClass1));
            PropertyMetadata result4 = dp.GetMetadata(typeof(TestClass4));

            Assert.AreSame(result1, result4);
        }

        [TestMethod]
        public void Registered_Class_Should_Return_Metadata()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "Registered_Class_Should_Return_Metadata",
                typeof(string),
                typeof(TestClass1),
                metadata);

            PropertyMetadata result = dp.GetMetadata(typeof(TestClass1));

            Assert.AreSame(metadata, result);
        }

        [TestMethod]
        public void Foreign_Class_Should_Return_Copy_Of_Registered_Metadata()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(
                "foo",
                FrameworkPropertyMetadataOptions.Inherits,
                this.PropertyChangedCallback,
                this.CoerceCallback);

            DependencyProperty dp = DependencyProperty.Register(
                "Foreign_Class_Should_Return_Copy_Of_Registered_Metadata",
                typeof(string),
                typeof(TestClass1),
                metadata);

            PropertyMetadata result1 = dp.GetMetadata(typeof(TestClass1));
            PropertyMetadata result4 = dp.GetMetadata(typeof(TestClass4));

            Assert.AreSame(metadata, result1);
            Assert.AreNotSame(result1, result4);
            Assert.AreSame(typeof(PropertyMetadata), result4.GetType());
            Assert.AreEqual("foo", result4.DefaultValue);
            Assert.IsNull(result4.PropertyChangedCallback);
            Assert.IsNull(result4.CoerceValueCallback);
        }

        [TestMethod]
        public void Overridden_Class_Should_Return_Overridden_Metadata()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "Overridden_Class_Should_Return_Overridden_Metadata",
                typeof(string),
                typeof(TestClass1),
                metadata);

            FrameworkPropertyMetadata overridden = new FrameworkPropertyMetadata("bar");
            dp.OverrideMetadata(typeof(TestClass2), overridden);

            PropertyMetadata result = dp.GetMetadata(typeof(TestClass2));

            Assert.AreSame(overridden, result);
        }

        [TestMethod]
        public void Overridden_Derived_Class_Should_Return_Overridden_Metadata()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "Overridden_Derived_Class_Should_Return_Overridden_Metadata",
                typeof(string),
                typeof(TestClass1),
                metadata);

            FrameworkPropertyMetadata overridden = new FrameworkPropertyMetadata("bar");
            dp.OverrideMetadata(typeof(TestClass2), overridden);

            PropertyMetadata result = dp.GetMetadata(typeof(TestClass3));

            Assert.AreSame(overridden, result);
        }

        [TestMethod]
        public void AddOwnered_Class_Should_Return_Overridden_Metadata()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "AddOwnered_Class_Should_Return_Overridden_Metadata",
                typeof(string),
                typeof(TestClass1),
                metadata);

            FrameworkPropertyMetadata overridden = new FrameworkPropertyMetadata("bar");
            dp.AddOwner(typeof(TestClass4), overridden);

            PropertyMetadata result = dp.GetMetadata(typeof(TestClass4));

            Assert.AreSame(overridden, result);
        }

        [TestMethod]
        public void AddOwnered_Derived_Class_Should_Return_Overridden_Metadata()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "AddOwnered_Derived_Class_Should_Return_Overridden_Metadata",
                typeof(string),
                typeof(TestClass1),
                metadata);

            FrameworkPropertyMetadata overridden = new FrameworkPropertyMetadata("bar");
            dp.OverrideMetadata(typeof(TestClass4), overridden);

            PropertyMetadata result = dp.GetMetadata(typeof(TestClass5));

            Assert.AreSame(overridden, result);
        }

        private void PropertyChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private object CoerceCallback(DependencyObject d, object baseValue)
        {
            throw new NotImplementedException();
        }

        private class TestClass1 : DependencyObject { }
        private class TestClass2 : TestClass1 { }
        private class TestClass3 : TestClass2 { }
        private class TestClass4 : DependencyObject { }
        private class TestClass5 : TestClass4 { }
    }
}
