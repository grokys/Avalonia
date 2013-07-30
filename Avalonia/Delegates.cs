using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public delegate object CoerceValueCallback(DependencyObject d, object baseValue);
    public delegate void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e);
    public delegate bool ValidateValueCallback(object value);
}
