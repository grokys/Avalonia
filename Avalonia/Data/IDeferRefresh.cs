using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    interface IDeferRefresh
    {
        int DeferLevel { get; set; }
        void Refresh();
    }
}
