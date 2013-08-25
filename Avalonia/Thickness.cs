// -----------------------------------------------------------------------
// <copyright file="Thickness.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TypeConverter(typeof(ThicknessConverter))]
    public struct Thickness
    {
        public Thickness(double uniformLength)
            : this()
        {
            this.Left = this.Top = this.Right = this.Bottom = uniformLength;
        }

        public Thickness(double left, double top, double right, double bottom)
                    : this()
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }

        internal bool IsEmpty
        {
            get { return this.Left == 0 && this.Top == 0 && this.Right == 0 && this.Bottom == 0; }
        }

        public static bool operator ==(Thickness a, Thickness b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Thickness a, Thickness b)
        {
            return !a.Equals(b);
        }

        [AvaloniaSpecific]
        public static Thickness operator +(Thickness a, Thickness b)
        {
            return new Thickness(
                a.Left + b.Left,
                a.Top + b.Top,
                a.Right + b.Right,
                a.Bottom + b.Bottom);
        }

        public override bool Equals(object obj)
        {
            if (obj is Thickness)
            {
                Thickness other = (Thickness)obj;
                return this.Left == other.Left && 
                       this.Top == other.Top && 
                       this.Right == other.Right && 
                       this.Bottom == other.Bottom;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + this.Left.GetHashCode();
                hash = (hash * 23) + this.Top.GetHashCode();
                hash = (hash * 23) + this.Right.GetHashCode();
                hash = (hash * 23) + this.Bottom.GetHashCode();
                return hash;
            }
        }
    }
}
