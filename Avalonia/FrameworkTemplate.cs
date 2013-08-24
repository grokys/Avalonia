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
    public class FrameworkTemplate : DispatcherObject, INameScope, IQueryAmbient
    {
        private NameScope nameScope = new NameScope();

        [Ambient]
        [XamlDeferLoad(typeof(TemplateContentLoader), typeof(TemplateContent))]
        public TemplateContent Template { get; set; }

        public void RegisterName(string name, object scopedElement)
        {
            this.nameScope.RegisterName(name, scopedElement);
        }

        public void UnregisterName(string name)
        {
            this.nameScope.UnregisterName(name);
        }

        object INameScope.FindName(string name)
        {
            return this.nameScope.FindName(name);
        }

        bool IQueryAmbient.IsAmbientPropertyAvailable(string propertyName)
        {
            // TODO: this should be more complex but I can't understand the docs:
            // http://msdn.microsoft.com/en-us/library/system.windows.markup.iqueryambient.aspx
            return true;
        }

        internal virtual FrameworkElement CreateVisualTree(DependencyObject parent)
        {
            FrameworkElement result = this.Template.Load() as FrameworkElement;
            result.TemplatedParent = parent;
            return result;
        }
    }
}
