// -----------------------------------------------------------------------
// <copyright file="ItemsControl.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System.ComponentModel;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class ItemsControl : Control
    {
        public ItemsControl()
        {
            this.Items = new ItemCollection();
        }

        [Bindable(true)]
        public ItemCollection Items 
        { 
            get; 
            private set; 
        }
    }
}
