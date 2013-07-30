using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace Avalonia.Controls
{
    public class ControlTemplate : FrameworkTemplate
    {
        [AvaloniaSpecific]
        public virtual Visual CreateVisualTree(DependencyObject parent)
        {
            throw new NotImplementedException();
        }
    }
}
