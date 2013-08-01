// -----------------------------------------------------------------------
// <copyright file="SetterBase.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
