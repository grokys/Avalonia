using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public class SolidColorBrush : Brush
    {
        public SolidColorBrush()
        {

        }

        public SolidColorBrush(Color color)
        {
            this.Color = color;
        }

        public Color Color { get; set; }
    }
}
