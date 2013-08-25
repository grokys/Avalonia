using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public static class FontWeights
    {
        private static FontWeight normal = new FontWeight();

        public static FontWeight Normal 
        {
            get { return normal; }
        }
    }
}
