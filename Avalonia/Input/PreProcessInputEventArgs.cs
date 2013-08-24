using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Input
{
    public delegate void PreProcessInputEventHandler(object sender, PreProcessInputEventArgs e);

    public class PreProcessInputEventArgs : EventArgs
    {
        private bool canceled;

        public PreProcessInputEventArgs(InputEventArgs input)
        {
            this.Input = input;
        }

        public bool Canceled
        {
            get { return this.canceled; }
        }

        public InputEventArgs Input 
        { 
            get; 
            private set; 
        }

        public void Cancel()
        {
            this.canceled = true;
        }
    }
}
