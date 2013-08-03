// -----------------------------------------------------------------------
// <copyright file="DependencyPropertyKey.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;

    public sealed class DependencyPropertyKey
    {
        private DependencyProperty dependencyProperty;

        internal DependencyPropertyKey(DependencyProperty dependencyProperty)
        {
            this.dependencyProperty = dependencyProperty;
        }

        public DependencyProperty DependencyProperty
        {
            get { return this.dependencyProperty; }
        }

        public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata)
        {
            this.dependencyProperty.OverrideMetadata(forType, typeMetadata, this);
        }
    }
}