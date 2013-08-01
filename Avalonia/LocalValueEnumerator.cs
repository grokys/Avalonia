// -----------------------------------------------------------------------
// <copyright file="LocalValueEnumerator.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public struct LocalValueEnumerator : IEnumerator
    {
        private IDictionaryEnumerator propertyEnumerator;
        private Dictionary<DependencyProperty, object> properties;
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalValueEnumerator"/> struct.
        /// </summary>
        internal LocalValueEnumerator(Dictionary<DependencyProperty, object> properties)
        {
            this.count = properties.Count;
            this.properties = properties;
            this.propertyEnumerator = properties.GetEnumerator();
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public int Count
        {
            get { return this.count; }
        }

        public LocalValueEntry Current
        {
            get
            {
                return new LocalValueEntry(
                    (DependencyProperty)this.propertyEnumerator.Key,
                    this.propertyEnumerator.Value);
            }
        }

        public static bool operator !=(LocalValueEnumerator obj1, LocalValueEnumerator obj2)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(LocalValueEnumerator obj1, LocalValueEnumerator obj2)
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            return this.propertyEnumerator.MoveNext();
        }

        public void Reset()
        {
            this.propertyEnumerator.Reset();
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