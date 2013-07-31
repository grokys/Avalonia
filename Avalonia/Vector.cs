namespace Avalonia
{
    using System;
    using System.ComponentModel;
    using System.Windows.Markup;
    using Avalonia.Media;

    public struct Vector : IFormattable
    {
        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Vector value)
        {
            return x == value.X && y == value.Y;
        }

        public override bool Equals(object o)
        {
            if (!(o is Vector))
                return false;

            return Equals((Vector)o);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        public static bool Equals(Vector vector1, Vector vector2)
        {
            return vector1.Equals(vector2);
        }

        public static Point Add(Vector vector, Point point)
        {
            return new Point(vector.X + point.X, vector.Y + point.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X,
                       vector1.Y + vector2.Y);
        }

        public static double AngleBetween(Vector vector1, Vector vector2)
        {
            double cos_theta = (vector1.X * vector2.X + vector1.Y * vector2.Y) / (vector1.Length * vector2.Length);

            return Math.Acos(cos_theta) / Math.PI * 180;
        }

        public static double CrossProduct(Vector vector1, Vector vector2)
        {
            // ... what operation is this exactly?
            return vector1.X * vector2.Y - vector1.Y * vector2.X;
        }

        public static double Determinant(Vector vector1, Vector vector2)
        {
            // same as CrossProduct, it appears.
            return vector1.X * vector2.Y - vector1.Y * vector2.X;
        }

        public static Vector Divide(Vector vector, double scalar)
        {
            return new Vector(vector.X / scalar, vector.Y / scalar);
        }

        public static double Multiply(Vector vector1, Vector vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y;
        }

        public static Vector Multiply(Vector vector, Matrix matrix)
        {
            return new Vector(vector.X * matrix.M11 + vector.Y * matrix.M21,
                       vector.X * matrix.M12 + vector.Y * matrix.M22);
        }

        public static Vector Multiply(double scalar, Vector vector)
        {
            return new Vector(scalar * vector.X, scalar * vector.Y);
        }

        public static Vector Multiply(Vector vector, double scalar)
        {
            return new Vector(scalar * vector.X, scalar * vector.Y);
        }

        public void Negate()
        {
            x = -x;
            y = -y;
        }

        public void Normalize()
        {
            double ls = LengthSquared;
            if (ls == 1)
                return;

            double l = Math.Sqrt(ls);
            x /= l;
            y /= l;
        }

        public static Vector Subtract(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        public static Vector Parse(string source)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return String.Format("{0},{1}", x, y);
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared
        {
            get { return x * x + y * y; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /* operators */
        public static explicit operator Point(Vector vector)
        {
            return new Point(vector.X, vector.Y);
        }

        public static explicit operator Size(Vector vector)
        {
            return new Size(vector.X, vector.Y);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return Subtract(vector1, vector2);
        }

        public static Vector operator -(Vector vector)
        {
            Vector result = vector;
            result.Negate();
            return result;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !Equals(vector1, vector2);
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return Equals(vector1, vector2);
        }

        public static double operator *(Vector vector1, Vector vector2)
        {
            return Multiply(vector1, vector2);
        }

        public static Vector operator *(Vector vector, Matrix matrix)
        {
            return Multiply(vector, matrix);
        }

        public static Vector operator *(double scalar, Vector vector)
        {
            return Multiply(scalar, vector);
        }

        public static Vector operator *(Vector vector, double scalar)
        {
            return Multiply(vector, scalar);
        }

        public static Vector operator /(Vector vector, double scalar)
        {
            return Divide(vector, scalar);
        }

        public static Point operator +(Vector vector, Point point)
        {
            return Add(vector, point);
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return Add(vector1, vector2);
        }

        double x;
        double y;
    }

}