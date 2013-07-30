using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public struct PropertyPathToken
    {
        public PropertyPathToken(object o, string propertyName)
            : this()
        {
            this.Object = o;
            this.PropertyName = propertyName;
        }

        public object Object { get; private set; }
        public string PropertyName { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Object, this.PropertyName);
        }
    }
}
