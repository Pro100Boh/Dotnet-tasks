using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RDtask2
{
    public class CustomRangeArray<T> : IEnumerable<T>, ICloneable
    {
        private T[] array;

        public int FirstIndex { get; }

        public int LastIndex { get; }

        public int Length { get; }

        public CustomRangeArray(int firstIndex, int lastIndex)
        {
            if (lastIndex < firstIndex)
                throw new ArgumentException($"Last index cannot be less that first index: {lastIndex} < {firstIndex}");

            Length = lastIndex - firstIndex + 1;
            array = new T[Length];
            FirstIndex = firstIndex;
            LastIndex = lastIndex;
        }

        private bool IsCorrectIndex(int i) => i >= FirstIndex && i <= LastIndex;

        public T this[int i]
        {
            get
            {
                if (!IsCorrectIndex(i))
                    throw new IndexOutOfRangeException();

                return array[i - FirstIndex];
            }
            set
            {
                if (!IsCorrectIndex(i))
                    throw new IndexOutOfRangeException();

                array[i - FirstIndex] = value;
            }
        }

        public T[] ToArray() => (T[])array.Clone();

        public IEnumerator<T> GetEnumerator() => array.Cast<T>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();

        public object Clone()
        {
            var cloneArray = new CustomRangeArray<T>(FirstIndex, LastIndex);

            Array.Copy(array, cloneArray.array, Length);

            return cloneArray;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CustomRangeArray<T> other))
                return false;

            if (FirstIndex != other.FirstIndex || LastIndex != other.LastIndex)
                return false;

            for (int i = 0; i <= Length; i++)
            {
                if (!array[i].Equals(other.array[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode() => array.GetHashCode();
    }
}
