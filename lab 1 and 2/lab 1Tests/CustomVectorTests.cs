using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomCollection;
using System;
using System.Collections.Generic;

namespace CustomCollection.Tests
{
    [TestClass()]
    public class CustomVectorTests
    {
        [TestMethod()]
        public void CustomVectorTest()
        {
            CustomVector<int> vector = new CustomVector<int>();
            Assert.IsTrue(vector.Count == 0);

            List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            vector = new CustomVector<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            Assert.IsTrue(vector.Count == list.Count);
            Assert.IsTrue(list.Capacity == vector.Capacity);


            int capacity = 50;
            vector = new CustomVector<int>(capacity);
            list = new List<int>(capacity);
            Assert.IsTrue(vector.Capacity == list.Capacity);
            Assert.IsTrue(vector.Count == list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                if (!Equals(vector[i], list[i]))
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            List<int> check = new List<int>() { 2, 6, 8, 5, 5, 1, 8, 5, 3, 5 };

            CustomVector<int> vector = new CustomVector<int>();
            vector.Add(2);
            vector.Add(6);
            vector.Add(8);
            vector.Add(5);
            vector.Add(5);
            vector.Add(1);
            vector.Add(8);
            vector.Add(5);
            vector.Add(3);
            vector.Add(5);
            for (int i = 0; i < check.Count; i++)
            {
                if (!Equals(vector[i], check[i]))
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void ClearTest()
        {
            CustomVector<int> vector = new CustomVector<int>() { 2, 8, 5, 1, 8, 50, 5, 60, 5 };
            List<int> check = new List<int>() { 2, 8, 5, 1, 8, 50, 5, 60, 5 };

            vector.Clear();
            check.Clear();
            Assert.AreEqual(0, vector.Count);
            for (int i = 0; i < check.Count; i++)
            {
                if (!Equals(vector[i], check[i]))
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void ContainsTest()
        {
            CustomVector<int> vector = new CustomVector<int>() { 2, 8, 5, 1, 8, 50, 5, 60, 5 };
            Assert.IsTrue(vector.Contains(1));

            Assert.IsTrue(vector.Contains(2));

            Assert.IsFalse(vector.Contains(4));

            vector.Add(4);
            Assert.IsTrue(vector.Contains(4));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            List<int> check = new List<int>() { 2, 8, 5, 5, 1, 8, 5, 5 };
            CustomVector<int> vector = new CustomVector<int>() { 2, 6, 8, 5, 5, 1, 8, 5, 3, 5 };
            vector.Remove(3);
            vector.Remove(7);
            vector.Remove(6);
            for (int i = 0; i < check.Count; i++)
            {
                if (!Equals(vector[i], check[i]))
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            List<int> check = new List<int>() { 2, 8, 5, 1, 8, 50, 5, 60, 5 };
            CustomVector<int> vector = new CustomVector<int>() { 0, 2, 8, 5, 5, 1, 8, 50, 5, 60, 5, 70 };
            vector.RemoveAt(4);
            vector.RemoveAt(0);
            vector.RemoveAt(vector.Count - 1);
            for (int i = 0; i < check.Count; i++)
            {
                if (!Equals(vector[i], check[i]))
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void RemoveAtTestFail()
        {
            CustomVector<int> vector = new CustomVector<int>() { 2, 8, 5, 1, 8, 50, 5, 60, 5 };
            try
            {
                vector.RemoveAt(vector.Count);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException) { }
        }

        [TestMethod()]
        public void InsertTest()
        {
            List<int> check = new List<int>() { 0, 2, 8, 5, 5, 1, 8, 50, 5, 60, 5, 70 };
            CustomVector<int> vector = new CustomVector<int>() { 2, 8, 5, 5, 1, 8, 5, 5 };
            vector.Insert(6, 50);
            vector.Insert(0, 0);
            vector.Insert(vector.Count - 1, 60);
            vector.Insert(vector.Count, 70);
            for (int i = 0; i < check.Count; i++)
            {
                if (!Equals(vector[i], check[i]))
                    Assert.Fail();
            }
        }
        [TestMethod()]
        public void InsertTestFail()
        {
            CustomVector<int> vector = new CustomVector<int>() { 0, 2, 8, 5, 5, 1, 8, 50, 5, 60, 5, 70 };
            try
            {
                vector.Insert(vector.Count + 1, -1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException) { }
        }
        [TestMethod()]
        public void IndexOfTest()
        {
            List<string> check = new List<string>() { "A", "AB", "ABC", "ABCD", "ABCDE", null, "ABCDEF", "ABCDEFG", " ", "ABCDEFGH", "ABCDEFGHI" };
            CustomVector<string> vector = new CustomVector<string>() { "A", "AB", "ABC", "ABCD", "ABCDE", null, "ABCDEF", "ABCDEFG", " ", "ABCDEFGH", "ABCDEFGHI" };

            for (int i = 0; i < check.Count; i++)
            {
                Assert.IsTrue(vector.IndexOf(check[i]) == i);
            }

            Assert.IsTrue(vector.IndexOf(" ") == check.IndexOf(" "));
        }

        [TestMethod()]
        public void CopyToTest()
        {
            CustomVector<string> vector = new CustomVector<string>() { "A", "AB", "ABC", "ABCDE", null, "ABCDEF", "ABCDEFG", " ", "ABCDEFGHI" };
            string[] check = new string[vector.Count];
            vector.CopyTo(check, 0);
            for (int i = 0; i < check.Length; i++)
            {
                if (!Equals(vector[i], check[i]))
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            List<string> check = new List<string>() { "", "hjchy", "ek", "32", null, "%$", "(PL::L" };
            CustomVector<string> vector = new CustomVector<string>() { "", "hjchy", "ek", "32", null, "%$", "(PL::L" };

            int index = 0;

            foreach (var item in vector)
            {
                Assert.AreEqual(item, check[index++]);
            }
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            List<string> check = new List<string>() { "", null, "1" };
            CustomVector<string> vector = new CustomVector<string>() { "", null, "1" };

            vector.AddRange(new string[] { "AB", "dh", "ef" });
            check.AddRange(new string[] { "AB", "dh", "ef" });

            for (int i = 0; i < check.Count; i++)
            {
                if (!Equals(vector[i], check[i]))
                    Assert.Fail();
            }
        }

        [TestMethod()]
        public void ToArrayTest()
        {
            List<string> check = new List<string>() { "A", "AB", "ABC", "ABCD", "ABCDE", "ABCDEF", "ABCDEFG", " ", "ABCDEFGH", "ABCDEFGHI" };
            CustomVector<string> vector = new CustomVector<string>() { "A", "AB", "ABC", "ABCD", "ABCDE", "ABCDEF", "ABCDEFG", " ", "ABCDEFGH", "ABCDEFGHI" };

            var vectorArray = vector.ToArray();
            var checkArray = check.ToArray();

            for (int i = 0; i < checkArray.Length; i++)
            {
                if (!Equals(vectorArray[i], checkArray[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(vectorArray.Length == checkArray.Length);
        }

    }
}