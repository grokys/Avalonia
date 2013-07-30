using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Input
{
    public delegate void MouseButtonEventHandler(object sender, MouseButtonEventArgs e);

    public class MouseButtonEventArgs : MouseEventArgs
    {
    }
}
