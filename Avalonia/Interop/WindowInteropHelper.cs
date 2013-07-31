using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Interop
{
    public class WindowInteropHelper
    {
        private Window window;

        public WindowInteropHelper(Window window)
        {
            this.window = window;
        }

        public IntPtr Handle 
        { 
            get { return this.window.PresentationSource.Handle; }
        }
    }
}
