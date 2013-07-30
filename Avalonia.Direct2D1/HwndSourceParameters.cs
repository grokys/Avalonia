using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Direct2D1
{
    public struct HwndSourceParameters
    {
        public HwndSourceParameters(string name)
            :this()
        {
            this.WindowName = name;
        }

        public HwndSourceParameters(string name, int width, int height)
            : this()
        {
            this.WindowName = name;
            this.Width = width;
            this.Height = height;
        }

        public bool AdjustSizingForNonClientArea { get; set; }
        public int ExtendedWindowStyle { get; set; }
        public int Height { get; set; }
        public IntPtr ParentWindow { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int WindowClassStyle { get; set; }
        public string WindowName { get; set; }
        public int WindowStyle { get; set; }
    }
}
