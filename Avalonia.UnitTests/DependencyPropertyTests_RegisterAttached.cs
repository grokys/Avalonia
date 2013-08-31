namespace Avalonia.UnitTests
{
#if TEST_WPF
    using System.Windows;
#endif

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
#if TEST_WPF    
    public class Wpf_DependencyPropertyTests_RegisterAttached
#else
    public class DependencyPropertyTests_RegisterAttached
#endif
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_ArumentException_If_Owner_Not_DependencyObject_And_Metadata_Supplied()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "RegisterAttached_Should_Throw_ArumentException_If_Owner_Not_DependencyObject_And_Metadata_Supplied",
                typeof(string),
                typeof(NotDependencyObject),
                new PropertyMetadata("foo"));
        }

        [TestMethod]
        public void Should_Not_Throw_ArumentException_If_Owner_Not_DependencyObject_And_Metadata_Not_Supplied()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "RegisterAttached_Should_Not_Throw_ArumentException_If_Owner_Not_DependencyObject_And_Metadata_Not_Supplied",
                typeof(string),
                typeof(NotDependencyObject));
        }

        [TestMethod]
        public void Should_Create_DefaultMetadata_If_Not_Supplied()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Create_DefaultMetadata_If_Not_Supplied",
                typeof(string),
                typeof(TestClass1));

            Assert.IsNotNull(dp.DefaultMetadata);
        }

        [TestMethod]
        public void Should_Create_Default_Value_Null_For_Reference_Types()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Create_Default_Value_Null_For_Reference_Types",
                typeof(string),
                typeof(TestClass1));

            Assert.IsNull(dp.DefaultMetadata.DefaultValue);
        }

        [TestMethod]
        public void Should_Create_Default_Value_Null_For_Value_Types()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Create_Default_Value_Null_For_Value_Types",
                typeof(string),
                typeof(TestClass1));

            Assert.IsNull(dp.DefaultMetadata.DefaultValue);
        }

        [TestMethod]
        public void Should_Place_Metadata_Into_DefaultMetadata()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(
                "baz",
                this.PropertyChangedCallback,
                this.CoerceCallback);
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Place_Metadata_Into_DefaultMetadata", 
                typeof(string), 
                typeof(TestClass1),
                metadata);

            Assert.AreSame(metadata, dp.DefaultMetadata);
        }

        [TestMethod]
        public void Should_Apply_Metadata_To_OwnerClass()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(
                "baz",
                this.PropertyChangedCallback,
                this.CoerceCallback);
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Apply_Metadata_To_OwnerClass",
                typeof(string),
                typeof(TestClass1),
                metadata);
            PropertyMetadata typeMetadata = dp.GetMetadata(typeof(TestClass1));

            Assert.AreEqual(metadata, typeMetadata);
        }

        [TestMethod]
        public void Should_Set_Name_Property()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Set_Name_Property",
                typeof(string),
                typeof(TestClass1));

            Assert.AreEqual("RegisterAttached_Should_Set_Name_Property", dp.Name);
        }

        [TestMethod]
        public void Should_Set_OwnerType_Property()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Set_OwnerType_Property",
                typeof(string),
                typeof(TestClass1));

            Assert.AreEqual(typeof(TestClass1), dp.OwnerType);
        }

        [TestMethod]
        public void Should_Set_PropertyType_Property()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Set_PropertyType_Property",
                typeof(string),
                typeof(TestClass1));

            Assert.AreEqual(typeof(string), dp.PropertyType);
        }

        [TestMethod]
        public void Should_Set_ReadOnly_Property_To_False()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Set_ReadOnly_Property_To_False",
                typeof(string),
                typeof(TestClass1));

            Assert.IsFalse(dp.ReadOnly);
        }

        [TestMethod]
        public void Should_Set_ValidateValueCallback()
        {
            ValidateValueCallback validateValueCallback = v => { return true; };

            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Set_ValidateValueCallback",
                typeof(string),
                typeof(TestClass1),
                null,
                validateValueCallback);

            Assert.AreEqual(validateValueCallback, dp.ValidateValueCallback);
        }

        [TestMethod]
        public void Should_Call_ValidateValueCallback_With_Default_Value()
        {
            bool validateValueCalled = false;
            ValidateValueCallback validateValueCallback = v =>
            {
                validateValueCalled = ((string)v == "foobar");
                return true;
            };

            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Call_ValidateValueCallback_With_Default_Value",
                typeof(string),
                typeof(TestClass1),
                new PropertyMetadata("foobar"),
                validateValueCallback);

            Assert.IsTrue(validateValueCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_ArgumentException_If_Already_Registered()
        {
            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Throw_ArgumentException_If_Already_Registered",
                typeof(string),
                typeof(TestClass1));

            DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Throw_ArgumentException_If_Already_Registered",
                typeof(string),
                typeof(TestClass1));
        }

        [TestMethod]
        public void Should_Not_Throw_ArgumentException_If_Already_Registered_On_Base()
        {
            DependencyProperty dp = DependencyProperty.Register(
                "RegisterAttached_Should_Not_Throw_ArgumentException_If_Already_Registered_On_Base",
                typeof(string),
                typeof(TestClass1));

            DependencyProperty.Register(
                "RegisterAttached_Should_Not_Throw_ArgumentException_If_Already_Registered_On_Base",
                typeof(string),
                typeof(TestClass2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_Throw_ArgumentException_If_DefaultValid_Is_Invalid()
        {
            ValidateValueCallback validateValueCallback = v =>
            {
                return false;
            };

            DependencyProperty dp = DependencyProperty.RegisterAttached(
                "RegisterAttached_Should_Throw_ArgumentException_If_DefaultValid_Is_Invalid",
                typeof(string),
                typeof(TestClass1),
                new PropertyMetadata("foobar"),
                validateValueCallback);
        }

        private void PropertyChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private object CoerceCallback(DependencyObject d, object baseValue)
        {
            throw new NotImplementedException();
        }

        private class TestClass1 : DependencyObject
        {
        }

        private class TestClass2 : TestClass1
        {
        }

        private class NotDependencyObject
        {
        }
    }
}
