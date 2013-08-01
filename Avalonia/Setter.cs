using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public class Setter : SetterBase
    {
        public Setter()
        {
        }

        public Setter(DependencyProperty property, object value)
        {
            this.Property = property;
            this.Value = value;
        }

        public Setter(DependencyProperty property, object value, string targetName)
        {
            this.Property = property;
            this.Value = value;
            this.TargetName = targetName;
        }

        public DependencyProperty Property
        {
            get;
            set;
        }

        public string TargetName
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }
    }
}
