using NUnit.Framework;
using System;

namespace RDtask2.UnitTests
{
    public abstract class CustomRangeArrayTests<T>
    {
        protected abstract T Item0 { get; }
        protected abstract T Item1 { get; }
        protected abstract T Item2 { get; }
        protected abstract T Item3 { get; }
        protected abstract T Item4 { get; }

        protected abstract T AdditionalItem { get; }

        protected CustomRangeArray<T> MakeCustomRangeArrayBoundariesFrom10To14() =>
            new CustomRangeArray<T>(10, 14);

        protected CustomRangeArray<T> MakeCustomRangeArrayBoundariesFrom10To14WithItems()
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14();
            arr[10] = Item0;
            arr[11] = Item1;
            arr[12] = Item2;
            arr[13] = Item3;
            arr[14] = Item4;
            return arr;
        }

        [Test]
        public void Construcotr_RightBoundarySmallerThanLeftBoundary_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(()=> new CustomRangeArray<T>(10, 5));
        }

        [Test]
        public void Construcotr_CorrectBoundaries_NotThrowsException()
        {
            Assert.DoesNotThrow(() => new CustomRangeArray<T>(5, 10));
        }

        [TestCase(9)]
        [TestCase(15)]
        public void IndexerGet_IndexOutOfRange_TrowsIndexOutOfRangeException(int index)
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14();
            T element;
            Assert.Throws<IndexOutOfRangeException>(()=> element = arr[index]);
        }

        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void IndexerGet_CorrectIndex_DoesNotTrowsIndexOutOfRangeException(int index)
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14();
            T element;
            Assert.DoesNotThrow(() => element = arr[index]);
        }

        [TestCase(9)]
        [TestCase(15)]
        public void IndexerSet_IndexOutOfRange_TrowsIndexOutOfRangeException(int index)
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14();
            Assert.Throws<IndexOutOfRangeException>(() => arr[index] = default);
        }

        [TestCase(10)]
        [TestCase(12)]
        [TestCase(14)]
        public void IndexerSet_CorrectIndex_DoesNotTrowsIndexOutOfRangeException(int index)
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14();
            Assert.DoesNotThrow(() => arr[index] = default);
        }

        public void Indexer_Index10_CorrectElement()
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            Assert.AreEqual(arr[10], Item0);
        }

        public void Indexer_Index12_CorrectElement()
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            Assert.AreEqual(arr[12], Item2);
        }

        public void Indexer_Index14_CorrectElement()
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            Assert.AreEqual(arr[14], Item4);
        }

        [Test]
        public void EnumerationOfElements_EqualsToArrayOfSameElements_True()
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14WithItems();

            var expected = new T[] {Item0, Item1, Item2, Item3, Item4 };
            CollectionAssert.AreEqual(expected, arr);
        }

        [Test]
        public void Equals_SameArrays_True()
        {
            var arr0 = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            var arr1 = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            Assert.AreEqual(arr0 , arr1);
        }

        [Test]
        public void Equals_NotSameArrays_False()
        {
            var arr0 = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            var arr1 = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            arr1[12] = AdditionalItem;
            Assert.AreNotEqual(arr0, arr1);
        }

        [Test]
        public void Clone_OriginalArrayEqualsToClone_True()
        {
            var arr = MakeCustomRangeArrayBoundariesFrom10To14WithItems();
            Assert.AreEqual(arr, arr.Clone());
        }

    }

    [TestFixture]
    public class CustomRangeArrayOfIntegersTests : CustomRangeArrayTests<int>
    {
        protected override int Item0 => 5;

        protected override int Item1 => 10;

        protected override int Item2 => 0;

        protected override int Item3 => -3;

        protected override int Item4 => 12;

        protected override int AdditionalItem => 20;
    }

    [TestFixture]
    public class CustomRangeArrayOfStringsTests : CustomRangeArrayTests<string>
    {
        protected override string Item0 => "abc";

        protected override string Item1 => "qwerty";

        protected override string Item2 => "1234";

        protected override string Item3 => "1345";

        protected override string Item4 => "hello";

        protected override string AdditionalItem => "test";
    }
}
