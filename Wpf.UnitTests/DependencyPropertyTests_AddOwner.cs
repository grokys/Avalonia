namespace Avalonia.UnitTests
{
#if TEST_WPF
    using System.Windows;
#endif

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
#if TEST_WPF    
    public class Wpf_DependencyPropertyTests_AddOwner
#else
    public class DependencyPropertyTests_AddOwner
#endif
    {
        [TestMethod]
        public void Should_Not_Change_OwnerType()
        {
            PropertyMetadata metadata1 = new PropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "Should_Not_Change_OwnerType",
                typeof(string),
                typeof(TestClass1),
                metadata1);

            dp.AddOwner(typeof(TestClass2));

            Assert.AreEqual(typeof(TestClass1), dp.OwnerType);
        }

        [TestMethod]
        public void Should_Return_Same_DependencyProperty()
        {
            PropertyMetadata metadata1 = new PropertyMetadata("foo");
            DependencyProperty dp1 = DependencyProperty.Register(
                "Should_Return_Same_DependencyProperty",
                typeof(string),
                typeof(TestClass1),
                metadata1);

            DependencyProperty dp2 = dp1.AddOwner(typeof(TestClass2));

            Assert.AreSame(dp1, dp2);
        }

        [TestMethod]
        public void Should_Use_Base_Metadata_If_None_Supplied()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "Should_Use_Base_Metadata_If_None_Supplied",
                typeof(string),
                typeof(TestClass1),
                metadata);

            dp.AddOwner(typeof(TestClass2));
            dp.AddOwner(typeof(TestClass3));

            PropertyMetadata metadata1 = dp.GetMetadata(typeof(TestClass1));
            PropertyMetadata metadata2 = dp.GetMetadata(typeof(TestClass2));
            PropertyMetadata metadata3 = dp.GetMetadata(typeof(TestClass3));

            Assert.AreSame(metadata, metadata1);
            Assert.AreSame(metadata1, metadata2);
            Assert.AreSame(metadata2, metadata3);
        }

        [TestMethod]
        public void Should_Merge_Metadata_If_Supplied()
        {
            FrameworkPropertyMetadata metadata1 = new FrameworkPropertyMetadata(
                "foo",
                FrameworkPropertyMetadataOptions.Inherits,
                this.PropertyChangedCallback1,
                this.CoerceCallback1);

            DependencyProperty dp = DependencyProperty.Register(
                "Should_Merge_Metadata_If_Supplied",
                typeof(string),
                typeof(TestClass1),
                metadata1);

            FrameworkPropertyMetadata metadata2 = new FrameworkPropertyMetadata(
                "bar",
                FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender,
                this.PropertyChangedCallback2,
                this.CoerceCallback2);

            dp.AddOwner(typeof(TestClass2), metadata2);

            FrameworkPropertyMetadata result = dp.GetMetadata(typeof(TestClass2)) as FrameworkPropertyMetadata;

            Assert.IsNotNull(result);
            Assert.AreNotSame(metadata1, result);
            Assert.AreSame(metadata2, result);
            Assert.AreEqual("bar", result.DefaultValue);
            Assert.IsTrue(result.Inherits);
            Assert.IsTrue(result.AffectsRender);
            Assert.AreEqual(2, result.PropertyChangedCallback.GetInvocationList().Length);
            Assert.AreEqual(1, result.CoerceValueCallback.GetInvocationList().Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_Exception_If_Overridden_With_Less_Derived_Metadata()
        {
            FrameworkPropertyMetadata metadata1 = new FrameworkPropertyMetadata(
                "foo",
                FrameworkPropertyMetadataOptions.Inherits,
                this.PropertyChangedCallback1,
                this.CoerceCallback1);

            DependencyProperty dp = DependencyProperty.Register(
                "Should_Merge_Metadata_If_Supplied",
                typeof(string),
                typeof(TestClass1),
                metadata1);

            PropertyMetadata metadata2 = new PropertyMetadata("bar");

            dp.AddOwner(typeof(TestClass2), metadata2);
        }

        private void PropertyChangedCallback1(object sender, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PropertyChangedCallback2(object sender, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private object CoerceCallback1(DependencyObject d, object baseValue)
        {
            throw new NotImplementedException();
        }

        private object CoerceCallback2(DependencyObject d, object baseValue)
        {
            throw new NotImplementedException();
        }

        private class TestClass1 : DependencyObject { }
        private class TestClass2 : TestClass1 { }
        private class TestClass3 : TestClass2 { }
    }
}
