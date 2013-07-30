using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public abstract class BindingExpressionBase : Expression, IWeakEventListener
    {
        private bool evaluated;
        private object value;

        public BindingExpressionBase(DependencyObject target, DependencyProperty dp)
        {
            this.Target = target;
            this.TargetProperty = dp;
        }

        public DependencyObject Target { get; private set; }
        public DependencyProperty TargetProperty { get; private set; }

        public object GetCurrentValue()
        {
            if (!evaluated)
            {
                this.value = this.Evaluate();
                this.evaluated = true;
            }

            return value;
        }

        protected void Invalidate()
        {
            this.evaluated = false;
            this.value = null;
            this.Target.InvalidateProperty(this.TargetProperty);
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected abstract object Evaluate();
    }
}
