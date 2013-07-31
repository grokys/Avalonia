namespace Avalonia
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    public class ResourceDictionary
    {
        private Dictionary<object, object> resources = new Dictionary<object,object>();

        public ResourceDictionary() 
        { 
        }

        public int Count 
        { 
            get { return this.resources.Count; }
        }

        public ICollection Keys 
        { 
            get { return this.resources.Keys; }
        }

        public ICollection Values 
        { 
            get { return this.resources.Values; }
        }

        public object this[object key] 
        { 
            get
            {
                object result;
                this.resources.TryGetValue(key, out result);
                return result;
            }
            
            set
            {
                this.resources[key] = value;
            }
        }

        public void Add(object key, object value) 
        {
            this.resources.Add(key, value);
        }

        public void Clear() 
        {
            this.resources.Clear();
        }

        public bool Contains(object key) 
        {
            return this.resources.ContainsKey(key);
        }

        public IDictionaryEnumerator GetEnumerator() 
        {
            return this.resources.GetEnumerator();
        }

        public void Remove(object key) 
        {
            this.resources.Remove(key);
        }
    }
}
