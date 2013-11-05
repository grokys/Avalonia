namespace Avalonia.UnitTests.Data
{
#if TEST_WPF
    using System.Windows.Data;
#else
    using Avalonia.Data;
#endif

    using System.Collections;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System;
    using System.Collections.Specialized;

    [TestClass]
    public class CollectionViewTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Does_Not_Allow_Null_Source()
        {
            CollectionView target = new CollectionView(null);
        }

        [TestMethod]
        public void SourceCollection_Is_Set()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.AreSame(source, target.SourceCollection);
        }

        [TestMethod]
        public void Initial_Items_Are_Same_As_SourceCollection()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            CollectionAssert.AreEqual(source, ((IEnumerable)target).Cast<object>().ToArray());
        }

        [TestMethod]
        public void CanFilter_Returns_True()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.CanFilter);
        }

        [TestMethod]
        public void CanGroup_Returns_False()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.CanGroup);
        }

        [TestMethod]
        public void CanSort_Returns_False()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.CanSort);
        }

        [TestMethod]
        public void Comparer_Is_Initially_Null()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.IsNull(target.Comparer);
        }

        [TestMethod]
        public void Culture_Is_Initially_Null()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.IsNull(target.Culture);
        }

        [TestMethod]
        public void GroupDescriptions_Returns_Null()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.IsNull(target.GroupDescriptions);
        }

        [TestMethod]
        public void Groups_Returns_Null()
        {
            int[] source = new[] { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            Assert.IsNull(target.Groups);
        }

        [TestMethod]
        public void Added_Item_Appears_Even_With_Basic_List()
        {
            List<int> source = new List<int> { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            source.Add(13);

            CollectionAssert.AreEqual(
                new[] { 1, 1, 2, 3, 5, 8, 13 },
                ((IEnumerable)target).Cast<object>().ToArray());
        }

        [TestMethod]
        public void Filter_Works()
        {
            List<int> source = new List<int> { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            target.Filter = (i) => (int)i >= 3;

            CollectionAssert.AreEqual(
                new[] { 3, 5, 8 },
                ((IEnumerable)target).Cast<object>().ToArray());
        }

        [TestMethod]
        public void Added_Item_Appears_With_Filter_Even_With_Basic_List()
        {
            List<int> source = new List<int> { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            target.Filter = (i) => (int)i >= 3;
            source.Add(13);

            CollectionAssert.AreEqual(
                new[] { 3, 5, 8, 13 },
                ((IEnumerable)target).Cast<object>().ToArray());
        }

        [TestMethod]
        public void Filter_Called_Every_Enumeration()
        {
            List<int> source = new List<int> { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);
            int invocationCount = 0;

            target.Filter = (i) =>
            {
                ++invocationCount;
                return (int)i >= 3;
            };

            invocationCount = 0;
            int[] foo = ((IEnumerable)target).Cast<int>().ToArray();

            Assert.AreEqual(6, invocationCount);

            int[] bar = ((IEnumerable)target).Cast<int>().ToArray();

            Assert.AreEqual(12, invocationCount);
        }

        [TestMethod]
        public void Count_Returns_Unfiltered_Result()
        {
            List<int> source = new List<int> { 1, 1, 2, 3, 5, 8 };
            CollectionView target = new CollectionView(source);

            target.Filter = (i) =>
            {
                return (int)i >= 3;
            };

            Assert.AreEqual(6, target.Count);
        }

        [TestMethod]
        public void Count_Works_Even_For_IEnumerable()
        {
            IEnumerable<int> source = Enumerable.Range(0, 10);
            CollectionView target = new CollectionView(source);

            Assert.AreEqual(10, target.Count);
        }

        [TestMethod]
        public void CurrentItem_Returns_Null_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.IsNull(target.CurrentItem);
        }

        [TestMethod]
        public void CurrentItem_Returns_First_Item()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            Assert.AreEqual(1, target.CurrentItem);
        }

        [TestMethod]
        public void CurrentItem_Returns_Null_For_Added_Item()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            source.Add(1);

            Assert.IsNull(target.CurrentItem);
        }

        [TestMethod]
        public void CurrentPosition_Returns_Minus1_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.AreEqual(-1, target.CurrentPosition);
        }

        [TestMethod]
        public void CurrentPosition_Returns_0()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            Assert.AreEqual(0, target.CurrentPosition);
        }

        [TestMethod]
        public void CurrentPosition_Returns_0_For_Added_Item()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            source.Add(1);

            Assert.AreEqual(0, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToFirst_Returns_False_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.MoveCurrentToFirst());
            Assert.IsNull(target.CurrentItem);
            Assert.AreEqual(-1, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToFirst_Returns_True_When_Item_Available()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.MoveCurrentToFirst());
            Assert.AreEqual(1, target.CurrentItem);
            Assert.AreEqual(0, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToLast_Returns_True_When_Item_Available()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.MoveCurrentToLast());
            Assert.AreEqual(2, target.CurrentItem);
            Assert.AreEqual(1, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToLast_Returns_False_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.MoveCurrentToLast());
            Assert.IsNull(target.CurrentItem);
            Assert.AreEqual(-1, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToNext_Returns_False_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.MoveCurrentToNext());
            Assert.IsNull(target.CurrentItem);
            Assert.AreEqual(-1, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToNext_Returns_True_When_Item_Available()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(2, target.CurrentItem);
            Assert.AreEqual(1, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToNext_Returns_True_When_Added_Item_Available()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            source.Add(1);

            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.AreEqual(1, target.CurrentItem);
            Assert.AreEqual(0, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToNext_Returns_False_When_No_Item_Available()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.MoveCurrentToNext());
            Assert.IsFalse(target.MoveCurrentToNext());
            Assert.AreEqual(null, target.CurrentItem);
            Assert.AreEqual(2, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToPosition_Returns_False_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.MoveCurrentToPosition(0));
            Assert.IsNull(target.CurrentItem);
            Assert.AreEqual(-1, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToPosition_Returns_True_When_Item_Available()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.MoveCurrentToPosition(1));
            Assert.AreEqual(2, target.CurrentItem);
            Assert.AreEqual(1, target.CurrentPosition);
        }

        [TestMethod]
        public void MoveCurrentToPosition_Returns_False_For_Minus_1_Index()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.MoveCurrentToPosition(-1));
            Assert.IsNull(target.CurrentItem);
            Assert.AreEqual(-1, target.CurrentPosition);
            Assert.IsTrue(target.IsCurrentBeforeFirst);
            Assert.IsFalse(target.IsCurrentAfterLast);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoveCurrentToPosition_Throws_Exception_For_Minus_2_Index()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            target.MoveCurrentToPosition(-2);
        }

        [TestMethod]
        public void MoveCurrentToPosition_Returns_False_For_After_End_Index()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.MoveCurrentToPosition(3));
            Assert.IsNull(target.CurrentItem);
            Assert.AreEqual(3, target.CurrentPosition);
            Assert.IsFalse(target.IsCurrentBeforeFirst);
            Assert.IsTrue(target.IsCurrentAfterLast);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MoveCurrentToPosition_Throws_Exception_For_Past_End_Index()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            target.MoveCurrentToPosition(4);
        }

        [TestMethod]
        public void MoveCurrentToPrevious_Can_Be_Called_Before_First()
        {
            List<int> source = new List<int> { 1, 2, 3 };
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.MoveCurrentToPrevious());
            Assert.AreEqual(-1, target.CurrentPosition);
            Assert.IsFalse(target.MoveCurrentToPrevious());
            Assert.AreEqual(-1, target.CurrentPosition);
        }

        [TestMethod]
        public void IsCurrentAfterLast_Returns_True_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.IsCurrentAfterLast);
        }

        [TestMethod]
        public void IsCurrentAfterLast_Returns_False_When_Before_Last()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.IsCurrentAfterLast);
        }

        [TestMethod]
        public void IsCurrentAfterLast_Returns_True_When_After_Last()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            target.MoveCurrentToLast();
            target.MoveCurrentToNext();

            Assert.IsTrue(target.IsCurrentAfterLast);
            Assert.AreEqual(2, target.CurrentPosition);
        }

        [TestMethod]
        public void IsCurrentBeforeFirst_Returns_True_For_Empty_Collection()
        {
            List<int> source = new List<int>();
            CollectionView target = new CollectionView(source);

            Assert.IsTrue(target.IsCurrentAfterLast);
        }

        [TestMethod]
        public void IsCurrentBeforeFirst_Returns_False_When_After_First()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            Assert.IsFalse(target.IsCurrentBeforeFirst);
        }

        [TestMethod]
        public void IsCurrentBeforeFirst_Returns_True_When_Before_First()
        {
            List<int> source = new List<int> { 1, 2 };
            CollectionView target = new CollectionView(source);

            target.MoveCurrentToPrevious();

            Assert.IsTrue(target.IsCurrentBeforeFirst);
            Assert.AreEqual(-1, target.CurrentPosition);
        }

        [TestMethod]
        public void CollectionView_Works_With_Null_Items()
        {
            List<object> source = new List<object> { null };
            CollectionView target = new CollectionView(source);

            Assert.AreEqual(0, target.CurrentPosition);
            Assert.IsNull(target.CurrentItem);
            Assert.IsFalse(target.IsCurrentAfterLast);
            Assert.IsFalse(target.IsCurrentBeforeFirst);
        }

        [TestMethod]
        public void CollectionChanged_Is_Raised_If_Source_Implements_INotifyCollectionChanged()
        {
            ObservableCollection<int> source = new ObservableCollection<int>();
            CollectionView target = new CollectionView(source);
            object sender = null;

            ((INotifyCollectionChanged)target).CollectionChanged += (s, e) => sender = s;
            source.Add(1);

            Assert.AreSame(sender, target);
        }

        [TestMethod]
        public void Calling_OnCollectionChanged_Doesnt_Call_ProcessCollectionChanged()
        {
            List<int> source = new List<int> { 1, 2 };
            TestCollectionView target = new TestCollectionView(source);

            target.CallOnCollectionChanged(
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            Assert.IsFalse(target.ProcessCollectionChangedCalled);
        }

        [TestMethod]
        public void Calling_OnCollectionChanged_Event_Handler_Calls_ProcessCollectionChanged()
        {
            List<int> source = new List<int> { 1, 2 };
            TestCollectionView target = new TestCollectionView(source);

            target.CallOnCollectionChanged(
                source, 
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            Assert.IsTrue(target.ProcessCollectionChangedCalled);
        }

        private class TestCollectionView : CollectionView
        {
            public TestCollectionView(IEnumerable collection)
                : base(collection)
            {
            }

            public bool ProcessCollectionChangedCalled { get; set; }

            public void CallOnCollectionChanged(NotifyCollectionChangedEventArgs args)
            {
                this.OnCollectionChanged(args);
            }

            public void CallOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
            {
                this.OnCollectionChanged(sender, args);
            }

            protected override void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args)
            {
                this.ProcessCollectionChangedCalled = true;
            }
        }
    }
}
