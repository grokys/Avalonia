using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public class BindingExpression : BindingExpressionBase
    {
        private IPropertyPathParser pathParser;
        private PropertyPathToken[] chain;

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
            this.chain = this.pathParser.Parse(this.DataItem, this.ParentBinding.Path.Path);
            this.AttachListeners();
            return this.chain.Last().Object;
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
