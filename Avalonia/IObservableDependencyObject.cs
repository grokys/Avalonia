using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public interface IObservableDependencyObject
    {
        void AttachPropertyChangedHandler(
            string propertyName,
            DependencyPropertyChangedEventHandler handler);

        void RemovePropertyChangedHandler(
            string propertyName,
            DependencyPropertyChangedEventHandler handler);
    }
}
