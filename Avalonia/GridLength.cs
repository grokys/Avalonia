// -----------------------------------------------------------------------
// <copyright file="GridLength.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.ComponentModel;

    public enum GridUnitType
    {
        Auto = 0,
        Pixel = 1,
        Star = 2,
    }

    [TypeConverter(typeof(GridLengthConverter))]
    public struct GridLength : IEquatable<GridLength>
    {
        private GridUnitType type;

        private double value;

        public GridLength(double value)
            : this(value, GridUnitType.Pixel)
        {
        }

        public GridLength(double value, GridUnitType type)
        {
            if (value < 0 || double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new ArgumentException("Invalid value", "value");
            }

            if (type < GridUnitType.Auto || type > GridUnitType.Star)
            {
                throw new ArgumentException("Invalid value", "type");
            }

            this.type = type;
            this.value = value;
        }

        public static GridLength Auto 
        {
            get { return new GridLength(0, GridUnitType.Auto); }
        }

        public GridUnitType GridUnitType
        {
            get { return this.type; }
        }

        public bool IsAbsolute
        {
            get { return this.type == GridUnitType.Pixel; }
        }

        public bool IsAuto
        {
            get { return this.type == GridUnitType.Auto; }
        }

        public bool IsStar 
        {
            get { return this.type == GridUnitType.Star; }
        }

        public double Value 
        {
            get { return this.value; }
        }

        public override bool Equals(object oCompare)
        {
            if (oCompare == null)
                return false;

            if (!(oCompare is GridLength))
                return false;

            return this == (GridLength)oCompare;
        }

        public bool Equals(GridLength gridLength)
        {
            return this == gridLength;
        }

        public static bool operator ==(GridLength a, GridLength b)
        {
            return (a.IsAuto && b.IsAuto) || (a.value == b.value && a.type == b.type);
        }

        public static bool operator !=(GridLength gl1, GridLength gl2)
        {
            return !(gl1 == gl2);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode() ^ type.GetHashCode();
        }

        public override string ToString()
        {
            if (IsAuto)
            {
                return "Auto";
            }

            string s = value.ToString();
            return IsStar ? s + "*" : s;
        }
    }
}
