// -----------------------------------------------------------------------
// <copyright file="Point.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Globalization;
    using Avalonia.Media;

    public struct Point : IFormattable
    {
        private double x;
        private double y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public static Point Add(Point point, Vector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y);
        }

        public static bool Equals(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        public static Point Multiply(Point point, Matrix matrix)
        {
            return new Point(
                (point.X * matrix.M11) + ((point.Y * matrix.M21) + matrix.OffsetX),
                (point.X * matrix.M12) + ((point.Y * matrix.M22) + matrix.OffsetY));
        }

        public static Vector Subtract(Point point1, Point point2)
        {
            return new Vector(point1.X - point2.X, point1.Y - point2.Y);
        }

        public static Point Subtract(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y);
        }

        public static Point Parse(string source)
        {
            string[] points = source.Split(',');

            if (points.Length < 2)
            {
                throw new InvalidOperationException("source does not contain two numbers");
            }

            if (points.Length > 2)
            {
                throw new InvalidOperationException("source contains too many delimiters");
            }

            CultureInfo ci = CultureInfo.InvariantCulture;
            return new Point(Convert.ToDouble(points[0], ci), Convert.ToDouble(points[1], ci));
        }

        public static Vector operator -(Point point1, Point point2)
        {
            return Subtract(point1, point2);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return Subtract(point, vector);
        }

        public static Point operator +(Point point, Vector vector)
        {
            return Add(point, vector);
        }

        public static Point operator *(Point point, Matrix matrix)
        {
            return Multiply(point, matrix);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !point1.Equals(point2);
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        public static explicit operator Size(Point point)
        {
            return new Size(point.X, point.Y);
        }

        public static explicit operator Vector(Point point)
        {
            return new Vector(point.X, point.Y);
        }

        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            return this.ToString(format, formatProvider);
        }

        public override bool Equals(object o)
        {
            if (!(o is Point))
            {
                return false;
            }

            return this.Equals((Point)o);
        }

        public bool Equals(Point value)
        {
            return this.x == value.X && this.y == value.Y;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode();
        }

        public void Offset(double offsetX, double offsetY)
        {
            this.x += offsetX;
            this.y += offsetY;
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }

        private string ToString(string format, IFormatProvider formatProvider)
        {
            CultureInfo ci = (CultureInfo)formatProvider;

            if (ci == null)
            {
                ci = CultureInfo.CurrentCulture;
            }

            string seperator = ci.NumberFormat.NumberDecimalSeparator;
            if (seperator.Equals(","))
            {
                seperator = ";";
            }
            else
            {
                seperator = ",";
            }

            object[] ob = { this.x, seperator, this.y };

            return string.Format(formatProvider, "{0:" + format + "}{1}{2:" + format + "}", ob);
        }
    }
}