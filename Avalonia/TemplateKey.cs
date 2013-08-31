// -----------------------------------------------------------------------
// <copyright file="TemplateKey.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    ////[TypeConverterAttribute(typeof(TemplateKeyConverter))]
    public abstract class TemplateKey : ResourceKey, ISupportInitialize
    {
        protected TemplateKey(TemplateKey.TemplateType templateType)
        {
        }

        protected TemplateKey(TemplateKey.TemplateType templateType, object dataType)
        {
            this.DataType = dataType;
        }

        protected enum TemplateType
        {
            DataTemplate,
            TableTemplate,
        }

        public override Assembly Assembly 
        {
            get { throw new NotImplementedException(); }
        }

        public object DataType 
        { 
            get; 
            set; 
        }

        public override bool Equals(object o)
        {
            TemplateKey other = o as TemplateKey;

            if (other != null)
            {
                return other.GetType() == this.GetType() && other.DataType == this.DataType;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode() ^ ((this.DataType != null) ? this.DataType.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return this.DataType.ToString();
        }

        void ISupportInitialize.BeginInit()
        {
            throw new NotImplementedException();
        }

        void ISupportInitialize.EndInit()
        {
            throw new NotImplementedException();
        }
    }
}
