// -----------------------------------------------------------------------
// <copyright file="FrameworkTemplate.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Windows.Markup;
    using Avalonia.Controls;
    using Avalonia.Media;
    using Avalonia.Threading;

    [ContentProperty("Template")]
    public class FrameworkTemplate : DispatcherObject
    {
        [AmbientAttribute]
        [XamlDeferLoad(typeof(TemplateContentLoader), typeof(TemplateContent))]
        public TemplateContent Template { get; set; }

        internal FrameworkElement CreateVisualTree(DependencyObject parent)
        {
            FrameworkElement result = this.Template.Load() as FrameworkElement;
            result.TemplatedParent = parent;
            return result;
        }
    }
}
