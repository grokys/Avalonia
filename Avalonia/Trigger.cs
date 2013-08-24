// -----------------------------------------------------------------------
// <copyright file="Trigger.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Markup;
    using System.Xaml;
    using Avalonia.Controls;

    [ContentProperty("Setters")]
    public class Trigger : TriggerBase
    {
        private Dictionary<DependencyObject, FrameworkElement> attached = new Dictionary<DependencyObject, FrameworkElement>();
        private List<FrameworkElement> applied = new List<FrameworkElement>();

        public Trigger()
        {
            this.Setters = new SetterBaseCollection();
        }

        public DependencyProperty Property
        {
            get;
            set;
        }

        public SetterBaseCollection Setters 
        { 
            get; 
            private set; 
        }

        [AmbientAttribute]
        public string SourceName
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }

        internal override void Attach(FrameworkElement target, DependencyObject parent)
        {
            DependencyObject source = parent;

            if (this.SourceName != null)
            {
                source = (DependencyObject)target.FindName(this.SourceName);
            }

            this.attached.Add(source, target);

            if (this.CheckCondition(source))
            {
                this.Setters.Attach(target);
                this.applied.Add(target);
            }

            IObservableDependencyObject observable = (IObservableDependencyObject)source;
            observable.AttachPropertyChangedHandler(this.Property.Name, ValueChanged);
        }

        private bool CheckCondition(DependencyObject source)
        {
            return source.GetValue(this.Property).Equals(Convert.ChangeType(this.Value, this.Property.PropertyType));
        }

        private void ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject source = (DependencyObject)sender;
            FrameworkElement target = this.attached[source];

            if (this.CheckCondition(source))
            {
                if (!applied.Contains(target))
                {
                    this.Setters.Attach(target);
                    this.applied.Add(target);
                }
            }
            else
            {
                if (applied.Contains(target))
                {
                    this.Setters.Detach(target);
                    this.applied.Remove(target);
                }
            }
        }
    }
}
