// -----------------------------------------------------------------------
// <copyright file="Color.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct Color
    {
        public byte A { get; set; }

        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color
            {
                A = a,
                R = r,
                G = g,
                B = b,
            };
        }

        public static Color FromRgb(byte r, byte g, byte b)
        {
            return new Color
            {
                A = 0xff,
                R = r,
                G = g,
                B = b,
            };
        }

        public override string ToString()
        {
            uint rgb = ((uint)this.A << 24) | ((uint)this.R << 16) | ((uint)this.G << 8) | (uint)this.B;
            return string.Format("#{0:x8}", rgb);
        }

        [AvaloniaSpecific]
        public static Color FromUInt32(uint value)
        {
            return new Color
            {
                A = (byte)((value >> 24) & 0xff),
                R = (byte)((value >> 16) & 0xff),
                G = (byte)((value >> 8) & 0xff),
                B = (byte)(value & 0xff),
            };
        }

        [AvaloniaSpecific]
        public uint ToUint32()
        {
            return ((uint)this.A << 24) | ((uint)this.R << 16) | ((uint)this.G << 8) | (uint)this.B;
        }
    }
}
