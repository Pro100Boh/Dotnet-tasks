using System;

namespace RDtask1
{

    [Serializable]
    public class NegativePolymonialExponentException : Exception
    {
        public NegativePolymonialExponentException() { }
        public NegativePolymonialExponentException(string message) : base(message) { }
        public NegativePolymonialExponentException(string message, Exception inner) : base(message, inner) { }
        protected NegativePolymonialExponentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class Polynomial
    {
        private double[] coef;

        public int Degree { get; private set; }

        public Polynomial(double leadingСoef, int exponent)
        {
            if (exponent < 0)
                throw new NegativePolymonialExponentException($"Exponent of polynomial can`t be negative: {exponent}");
            
            coef = new double[exponent + 1];
            coef[exponent] = leadingСoef;

            Degree = exponent;
        }

        public double this[int exponent]
        {
            get
            {
                if (exponent < 0)
                    throw new NegativePolymonialExponentException($"Exponent of polynomial can`t be negative: {exponent}");

                if (exponent > Degree)
                    return 0.0;

                return coef[exponent];
            }
            set
            {
                if (exponent < 0)
                    throw new NegativePolymonialExponentException($"Exponent of polynomial can`t be negative: {exponent}");

                if (exponent > Degree)
                {
                    var newCoef = new double[coef.Length + 1];
                    Array.Copy(coef, newCoef, coef.Length);

                    coef = newCoef;
                    Degree = exponent;
                }

                coef[exponent] = value;
                UpdateDegree();
            }
        }

        private void UpdateDegree()
        {
            Degree = 0;
            for (int i = coef.Length - 1; i >= 0; i--)
            {
                if (coef[i] != 0)
                {
                    Degree = i;

                    if (i < coef.Length - 1)
                    {
                        int newLength = i + 1;
                        var newCoef = new double[newLength];
                        Array.Copy(coef, newCoef, newLength);
                        coef = newCoef;
                    }
                    return;
                }
            }
        }

        public double GetValue(double x)
        {
            double result = 0;

            for (int i = Degree; i >= 0; i--)
                result += coef[i] * Math.Pow(x, i);

            return result;
        }

        public Polynomial Add(Polynomial other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            Polynomial result = new Polynomial(0, Math.Max(Degree, other.Degree));

            for (int i = 0; i <= Degree; i++)
                result.coef[i] += coef[i];

            for (int i = 0; i <= other.Degree; i++)
                result.coef[i] += other.coef[i];

            result.UpdateDegree();

            return result;
        }

        public Polynomial Subtract(Polynomial other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            Polynomial result = new Polynomial(0, Math.Max(Degree, other.Degree));

            for (int i = 0; i <= Degree; i++)
                result.coef[i] += coef[i];

            for (int i = 0; i <= other.Degree; i++)
                result.coef[i] -= other.coef[i];

            result.UpdateDegree();

            return result;
        }

        public Polynomial Multiply(Polynomial other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            Polynomial result = new Polynomial(0, Degree + other.Degree);

            for (int i = 0; i <= Degree; i++)
                for (int j = 0; j <= other.Degree; j++)
                    result.coef[i + j] += (coef[i] * other.coef[j]);

            result.UpdateDegree();

            return result;
        }

        public static Polynomial operator +(Polynomial a, Polynomial b) => a.Add(b);

        public static Polynomial operator -(Polynomial a, Polynomial b) => a.Subtract(b);

        public static Polynomial operator *(Polynomial a, Polynomial b) => a.Multiply(b);

        public override string ToString()
        {
            string result = $"({coef[Degree]}*x^{Degree}) ";

            for (int i = Degree - 1; i >= 0; i--)
                if (coef[i] != 0)
                    result += $"+ ({coef[i]}*x^{i}) ";

            return result;
        }
    }
}
