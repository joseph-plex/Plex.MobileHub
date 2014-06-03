using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plex.MobileHub.Data;

namespace Plex.MobileHub.Data.Tests
{
    [TestClass]
    public class IListExtensionsTest
    {
        [TestMethod]
        public void FindIndexTest1()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(p => p == 2);
            Assert.AreEqual(1, index);
        }
        [TestMethod]
        public void FindIndexTest2()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(1, p => p == 2);
            Assert.AreEqual(1, index);
        }

        [TestMethod]
        public void FindIndexTest3()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(1, 1, p => p == 2);
            Assert.AreEqual(1, index);
        }

        [TestMethod]
        public void FindIndexTest4()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(p => p == 4);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void FindIndexTest5()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(1, p => p == 4);
            Assert.AreEqual(-1, index);
        }

        [TestMethod]
        public void FindIndexTest6()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(1, 1, p => p == 4);
            Assert.AreEqual(-1, index);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FindIndexTest7()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(-3, p => p == 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FindIndexTest8()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            int index = list.FindIndex(1, 4, p => p == 4);
        }

        [TestMethod]
        public void AddRange1()
        {
            IList<Int32> list = new List<Int32>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            IList<Int32> list2 = new List<Int32>();
            list2.AddRange(list);

            foreach (var i in list2)
                Assert.AreEqual(true, list.Contains(i));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRange2()
        {
            IList<Int32> list = new List<Int32>();
            list.AddRange(null);
        }
    }
}
