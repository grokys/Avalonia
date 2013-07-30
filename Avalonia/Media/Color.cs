using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public struct Color
    {
        public byte A { get; set;  }
        public byte R { get; set;  }
        public byte G { get; set;  }
        public byte B { get; set;  }

        public static Color FromRgb(byte r, byte g, byte b)
        {
            return new Color
            {
                A = 0xff,
                R = r,
                G = g,
                B = b,
            };
        }
    }
}
