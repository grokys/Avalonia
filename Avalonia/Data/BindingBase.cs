// -----------------------------------------------------------------------
// <copyright file="BindingBase.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Windows.Markup;

    public abstract class BindingBase
    {
        private object fallbackValue;

        private bool isSealed;
        
        private string stringFormat;
        
        private object targetNullValue;

        protected BindingBase()
        {
        }

        public object FallbackValue
        {
            get 
            { 
                return this.fallbackValue; 
            }

            set
            {
                this.CheckSealed();
                this.fallbackValue = value;
            }
        }

        public string StringFormat
        {
            get 
            { 
                return this.stringFormat; 
            }
            
            set
            {
                this.CheckSealed();
                this.stringFormat = value;
            }
        }

        public object TargetNullValue
        {
            get 
            {
                return this.targetNullValue; 
            }
            
            set
            {
                this.CheckSealed();
                this.targetNullValue = value;
            }
        }

        internal bool Sealed
        {
            get
            {
                return this.isSealed;
            }

            private set
            {
                this.isSealed = value;
            }
        }

        protected void CheckSealed()
        {
            if (this.Sealed)
            {
                throw new InvalidOperationException("The Binding cannot be changed after it has been used.");
            }
        }

        internal void Seal()
        {
            this.Sealed = true;
        }
    }
}
