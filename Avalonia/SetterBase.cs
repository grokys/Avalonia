using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia
{
    public abstract class SetterBase
    {
        public bool IsSealed 
        { 
            get; 
            internal set; 
        }

        protected void CheckSealed()
        {
            if (this.IsSealed)
            {
                throw new InvalidOperationException("Object is sealed.");
            }
        }
    }
}
