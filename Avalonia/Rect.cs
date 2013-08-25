// -----------------------------------------------------------------------
// <copyright file="Rect.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Globalization;
    using Avalonia.Media;

    public struct Rect : IFormattable
    {
        private double x;
        private double y;
        private double width;
        private double height;

        public Rect(Size size)
        {
            this.x = this.y = 0.0;
            this.width = size.Width;
            this.height = size.Height;
        }

        public Rect(Point point, Vector vector)
                    : this(point, Point.Add(point, vector))
        {
        }

        public Rect(Point point1, Point point2)
        {
            if (point1.X < point2.X)
            {
                this.x = point1.X;
                this.width = point2.X - point1.X;
            }
            else
            {
                this.x = point2.X;
                this.width = point1.X - point2.X;
            }

            if (point1.Y < point2.Y)
            {
                this.y = point1.Y;
                this.height = point2.Y - point1.Y;
            }
            else
            {
                this.y = point2.Y;
                this.height = point1.Y - point2.Y;
            }
        }

        public Rect(double x, double y, double width, double height)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentException("width and height must be non-negative.");
            }

            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rect(Point location, Size size)
        {
            this.x = location.X;
            this.y = location.Y;
            this.width = size.Width;
            this.height = size.Height;
        }

        public static Rect Empty
        {
            get
            {
                Rect r = new Rect();
                r.x = r.y = double.PositiveInfinity;
                r.width = r.height = double.NegativeInfinity;
                return r;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.x == double.PositiveInfinity &&
                       this.y == double.PositiveInfinity &&
                       this.width == double.NegativeInfinity &&
                       this.height == double.NegativeInfinity;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(this.x, this.y);
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify this property on the Empty Rect.");
                }

                this.x = value.X;
                this.y = value.Y;
            }
        }

        public Size Size
        {
            get
            {
                if (this.IsEmpty)
                {
                    return Size.Empty;
                }

                return new Size(this.width, this.height);
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify this property on the Empty Rect.");
                }

                this.width = value.Width;
                this.height = value.Height;
            }
        }

        public double X
        {
            get
            {
                return this.x;
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify this property on the Empty Rect.");
                }

                this.x = value;
            }
        }

        public double Y
        {
            get
            {
                return this.y;
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify this property on the Empty Rect.");
                }

                this.y = value;
            }
        }

        public double Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify this property on the Empty Rect.");
                }

                if (value < 0)
                {
                    throw new ArgumentException("width must be non-negative.");
                }

                this.width = value;
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }

            set
            {
                if (this.IsEmpty)
                {
                    throw new InvalidOperationException("Cannot modify this property on the Empty Rect.");
                }

                if (value < 0)
                {
                    throw new ArgumentException("height must be non-negative.");
                }

                this.height = value;
            }
        }

        public double Left
        {
            get { return this.x; }
        }

        public double Top
        {
            get { return this.y; }
        }

        public double Right
        {
            get { return this.x + this.width; }
        }

        public double Bottom
        {
            get { return this.y + this.height; }
        }

        public Point TopLeft
        {
            get { return new Point(this.Left, this.Top); }
        }

        public Point TopRight
        {
            get { return new Point(this.Right, this.Top); }
        }

        public Point BottomLeft
        {
            get { return new Point(this.Left, this.Bottom); }
        }

        public Point BottomRight
        {
            get { return new Point(this.Right, this.Bottom); }
        }

        public static bool Equals(Rect rect1, Rect rect2)
        {
            return rect1.Equals(rect2);
        }

        public static Rect Inflate(Rect rect, double width, double height)
        {
            if (width < rect.Width * -2)
            {
                return Rect.Empty;
            }

            if (height < rect.Height * -2)
            {
                return Rect.Empty;
            }

            Rect result = rect;
            result.Inflate(width, height);
            return result;
        }

        public static Rect Inflate(Rect rect, Size size)
        {
            return Rect.Inflate(rect, size.Width, size.Height);
        }

        public static Rect Intersect(Rect rect1, Rect rect2)
        {
            Rect result = rect1;
            result.Intersect(rect2);
            return result;
        }

        public static Rect Offset(Rect rect, double offsetX, double offsetY)
        {
            Rect result = rect;
            result.Offset(offsetX, offsetY);
            return result;
        }

        public static Rect Offset(Rect rect, Vector offsetVector)
        {
            Rect result = rect;
            result.Offset(offsetVector);
            return result;
        }

        public static Rect Transform(Rect rect, Matrix matrix)
        {
            Rect result = rect;
            result.Transform(matrix);
            return result;
        }

        public static Rect Union(Rect rect1, Rect rect2)
        {
            Rect result = rect1;
            result.Union(rect2);
            return result;
        }

        public static Rect Union(Rect rect, Point point)
        {
            Rect result = rect;
            result.Union(point);
            return result;
        }

        public static Rect Parse(string source)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Rect rect1, Rect rect2)
        {
            return !(rect1.Location == rect2.Location && rect1.Size == rect2.Size);
        }

        public static bool operator ==(Rect rect1, Rect rect2)
        {
            return rect1.Location == rect2.Location && rect1.Size == rect2.Size;
        }

        [AvaloniaSpecific]
        public static Rect operator -(Rect rect, Thickness thickness)
        {
            return new Rect(
                rect.Left + thickness.Left,
                rect.Top + thickness.Top,
                Math.Max(0.0, rect.Width - thickness.Left - thickness.Right),
                Math.Max(0.0, rect.Height - thickness.Top - thickness.Bottom));
        }

        public void Union(Point point)
        {
            this.Union(new Rect(point, point));
        }

        public bool Equals(Rect value)
        {
            return this.x == value.X &&
                   this.y == value.Y &&
                   this.width == value.Width &&
                   this.height == value.Height;
        }

        public override bool Equals(object o)
        {
            if (!(o is Rect))
            {
                return false;
            }

            return this.Equals((Rect)o);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Rect rect)
        {
            if (rect.Left < this.Left ||
                rect.Right > this.Right)
            {
                return false;
            }

            if (rect.Top < this.Top ||
                rect.Bottom > this.Bottom)
            {
                return false;
            }

            return true;
        }

        public bool Contains(double x, double y)
        {
            if (x < this.Left || x > this.Right)
            {
                return false;
            }

            if (y < this.Top || y > this.Bottom)
            {
                return false;
            }

            return true;
        }

        public bool Contains(Point point)
        {
            return this.Contains(point.X, point.Y);
        }

        public void Intersect(Rect rect)
        {
            double x = Math.Max(this.x, rect.x);
            double y = Math.Max(this.y, rect.y);
            double width = Math.Min(this.Right, rect.Right) - x;
            double height = Math.Min(this.Bottom, rect.Bottom) - y;

            if (width < 0 || height < 0)
            {
                this.x = this.y = double.PositiveInfinity;
                this.width = this.height = double.NegativeInfinity;
            }
            else
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }
        }

        public void Inflate(double width, double height)
        {
            // XXX any error checking like in the static case?
            this.x -= width;
            this.y -= height;

            this.width += 2 * width;
            this.height += 2 * height;
        }

        public void Inflate(Size size)
        {
            this.Inflate(size.Width, size.Height);
        }

        public bool IntersectsWith(Rect rect)
        {
            return !((this.Left >= rect.Right) || (this.Right <= rect.Left) ||
                (this.Top >= rect.Bottom) || (this.Bottom <= rect.Top));
        }

        public void Offset(double offsetX, double offsetY)
        {
            this.x += offsetX;
            this.y += offsetY;
        }

        public void Offset(Vector offsetVector)
        {
            this.x += offsetVector.X;
            this.y += offsetVector.Y;
        }

        public void Scale(double scaleX, double scaleY)
        {
            this.x *= scaleX;
            this.y *= scaleY;
            this.width *= scaleX;
            this.height *= scaleY;
        }

        public void Transform(Matrix matrix)
        {
            throw new NotImplementedException();
        }

        public void Union(Rect rect)
        {
            var left = Math.Min(this.Left, rect.Left);
            var top = Math.Min(this.Top, rect.Top);
            var right = Math.Max(this.Right, rect.Right);
            var bottom = Math.Max(this.Bottom, rect.Bottom);

            this.x = left;
            this.y = top;
            this.width = right - left;
            this.height = bottom - top;
        }

        public override string ToString()
        {
            return this.ToString(null);
        }

        public string ToString(IFormatProvider provider)
        {
            return this.ToString(null, provider);
        }

        string IFormattable.ToString(string format, IFormatProvider provider)
        {
            return this.ToString(format, provider);
        }

        private string ToString(string format, IFormatProvider provider)
        {
            if (this.IsEmpty)
            {
                return "Empty";
            }

            if (provider == null)
            {
                provider = CultureInfo.CurrentCulture;
            }

            if (format == null)
            {
                format = string.Empty;
            }

            string separator = ",";
            NumberFormatInfo numberFormat = provider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo;

            if (numberFormat != null && numberFormat.NumberDecimalSeparator == separator)
            {
                separator = ";";
            }

            string rectFormat = string.Format(
                "{{0:{0}}}{1}{{1:{0}}}{1}{{2:{0}}}{1}{{3:{0}}}",
                format,
                separator);

            return string.Format(provider, rectFormat, this.x, this.y, this.width, this.height);
        }
    }
}