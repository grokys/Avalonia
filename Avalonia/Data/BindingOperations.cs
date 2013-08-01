// -----------------------------------------------------------------------
// <copyright file="BindingOperations.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class BindingOperations
    {
        public static BindingExpressionBase SetBinding(
            DependencyObject target,
            DependencyProperty dp,
            BindingBase binding)
        {
            Binding b = binding as Binding;

            if (b == null)
            {
                throw new NotSupportedException("Unsupported binding type.");
            }

            PropertyPathParser pathParser = new PropertyPathParser();
            BindingExpression expression = new BindingExpression(pathParser, target, dp, b);
            target.SetValue(dp, expression);
            return expression;
        }
    }
}
