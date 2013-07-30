using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Animation
{
    public class Animatable : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }
    }
}
