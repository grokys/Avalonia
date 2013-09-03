// -----------------------------------------------------------------------
// <copyright file="DoubleKeyedDictionary.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class DoubleKeyedDictionary<K1, K2> : IEnumerable<KeyValuePair<K1, K2>>
    {
        private Dictionary<K1, K2> forwards;
        private Dictionary<K2, K1> backwards;

        public DoubleKeyedDictionary()
        {
            this.forwards = new Dictionary<K1, K2>();
            this.backwards = new Dictionary<K2, K1>();
        }

        public K1 this[K2 key]
        {
            get { return this.backwards[key]; }
        }

        public K2 this[K1 key]
        {
            get { return this.forwards[key]; }
        }

        public void Add(K1 key1, K2 key2)
        {
            this.Add(key1, key2, false);
        }

        public void Add(K1 key1, K2 key2, bool ignoreExisting)
        {
            if (!ignoreExisting && (this.forwards.ContainsKey(key1) || this.backwards.ContainsKey(key2)))
            {
                throw new InvalidOperationException("Dictionary already contains this key pair");
            }

            this.forwards[key1] = key2;
            this.backwards[key2] = key1;
        }

        public void Clear()
        {
            this.forwards.Clear();
            this.backwards.Clear();
        }

        public void Remove(K1 key1, K2 key2)
        {
            if (!this.forwards.ContainsKey(key1) || !this.backwards.ContainsKey(key2))
            {
                throw new InvalidOperationException("Dictionary does not contain this key pair");
            }

            this.forwards.Remove(key1);
            this.backwards.Remove(key2);
        }

        public void Remove(K1 key1, K2 key2, bool ignoreExisting)
        {
            if (!ignoreExisting && (!this.forwards.ContainsKey(key1) || !this.backwards.ContainsKey(key2)))
            {
                throw new InvalidOperationException("Dictionary does not contain this key pair");
            }

            this.forwards.Remove(key1);
            this.backwards.Remove(key2);
        }

        public bool TryMap(K1 key, out K2 value)
        {
            return this.forwards.TryGetValue(key, out value);
        }

        public bool TryMap(K2 key, out K1 value)
        {
            return this.backwards.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<K1, K2>> GetEnumerator()
        {
            return this.forwards.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
