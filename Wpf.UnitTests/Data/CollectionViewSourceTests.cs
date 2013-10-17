namespace Avalonia.UnitTests.Data
{
#if TEST_WPF
    using System.Windows.Data;
#else
    using Avalonia.Data;
#endif

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CollectionViewSourceTests
    {
        [TestMethod]
        public void GetDefaultView_Returns_Null_For_Null()
        {
            ICollectionView result = CollectionViewSource.GetDefaultView(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetDefaultView_Returns_Null_For_Non_Enumerable()
        {
            ICollectionView result = CollectionViewSource.GetDefaultView(6);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetDefaultView_Returns_ListCollectionView_For_Array()
        {
            ICollectionView result = CollectionViewSource.GetDefaultView(new int[0]);
            Assert.IsInstanceOfType(result, typeof(ListCollectionView));
        }

        [TestMethod]
        public void GetDefaultView_Returns_ListCollectionView_For_List()
        {
            ICollectionView result = CollectionViewSource.GetDefaultView(new List<int>());
            Assert.IsInstanceOfType(result, typeof(ListCollectionView));
        }

        [TestMethod]
        public void GetDefaultView_Returns_ListCollectionView_For_ArrayList()
        {
            ICollectionView result = CollectionViewSource.GetDefaultView(new ArrayList());
            Assert.IsInstanceOfType(result, typeof(ListCollectionView));
        }

        [TestMethod]
        public void GetDefaultView_Returns_EnumerableCollectionView_For_IEnumerable()
        {
            ICollectionView result = CollectionViewSource.GetDefaultView(Enumerable.Repeat(1, 1));

            // For some reason EnumerableCollectionView is internal.
            Assert.AreEqual("EnumerableCollectionView", result.GetType().Name);
        }

        [TestMethod]
        public void GetDefaultView_Returns_ListCollectionView_For_Dictionary()
        {
            // Dictionary<> implements ICollection<> but not IList<>
            ICollectionView result = CollectionViewSource.GetDefaultView(new Dictionary<int, int>());
            Assert.AreEqual("EnumerableCollectionView", result.GetType().Name);
        }

        [TestMethod]
        public void GetDefaultView_Returns_ListCollectionView_For_ObservableCollection()
        {
            ICollectionView result = CollectionViewSource.GetDefaultView(new ObservableCollection<int>());
            Assert.IsInstanceOfType(result, typeof(ListCollectionView));
        }

        [TestMethod]
        public void GetDefaultView_Returns_Same_CollectionView_For_Same_Collection()
        {
            List<int> list = new List<int>();
            ICollectionView result1 = CollectionViewSource.GetDefaultView(list);
            ICollectionView result2 = CollectionViewSource.GetDefaultView((IEnumerable)list);
            Assert.AreSame(result1, result2);
        }
    }
}
