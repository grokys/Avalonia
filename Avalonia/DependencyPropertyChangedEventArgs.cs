// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyChangedEventArgs.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;

    public delegate void DependencyPropertyChangedEventHandler(object sender, DependencyPropertyChangedEventArgs e);

    public struct DependencyPropertyChangedEventArgs
    {
        public DependencyPropertyChangedEventArgs(DependencyProperty property, object oldValue, object newValue)
                    : this()
        {
            this.Property = property;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public object NewValue
        {
            get;
            private set;
        }

        public object OldValue
        {
            get;
            private set;
        }

        public DependencyProperty Property
        {
            get;
            private set;
        }

        public static bool operator !=(DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(DependencyPropertyChangedEventArgs left, DependencyPropertyChangedEventArgs right)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DependencyPropertyChangedEventArgs))
            {
                return false;
            }

            return this.Equals((DependencyPropertyChangedEventArgs)obj);
        }

        public bool Equals(DependencyPropertyChangedEventArgs args)
        {
            return this.Property == args.Property &&
                   this.NewValue == args.NewValue &&
                   this.OldValue == args.OldValue;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}