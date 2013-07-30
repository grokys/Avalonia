using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public interface IWeakEventListener
    {
        bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e);
    }
}
