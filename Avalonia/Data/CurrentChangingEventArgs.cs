using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public delegate void CurrentChangingEventHandler(object sender, CurrentChangingEventArgs e);

    public class CurrentChangingEventArgs : EventArgs
    {
        private bool canCancelEvent;
        private bool canceled;

        public CurrentChangingEventArgs()
            : this(true)
        {
        }

        public CurrentChangingEventArgs(bool isCancelable)
        {
            canCancelEvent = isCancelable;
            canceled = false;
        }

        public bool Cancel
        {
            get { return canceled; }
            set
            {
                if (!IsCancelable && value)
                    throw new InvalidOperationException("Cannot cancel an event that is not Cancelable.");
                canceled = value;
            }
        }

        public bool IsCancelable
        {
            get { return canCancelEvent; }
        }
    }
}
