// -----------------------------------------------------------------------
// <copyright file="ListBoxItem.cs" company="Steven Kirk">
// Copyright 2014 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    public class ListBoxItem : ContentControl
    {
        static ListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxItem), new FrameworkPropertyMetadata(typeof(ListBoxItem)));
        }
    }
}
