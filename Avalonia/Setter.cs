// -----------------------------------------------------------------------
// <copyright file="Setter.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Setter : SetterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setter"/> class.
        /// </summary>
        public Setter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setter"/> class.
        /// </summary>
        public Setter(DependencyProperty property, object value)
        {
            this.Property = property;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setter"/> class.
        /// </summary>
        public Setter(DependencyProperty property, object value, string targetName)
        {
            this.Property = property;
            this.Value = value;
            this.TargetName = targetName;
        }

        public DependencyProperty Property
        {
            get;
            set;
        }

        public string TargetName
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }
    }
}
