// -----------------------------------------------------------------------
// <copyright file="GeneratorPosition.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System;

    public struct GeneratorPosition
    {
        public GeneratorPosition(int index, int offset)
            : this()
        {
            this.Index = index;
            this.Offset = offset;
        }

        public int Index
        {
            get;
            set;
        }

        public int Offset
        {
            get;
            set;
        }

        public static bool operator ==(GeneratorPosition gp1, GeneratorPosition gp2)
        {
            return gp1.Index == gp2.Index && gp1.Offset == gp2.Offset;
        }

        public static bool operator !=(GeneratorPosition gp1, GeneratorPosition gp2)
        {
            return !(gp1 == gp2);
        }

        public override bool Equals(object o)
        {
            return o is GeneratorPosition && this == ((GeneratorPosition)o);
        }

        public override int GetHashCode()
        {
            return this.Index + this.Offset;
        }

        public override string ToString()
        {
            return string.Format("GeneratorPosition ({0},{1})", this.Index, this.Offset);
        }
    }
}
