// -----------------------------------------------------------------------
// <copyright file="ListBox.cs" company="Steven Kirk">
// Copyright 2014 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using Avalonia.Controls.Primitives;

    public class ListBox : Selector
    {
        static ListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBox), new FrameworkPropertyMetadata(typeof(ListBox)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            ListBoxItem result = new ListBoxItem();
            System.Console.WriteLine("Created: " + result.GetHashCode());
            result.Initialized += (s, e) =>
            {
                System.Console.WriteLine("Initialized: " + result.GetHashCode());
            };
            return result;
        }
    }
}
