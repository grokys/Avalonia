// -----------------------------------------------------------------------
// <copyright file="ControlTemplate.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Windows.Markup;
    using Avalonia.Media;

    public class ControlTemplate : FrameworkTemplate
    {
        public ControlTemplate()
        {
            this.Triggers = new TriggerCollection();
        }

        [AmbientAttribute]
        public Type TargetType 
        { 
            get; 
            set; 
        }

        public TriggerCollection Triggers 
        { 
            get; 
            private set; 
        }

        internal override FrameworkElement CreateVisualTree(DependencyObject parent)
        {
            FrameworkElement result = base.CreateVisualTree(parent);

            foreach (TriggerBase trigger in this.Triggers)
            {
                trigger.Attach(result, parent);
            }

            return result;
        }
    }
}
