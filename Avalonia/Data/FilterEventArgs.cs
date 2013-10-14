using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public delegate void FilterEventHandler(object sender, FilterEventArgs e);

    public class FilterEventArgs : EventArgs
    {
        internal FilterEventArgs(object item)
        {
            Item = item;
        }

        public bool Accepted
        {
            get;
            set;
        }

        public object Item
        {
            get;
            internal set;
        }
    }
}
