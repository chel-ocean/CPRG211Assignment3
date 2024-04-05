using Assignment3.Utility;
using Assignment3.ProblemDomain;
using System.Runtime.Serialization;
using System.Collections.Concurrent;
using System.Reflection.Metadata;
using NUnit.Framework.Constraints;
using Microsoft.VisualBasic;
using System.Diagnostics.CodeAnalysis;
using System.Data.Common;

namespace Assignment3.UnitTests
{
    public class LinkedListTest
    {
        ILinkedListADT users= new SLL(); // non empty list
        ILinkedListADT usersEmpty = new SLL(); // empty list

        [SetUp]
        public void Setup()
        {
            users.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            users.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            users.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            users.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));
        }

        [Test] // tests IsEmpty() on non empty list
        [TestCase(false)]
        public void Test_IsEmpty(bool expected)
        {
            bool actual = users.IsEmpty();
            Assert.AreEqual(expected, actual, "IsEmtpy failed.");
        }
        
        [Test]
        [TestCase(true)] // tests IsEmpty() on empty list
        public void Test_IsEmpty_Empty(bool expected)
        {
            bool actual = usersEmpty.IsEmpty();
            Assert.AreEqual(expected, actual, "IsEmpty failed.");
        }

        [Test] // tests AddFirst() on non empty list
        public void Test_AddFirst() // checks if number of nodes and new node values are correct
        {
            int[] expected = {0, users.Count() + 1}; 
            users.AddFirst(new User(0, "Chelsea", "chelsea@gmail.com", "123456"));
            int[] actual = {users.GetValue(0).Id, users.Count()};
            Assert.AreEqual(expected, actual, "AddFirst failed.");
        }

        [Test] // tests AddFirst() on empty list
        public void Test_AddFirst_Empty()
        {
            int[] expected = {0, usersEmpty.Count() + 1};
            usersEmpty.AddFirst(new User(0, "Chelsea", "chelsea@gmail.com", "123456"));
            int[] actual = {usersEmpty.GetValue(0).Id, usersEmpty.Count()};
            Assert.AreEqual(expected, actual, "AddFirst failed.");
        }

        [Test] // tests AddLast() on non empty list
        public void Test_AddLast() // checks if number of nodes and new node values are correct
        {
            int[] expected = {0, users.Count() + 1};
            users.AddLast(new User(0, "Chelsea", "chelsea@gmail.com", "123456"));
            int[] actual = {users.GetValue(users.Count() - 1).Id, users.Count()};
            Assert.AreEqual(expected, actual, "Add at index failed.");
        }

        [Test] // tests AddLast() on empty list
        public void Test_AddLast_Empty() 
        {
            int[] expected = {0, usersEmpty.Count() + 1};
            usersEmpty.AddLast(new User(0, "Chelsea", "chelsea@gmail.com", "123456"));
            int[] actual = {usersEmpty.GetValue(0).Id, usersEmpty.Count()};
            Assert.AreEqual(expected, actual, "Add at index failed.");
        }

        [Test] // tests Add(User value, int index) on non empty list
        [TestCase(0)] // add to beginning
        [TestCase(2)] // add to index 2
        public void Test_Add(int index) // checks if number of nodes and new node values are correct
        {
            int[] expected = {0, users.Count() + 1};
            users.Add(new User(0, "Chelsea", "chelsea@gmail.com", "123456"),index);
            int[] actual = {users.GetValue(index).Id, users.Count()};
            Assert.AreEqual(expected, actual, "Add at index failed.");
        }

        [Test] // tests Add(User value, int index) exceptions on non empty list
        [TestCase(-1)] // add to invalid index
        [TestCase(10)] // add to invalid index
        public void Test_Add_Invalid(int index)
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => users.Add(new User(0, "Chelsea", "chelsea@gmail.com", "123456"), index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }

        [Test] // tests Add(User value, int index) exceptions on empty list
        [TestCase(0)]
        [TestCase(1)]
        public void Test_Add_Empty(int index)
        {   
            var ex = Assert.Throws<IndexOutOfRangeException>(() => usersEmpty.Add(new User(0, "Chelsea", "chelsea@gmail.com", "123456"), index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }

        [Test] // tests Replace(User value int index) on non empty list
        [TestCase(0)] // replace first node
        [TestCase(2)] // replace third node
        [TestCase(3)] // replace last node
        public void Test_Replace(int index)
        {
            int[] expected = {0, users.Count()};
            users.Replace(new User(0, "Chelsea", "chelsea@gmail.com", "123456"), index);
            int[] actual = {users.GetValue(index).Id, users.Count()};
            Assert.AreEqual(expected, actual, "Replace at index failed.");
        }

        [Test] // tests Replace(User value int index) exceptions on non empty list
        [TestCase(-1)] // add to invalid index
        [TestCase(4)] // add to invalid index
        public void Test_Replace_Invalid(int index)
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => users.Replace(new User(0, "Chelsea", "chelsea@gmail.com", "123456"), index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }

        [Test] // tests Replace(User value int index) exceptions on empty list
        [TestCase(0)] // add to empty list
        public void Test_Replace_Empty(int index)
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => usersEmpty.Replace(new User(0, "Chelsea", "chelsea@gmail.com", "123456"), index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }
        
        [Test] // tests RemoveFirst() on non empty list
        public void Test_RemoveFirst()
        {
            int[] expected = {2, users.Count() - 1};
            users.RemoveFirst();
            int[] actual = {users.GetValue(0).Id, users.Count()};
            Assert.AreEqual(expected, actual, "RemoveFirst failed.");
        }

        [Test] // tests RemoveFirst() on empty list
        public void Test_RemoveFirst_Empty()
        {
            var ex = Assert.Throws<CannotRemoveException>(() => usersEmpty.RemoveFirst());
            Assert.That(ex.Message, Is.EqualTo("The list is empty."));
        }
        
        [Test] // tests RemoveLast() on non empty list
        public void Test_RemoveLast()
        {
            int[] expected = {3, users.Count() - 1};
            users.RemoveLast();
            int[] actual = {users.GetValue(users.Count() - 1).Id, users.Count()};
            Assert.AreEqual(expected, actual, "RemoveLast failed.");
        }

        [Test] // tests RemoveLast() on empty list
        public void Test_RemoveLast_Empty()
        {
            var ex = Assert.Throws<CannotRemoveException>(() => usersEmpty.RemoveLast());
            Assert.That(ex.Message, Is.EqualTo("The list is empty."));
        }

        [Test] // tests Remove(int index) on non empty list
        [TestCase(0, 2)] // remove first node
        [TestCase(1, 3)] // remove second node
        public void Test_Remove(int index, int? expectedId)
        {
            int[] expected = {(int)expectedId, users.Count() - 1};
            users.Remove(index);
            int[] actual = {users.GetValue(index).Id, users.Count()};
            Assert.AreEqual(expected, actual, "Remove at index failed.");
        }

        [Test] // tests Remove(int index) exceptions on non empty list
        [TestCase(-1)] // remove invalid index
        [TestCase(3)] // remove last node
        [TestCase(4)] // remove invalid index
        public void Test_Remove_Invalid(int index)
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => users.Remove(index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }

        [Test] // tests Remove(int index) exception on empty list
        [TestCase(0)]
        public void Test_Remove_Empty(int index)
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => usersEmpty.Remove(index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }

        [Test] // tests GetValue(int index) on non empty list
        [TestCase(0, 1)]
        public void Test_GetValue(int index, int id)
        {
            int expected = id;
            int actual = users.GetValue(index).Id;
            Assert.AreEqual(expected, actual, "GetValue failed.");
        }

        [Test] // tests GetValue(int index) exceptions on non empty list
        [TestCase(-1)]
        [TestCase(5)]
        public void Test_GetValue_Invalid(int index)
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => users.GetValue(index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }

        [Test] // tests GetValue(int index) exceptions on empty list
        [TestCase(0)]
        public void Test_GetValue_Empty(int index)
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => usersEmpty.GetValue(index));
            Assert.That(ex.Message, Is.EqualTo("Index is out of range."));
        }

        [Test] // tests Reverse() on non empty list
        public void Test_Reserve() // checks first ID of original list and last ID of reversed list
        {
            int expected = users.GetValue(0).Id;
            SLL newll = users.Reverse();
            int actual = newll.GetValue(newll.Count() - 1).Id;
        }

        [TearDown]
        public void Cleanup()
        {
            users.Clear();
            usersEmpty.Clear();
        }
    }
}