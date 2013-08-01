namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Markup;

    public class NameScope : INameScope
    {
        Dictionary<string, object> scope = new Dictionary<string, object>();

        public object FindName(string name)
        {
            object result;
            this.scope.TryGetValue(name, out result);
            return result;
        }

        public static readonly DependencyProperty NameScopeProperty =
            DependencyProperty.RegisterAttached(
                "NameScope",
                typeof(NameScope),
                typeof(NameScope));

        public void RegisterName(string name, object scopedElement)
        {
            this.scope.Add(name, scopedElement);
        }

        public void UnregisterName(string name)
        {
            this.scope.Remove(name);
        }

        public static INameScope GetNameScope(DependencyObject dependencyObject)
        {
            return (INameScope)dependencyObject.GetValue(NameScopeProperty);
        }

        public static void SetNameScope(DependencyObject dependencyObject, INameScope value)
        {
            dependencyObject.SetValue(NameScopeProperty, value);
        }

    }
}
