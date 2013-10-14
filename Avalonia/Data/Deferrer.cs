using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    class Deferrer : IDisposable
    {
        IDeferRefresh Source
        {
            get;
            set;
        }

        public Deferrer(IDeferRefresh source)
        {
            Source = source;
            Source.DeferLevel++;
        }

        public void Dispose()
        {
            if (Source != null)
            {
                var s = Source;
                Source = null;

                s.DeferLevel--;
                if (s.DeferLevel == 0)
                    s.Refresh();
            }
        }
    }
}
