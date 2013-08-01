// -----------------------------------------------------------------------
// <copyright file="Size.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;

    public struct Size : IFormattable
    {
        private double width;
        private double height;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> struct.
        /// </summary>
        public Size(double width, double height)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentException("Width and Height must be non-negative.");
            }

            this.width = width;
            this.height = height;
        }

        public static Size Empty
        {
            get
            {
                Size s = new Size();
                s.width = s.height = double.NegativeInfinity;
                return s;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.width == double.NegativeInfinity &&
                       this.height == double.NegativeInfinity;
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
                    throw new InvalidOperationException("Cannot modify this property on the Empty Size.");
                }

                if (value < 0)
                {
                    throw new ArgumentException("height must be non-negative.");
                }

                this.height = value;
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
                    throw new InvalidOperationException("Cannot modify this property on the Empty Size.");
                }

                if (value < 0)
                {
                    throw new ArgumentException("width must be non-negative.");
                }

                this.width = value;
            }
        }

        public static bool Equals(Size size1, Size size2)
        {
            return size1.Equals(size2);
        }

        public static Size Parse(string source)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Size size1, Size size2)
        {
            return size1.Equals(size2);
        }

        public static bool operator !=(Size size1, Size size2)
        {
            return !size1.Equals(size2);
        }

        public static explicit operator Point(Size size)
        {
            return new Point(size.Width, size.Height);
        }

        public static explicit operator Vector(Size size)
        {
            return new Vector(size.Width, size.Height);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Size value)
        {
            return this.width == value.Width && this.height == value.Height;
        }

        public override bool Equals(object o)
        {
            if (!(o is Size))
            {
                return false;
            }

            return this.Equals((Size)o);
        }

        public override string ToString()
        {
            if (this.IsEmpty)
            {
                return "Empty";
            }

            return string.Format("{0},{1}", this.width, this.height);
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        string IFormattable.ToString(string format, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}