using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Media
{
    public class TranslateTransform : Transform
    {
        public TranslateTransform(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override Matrix Value
        {
            get { return new Matrix(1, 0, 0, 1, this.X, this.Y); }
        }
    }
}
