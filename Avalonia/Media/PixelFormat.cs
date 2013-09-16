// -----------------------------------------------------------------------
// <copyright file="PixelFormat.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;

    public struct PixelFormat : IEquatable<PixelFormat>
    {
        private string name;
        private int bitsPerPixel;

        internal PixelFormat(string name, int bitsPerPixel)
        {
            this.name = name;
            this.bitsPerPixel = bitsPerPixel;
        }

        public int BitsPerPixel
        {
            get { return this.bitsPerPixel; }
        }

        public static bool operator !=(PixelFormat left, PixelFormat right)
        {
            return !left.Equals(right);
        }

        public static bool operator ==(PixelFormat left, PixelFormat right)
        {
            return left.Equals(right);
        }

        public static bool Equals(PixelFormat left, PixelFormat right)
        {
            return left.name == right.name;
        }

        public override bool Equals(object obj)
        {
            PixelFormat? other = obj as PixelFormat?;
            return (other != null) ? this.Equals(other.Value) : false;
        }
        
        public bool Equals(PixelFormat pixelFormat)
        {
            return Equals(this, pixelFormat);
        }
        
        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }
        
        public override string ToString()
        {
            return this.name.ToString();
        }
    }
}
