// -----------------------------------------------------------------------
// <copyright file="BindingExpression.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class BindingExpression : BindingExpressionBase
    {
        private IPropertyPathParser pathParser;
        private PropertyPathToken[] chain;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingExpression"/> class.
        /// </summary>
        public BindingExpression(
                    IPropertyPathParser pathParser,
                    DependencyObject target,
                    DependencyProperty dp,
                    Binding binding)
                    : base(target, dp)
        {
            this.pathParser = pathParser;
            this.ParentBinding = binding;
        }

        public object DataItem
        {
            get { return this.ParentBinding.Source; }
        }

        public Binding ParentBinding
        {
            get;
            private set;
        }

        public object ResolvedSource
        {
            get;
            private set;
        }

        public string ResolvedSourcePropertyName
        {
            get;
            private set;
        }

        protected override object Evaluate()
        {
            this.chain = this.pathParser.Parse(this.GetSource(), this.ParentBinding.Path.Path);
            this.AttachListeners();
            return this.chain.Last().Object;
        }

        private object GetSource()
        {
            if (this.DataItem != null)
            {
                return this.DataItem;
            }
            else
            {
                if (this.ParentBinding.RelativeSource != null)
                {
                    switch (this.ParentBinding.RelativeSource.Mode)
                    {
                        case RelativeSourceMode.TemplatedParent:
                            FrameworkElement fe = this.Target as FrameworkElement;

                            if (fe != null)
                            {
                                return fe.TemplatedParent;
                            }
                            else
                            {
                                throw new InvalidOperationException("Cannot get TemplatedParent outside a Template.");
                            }
                    }
                }
            }

            throw new NotSupportedException("Don't know how to get binding source!");
        }

        private void AttachListeners()
        {
            foreach (PropertyPathToken link in this.chain.Take(this.chain.Length - 1))
            {
                this.AttachListener(link);
            }
        }

        private void AttachListener(PropertyPathToken link)
        {
            IObservableDependencyObject dependencyObject = link.Object as IObservableDependencyObject;

            if (dependencyObject != null)
            {
                dependencyObject.AttachPropertyChangedHandler(link.PropertyName, this.DependencyPropertyChanged);
            }
        }

        private void DetachListeners()
        {
            foreach (PropertyPathToken link in this.chain.Take(this.chain.Length - 1))
            {
                this.DetachListener(link);
            }
        }

        private void DetachListener(PropertyPathToken link)
        {
            IObservableDependencyObject dependencyObject = link.Object as IObservableDependencyObject;

            if (dependencyObject != null)
            {
                dependencyObject.RemovePropertyChangedHandler(link.PropertyName, this.DependencyPropertyChanged);
            }
        }

        private void DependencyPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.DetachListeners();
            this.Invalidate();
        }
    }
}
