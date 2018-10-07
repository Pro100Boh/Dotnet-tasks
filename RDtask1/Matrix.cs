using System;

namespace RDtask1
{

    [Serializable]
    public class MatrixDimensionException : ApplicationException
    {
        public MatrixDimensionException() { }
        public MatrixDimensionException(string message) : base(message) { }
        public MatrixDimensionException(string message, Exception inner) : base(message, inner) { }
        protected MatrixDimensionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class MatrixIndexOutOfRangeException : ApplicationException
    {
        public MatrixIndexOutOfRangeException() { }
        public MatrixIndexOutOfRangeException(string message) : base(message) { }
        public MatrixIndexOutOfRangeException(string message, Exception inner) : base(message, inner) { }
        protected MatrixIndexOutOfRangeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class Matrix : ICloneable
    {
        private int[,] items;

        public int Rows { get; }

        public int Cols { get; }

        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
                throw new MatrixIndexOutOfRangeException($"Unable to create matrix with [{rows}][{cols}] dimensions");

            items = new int[rows, cols];

            Rows = rows;
            Cols = cols;
        }

        public Matrix(int[,] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            this.items = (int[,])items.Clone();

            Rows = items.GetLength(0);
            Cols = items.GetLength(1);
        }

        private bool IsCorrectDimensions(int i, int j) => i >= 0 && i < Rows && j >= 0 && j < Cols;

        public int this[int i, int j]
        {
            get
            {
                if (!IsCorrectDimensions(i, j))
                    throw new MatrixIndexOutOfRangeException($"No such element with indexes [{i}][{j}]");

                return items[i, j];
            }
            set
            {
                if (!IsCorrectDimensions(i, j))
                    throw new MatrixIndexOutOfRangeException($"No such element with indexes [{i}][{j}]");

                items[i, j] = value;
            }
        }

        private bool IsEqualDimensions(Matrix other) => Rows == other.Rows && Cols == other.Cols;

        public Matrix Add(Matrix b)
        {
            if (b == null)
                throw new ArgumentNullException(nameof(b));

            if (!IsEqualDimensions(b))
                throw new MatrixDimensionException($"Unable to add matrices with different dimensions [{Rows}][{Cols}], [{b.Rows}][{b.Cols}]");

            Matrix c = (Matrix)Clone();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    c.items[i, j] += b.items[i, j];
                }
            }

            return c;
        }

        public Matrix Subtract(Matrix b)
        {
            if (b == null)
                throw new ArgumentNullException(nameof(b));

            if (!IsEqualDimensions(b))
                throw new MatrixDimensionException($"Unable to subtract matrices with different dimensions [{Rows}][{Cols}], [{b.Rows}][{b.Cols}]");

            Matrix c = (Matrix)Clone();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    c.items[i, j] -= b.items[i, j];
                }
            }

            return c;
        }

        public Matrix Multiply(Matrix b)
        {
            if (b == null)
                throw new ArgumentNullException(nameof(b));

            if (Cols != b.Rows)
                throw new MatrixDimensionException($"Unable to multiply matrices - j dimension of first matrix ({Cols}) not equals to i dimension of second matrix ({b.Rows})");

            Matrix c = new Matrix(Rows, b.Cols);

            for (int i = 0; i < c.Rows; i++)
            {
                for (int j = 0; j < c.Cols; j++)
                {
                    int element = 0;
                    for (int k = 0; k < Cols; k++)
                    {
                        element += items[i, k] * b.items[k, j];
                    }
                    c.items[i, j] = element;
                }
            }

            return c;
        }

        public override bool Equals(object obj)
        {
            Matrix b = obj as Matrix;

            if (obj == null)
                return false;

            if (Rows != b.Rows || Cols != b.Cols)
                return false;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (items[i, j] != b.items[i, j])
                        return false;
                }
            }

            return true;
        }

        public static Matrix operator +(Matrix a, Matrix b) => a.Add(b);

        public static Matrix operator -(Matrix a, Matrix b) => a.Subtract(b);

        public static Matrix operator *(Matrix a, Matrix b) => a.Multiply(b);

        public static bool operator ==(Matrix a, Matrix b) => a.Equals(b);

        public static bool operator !=(Matrix a, Matrix b) => !a.Equals(b);

        public override int GetHashCode() => items.GetHashCode();

        public object Clone() => new Matrix(items);

        public int[,] ToArray() => (int[,])items.Clone();
    }
}
