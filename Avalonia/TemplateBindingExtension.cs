// -----------------------------------------------------------------------
// <copyright file="TemplateBindingExtension.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System.Windows.Markup;
    using Avalonia.Data;

    public class TemplateBindingExtension : MarkupExtension
    {
        private string path;

        public TemplateBindingExtension(string path)
        {
            this.path = path;
        }

        public override object ProvideValue(System.IServiceProvider serviceProvider)
        {
            Binding result = new Binding(this.path);
            result.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            return result;
        }
    }
}
