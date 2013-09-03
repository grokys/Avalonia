// -----------------------------------------------------------------------
// <copyright file="ItemsChangedEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public delegate void ItemsChangedEventHandler(object sender, ItemsChangedEventArgs e);

    public class ItemsChangedEventArgs : EventArgs
    {
        internal ItemsChangedEventArgs()
        {
        }

        public NotifyCollectionChangedAction Action
        {
            get;
            internal set;
        }

        public int ItemCount
        {
            get;
            internal set;
        }

        public int ItemUICount
        {
            get;
            internal set;
        }

        public GeneratorPosition OldPosition
        {
            get;
            internal set;
        }

        public GeneratorPosition Position
        {
            get;
            internal set;
        }
    }
}
