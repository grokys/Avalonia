// -----------------------------------------------------------------------
// <copyright file="BindingExpression.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    public sealed class BindingExpression : BindingExpressionBase
    {
        public Binding ParentBinding
        {
            get { return Binding; }
        }

        public object DataItem
        {
            get { return DataSource; }
        }

        internal BindingExpression(Binding binding, DependencyObject target, DependencyProperty property)
            : base(binding, target, property)
        {
        }

        public void UpdateSource()
        {
            UpdateSourceObject(true);
        }
    }
}
