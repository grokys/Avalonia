using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public class Visual : DependencyObject
    {
        protected internal virtual int VisualChildrenCount 
        {
            get { return 0; }
        }

        protected internal Transform VisualTransform { get; protected set; }

        protected internal virtual Visual GetVisualChild(int index)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
