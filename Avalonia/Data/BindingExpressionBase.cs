// -----------------------------------------------------------------------
// <copyright file="BindingExpressionBase.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class BindingExpressionBase : Expression, IWeakEventListener
    {
        private bool evaluated;
        private object value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingExpressionBase"/> class.
        /// </summary>
        public BindingExpressionBase(DependencyObject target, DependencyProperty dp)
        {
            this.Target = target;
            this.TargetProperty = dp;
        }

        public DependencyObject Target { get; private set; }

        public DependencyProperty TargetProperty { get; private set; }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public object GetCurrentValue()
        {
            if (!this.evaluated)
            {
                this.value = this.Evaluate();
                this.evaluated = true;
            }

            return this.value;
        }

        protected void Invalidate()
        {
            this.evaluated = false;
            this.value = null;
            this.Target.InvalidateProperty(this.TargetProperty);
        }

        protected abstract object Evaluate();
    }
}
