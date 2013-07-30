using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public struct Thickness
    {
        public Thickness(double uniformLength)
            : this()
        {
            this.Left = this.Top = this.Right = this.Bottom = uniformLength;
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
    }
}
