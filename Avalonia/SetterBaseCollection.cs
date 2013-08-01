// -----------------------------------------------------------------------
// <copyright file="SetterBaseCollection.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class SetterBaseCollection : Collection<SetterBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetterBaseCollection"/> class.
        /// </summary>
        public SetterBaseCollection()
        {
        }

        protected override void ClearItems()
        {
        }

        protected override void InsertItem(int index, SetterBase item)
        {
        }

        protected override void RemoveItem(int index)
        {
        }

        protected override void SetItem(int index, SetterBase item)
        {
        }
    }
}
