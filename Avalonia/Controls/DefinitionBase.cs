// -----------------------------------------------------------------------
// <copyright file="DefinitionBase.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    public class DefinitionBase : FrameworkContentElement
    {
        public static readonly DependencyProperty SharedSizeGroupProperty = 
            DependencyProperty.Register(
                "SharedSizeGroup",
                typeof(string),
                typeof(DefinitionBase),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.Inherits));

        public string SharedSizeGroup
        {
            get { return (string)this.GetValue(SharedSizeGroupProperty); }
            set { this.SetValue(SharedSizeGroupProperty, value); }
        }
    }
}
