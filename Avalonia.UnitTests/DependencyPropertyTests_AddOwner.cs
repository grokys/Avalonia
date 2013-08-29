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
        public void Should_Use_Base_Metadata_If_None_Supplied()
        {
            PropertyMetadata metadata1 = new PropertyMetadata("foo");
            DependencyProperty dp = DependencyProperty.Register(
                "Should_Use_Base_Metadata_If_None_Supplied",
                typeof(string),
                typeof(TestClass1),
                metadata1);

            dp.AddOwner(typeof(TestClass2));
            dp.AddOwner(typeof(TestClass3));

            PropertyMetadata metadata2 = dp.GetMetadata(typeof(TestClass2));
            PropertyMetadata metadata3 = dp.GetMetadata(typeof(TestClass3));

            Assert.AreSame(metadata1, metadata2);
            Assert.AreSame(metadata2, metadata3);
        }

        [TestMethod]
        public void Should_Merge_Metadata_If_Supplied()
        {
            PropertyMetadata metadata1 = new PropertyMetadata
            {
                DefaultValue = "foo",
                PropertyChangedCallback = this.PropertyChangedCallback1,
                CoerceValueCallback = this.CoerceCallback1,
            };

            DependencyProperty dp = DependencyProperty.Register(
                "Should_Merge_Metadata_If_Supplied",
                typeof(string),
                typeof(TestClass1),
                metadata1);

            PropertyMetadata metadata2 = new PropertyMetadata
            {
                DefaultValue = "bar",
                PropertyChangedCallback = this.PropertyChangedCallback2,
                CoerceValueCallback = this.CoerceCallback2,
            };

            dp.AddOwner(typeof(TestClass2), metadata2);

            PropertyMetadata result = dp.GetMetadata(typeof(TestClass2));

            Assert.AreNotSame(metadata1, result);
            Assert.AreSame(metadata2, result);
            Assert.AreEqual("bar", result.DefaultValue);
            Assert.AreEqual(2, result.PropertyChangedCallback.GetInvocationList().Length);
            Assert.AreEqual(1, result.CoerceValueCallback.GetInvocationList().Length);
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
