// -----------------------------------------------------------------------
// <copyright file="LocalValueEntry.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;

    public struct LocalValueEntry
    {
        private DependencyProperty property;
        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalValueEntry"/> struct.
        /// </summary>
        internal LocalValueEntry(DependencyProperty property, object value)
        {
            this.property = property;
            this.value = value;
        }

        public DependencyProperty Property
        {
            get { return this.property; }
        }

        public object Value
        {
            get { return this.value; }
        }

        public static bool operator !=(LocalValueEntry obj1, LocalValueEntry obj2)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(LocalValueEntry obj1, LocalValueEntry obj2)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}