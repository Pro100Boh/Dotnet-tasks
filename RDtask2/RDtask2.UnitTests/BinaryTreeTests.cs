using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace RDtask2.UnitTests
{
    /*
    Принцип именования [Тестирующийся метод]_[Сценарий]_[Ожидаемое поведение]
    Примеры: 
    Sum_10plus20_30returned
    GetPasswordStrength_AllCahrs_5Points
    */

    class ReverseComparer : IComparer
    {
        public int Compare(object x, object y) => ((IComparable)y).CompareTo(x);
    }

    public abstract class BinaryTreeTests<T> where T : IComparable<T>
    {
        protected abstract T Item0 { get; }
        protected abstract T Item1 { get; }
        protected abstract T Item2 { get; }
        protected abstract T Item3 { get; }
        protected abstract T Item4 { get; }

        protected abstract T AdditionalItem0 { get; }
        protected abstract T AdditionalItem1 { get; }

        protected BinaryTree<T> sut;

        protected BinaryTree<T> MakeBinaryTreeWith5Elements() =>
            new BinaryTree<T>() { Item0, Item1, Item2, Item3, Item4 };

        protected BinaryTree<T> MakeBinaryTreeWith4Elements() =>
            new BinaryTree<T>() { Item0, Item1, Item2, Item3 };


        [SetUp]
        public void SetUP()
        {
            sut = MakeBinaryTreeWith5Elements();
        }

        [Test]
        public void Equality_SameCollections_Equals()
        {
            var expected = MakeBinaryTreeWith5Elements();
            CollectionAssert.AreEqual(expected, sut);
        }

        [Test]
        public void Equality_NotSameCollections_NotEquals()
        {
            var expected = MakeBinaryTreeWith4Elements();
            CollectionAssert.AreNotEqual(expected, sut);
        }

        [Test]
        public void Equivalent_SameCollectionsWithAnotherOrder_Equivalents()
        {
            var expected = sut.Reverse();
            CollectionAssert.AreEquivalent(expected, sut);
        }

        [Test]
        public void Equivalent_NotSameCollections_NotEquivalents()
        {
            var expected = MakeBinaryTreeWith4Elements();
            CollectionAssert.AreNotEquivalent(expected, sut);
        }

        [Test]
        public void Count_CollectionWtith5ElementsConut5_True()
        {
            Assert.AreEqual(sut.Count, 5);
        }

        [Test]
        public void Conut_NewCollectionConut0_True()
        {
            Assert.AreEqual(new BinaryTree<T>().Count, 0);
        }

        [Test]
        public void Conut_CollectionAfterAddedItemCount6_True()
        {
            sut.Add(AdditionalItem0);
            Assert.AreEqual(sut.Count, 6);
        }

        [Test]
        public void Conut_CollectionAfterRemovedItemCount4_True()
        {
            sut.Remove(Item0);
            Assert.AreEqual(sut.Count, 4);
        }

        [Test]
        public void Contains_Item0_True()
        {
            CollectionAssert.Contains(sut, Item0);
        }

        [Test]
        public void Contains_Item2_True()
        {
            CollectionAssert.Contains(sut, Item2);
        }

        [Test]
        public void Contains_Item4_True()
        {
            CollectionAssert.Contains(sut, Item4);
        }

        [Test]
        public void Contains_ItemNotInCollection_False()
        {
            CollectionAssert.DoesNotContain(sut, AdditionalItem0);
        }

        public void Remove_Item0_CollectionNotContain()
        {
            sut.Remove(Item0);
            CollectionAssert.DoesNotContain(sut, Item0);
        }

        public void Remove_Item0_ReturnsTrue()
        {
            Assert.IsTrue(sut.Remove(Item0));
        }

        public void Remove_ItemNotInCollection_ReturnsFalse()
        {
            Assert.IsFalse(sut.Remove(AdditionalItem0));
        }

        [Test]
        public void IsEmpty_NewCollection_True()
        {
            CollectionAssert.IsEmpty(new BinaryTree<T>());
        }

        [Test]
        public void IsEmpty_ClearedCollection_True()
        {
            sut.Clear();
            CollectionAssert.IsEmpty(sut);
        }

        [Test]
        public void IsEmpty_CollectionWithElements_False()
        {
            CollectionAssert.IsNotEmpty(sut);
        }

        [Test]
        public void EnumerationOfElements_IsOrdered_True()
        {
            CollectionAssert.IsOrdered(sut);
        }

        [Test]
        public void ReverseEnumerationOfElements_IsReverseOrdered_True()
        {
            CollectionAssert.IsOrdered(sut.Reverse(), new ReverseComparer());
        }

        [Test]
        public void EnumerationOfElements_IsAllItemsUnique_True()
        {
            CollectionAssert.AllItemsAreUnique(sut);
        }

        [Test]
        public void Add_DuplicateItem_TrowsDuplicateItemException()
        {
            Assert.Throws<DuplicateItemException>(() => sut.Add(Item0));
        }

        [Test]
        public void EventAdded_TwoItemsAdded_TwoCorrectEventsRaised()
        {
            var receivedEventsItems = new List<T>();

            sut.Added += delegate (object sender, BinaryTreeItemAddedEventArgs<T> e)
            {
                receivedEventsItems.Add(e.Item);
            };

            sut.Add(AdditionalItem0);
            sut.Add(AdditionalItem1);

            Assert.AreEqual(2, receivedEventsItems.Count);
            Assert.AreEqual(AdditionalItem0, receivedEventsItems[0]);
            Assert.AreEqual(AdditionalItem1, receivedEventsItems[1]);
        }

        [Test]
        public void EventRemoved_TwoItemsRemoved_TwoCorrectEventsRaised()
        {
            var receivedEventsItems = new List<T>();

            sut.Removed += delegate (object sender, BinaryTreeItemRemovedEventArgs<T> e)
            {
                receivedEventsItems.Add(e.Item);
            };

            sut.Remove(Item0);
            sut.Remove(Item4);
            sut.Remove(AdditionalItem0);

            Assert.AreEqual(2, receivedEventsItems.Count);
            Assert.AreEqual(Item0, receivedEventsItems[0]);
            Assert.AreEqual(Item4, receivedEventsItems[1]);
        }

        [Test]
        public void EventCleared_ClearedBinaryTree_EventRaised()
        {
            int receivedEventsCount = 0;

            sut.Cleared += delegate (object sender, BinaryTreeClearedEventArgs e)
            {
                receivedEventsCount++;
            };

            sut.Clear();

            Assert.AreEqual(1, receivedEventsCount);
        }

    }

    [TestFixture]
    public class BinaryTreeOfItegersTests : BinaryTreeTests<int>
    {
        protected override int Item0 => 5;

        protected override int Item1 => 10;

        protected override int Item2 => 0;

        protected override int Item3 => -3;

        protected override int Item4 => 12;

        protected override int AdditionalItem0 => 20;

        protected override int AdditionalItem1 => -7;
    }

    [TestFixture]
    public class BinaryTreeOfStringsTests : BinaryTreeTests<string>
    {
        protected override string Item0 => "abc";

        protected override string Item1 => "qwerty";

        protected override string Item2 => "1234";

        protected override string Item3 => "1345";

        protected override string Item4 => "hello";

        protected override string AdditionalItem0 => "test0";

        protected override string AdditionalItem1 => "test1";
    }

}
