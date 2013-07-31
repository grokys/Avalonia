// -----------------------------------------------------------------------
// <copyright file="DependencyProperty.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;

    public sealed class DependencyProperty
    {
        public static readonly object UnsetValue = new object();
        private Dictionary<Type, PropertyMetadata> metadataByType = new Dictionary<Type, PropertyMetadata>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyProperty"/> class.
        /// </summary>
        private DependencyProperty(
                    bool isAttached,
                    string name,
                    Type propertyType,
                    Type ownerType,
                    PropertyMetadata defaultMetadata,
                    ValidateValueCallback validateValueCallback)
        {
            this.IsAttached = isAttached;
            this.DefaultMetadata = (defaultMetadata == null) ? new PropertyMetadata() : defaultMetadata;
            this.Name = name;
            this.OwnerType = ownerType;
            this.PropertyType = propertyType;
            this.ValidateValueCallback = validateValueCallback;
        }

        public bool ReadOnly { get; private set; }

        public PropertyMetadata DefaultMetadata { get; private set; }
        
        public string Name { get; private set; }
        
        public Type OwnerType { get; private set; }
        
        public Type PropertyType { get; private set; }
        
        public ValidateValueCallback ValidateValueCallback { get; private set; }

        public int GlobalIndex
        {
            get { throw new NotImplementedException(); }
        }

        internal bool IsAttached { get; set; }

        public static DependencyProperty Register(string name, Type propertyType, Type ownerType)
        {
            return Register(name, propertyType, ownerType, null, null);
        }

        public static DependencyProperty Register(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata typeMetadata)
        {
            return Register(name, propertyType, ownerType, typeMetadata, null);
        }

        public static DependencyProperty Register(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata typeMetadata,
            ValidateValueCallback validateValueCallback)
        {
            if (typeMetadata == null)
            {
                typeMetadata = new PropertyMetadata();

                if (propertyType.IsValueType && Nullable.GetUnderlyingType(propertyType) == null)
                {
                    typeMetadata.DefaultValue = Activator.CreateInstance(propertyType);
                }
            }

            DependencyProperty dp = new DependencyProperty(
                false, 
                name, 
                propertyType, 
                ownerType,
                typeMetadata, 
                validateValueCallback);

            DependencyObject.Register(ownerType, dp);

            dp.OverrideMetadata(ownerType, typeMetadata);

            return dp;
        }

        public static DependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType)
        {
            return RegisterAttached(name, propertyType, ownerType, null, null);
        }

        public static DependencyProperty RegisterAttached(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata defaultMetadata)
        {
            return RegisterAttached(name, propertyType, ownerType, defaultMetadata, null);
        }

        public static DependencyProperty RegisterAttached(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata defaultMetadata,
            ValidateValueCallback validateValueCallback)
        {
            DependencyProperty dp = new DependencyProperty(
                true, 
                name, 
                propertyType, 
                ownerType,
                defaultMetadata, 
                validateValueCallback);
            DependencyObject.Register(ownerType, dp);
            return dp;
        }

        public static DependencyPropertyKey RegisterAttachedReadOnly(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata defaultMetadata)
        {
            throw new NotImplementedException("RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata)");
        }

        public static DependencyPropertyKey RegisterAttachedReadOnly(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata defaultMetadata,
            ValidateValueCallback validateValueCallback)
        {
            throw new NotImplementedException("RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, PropertyMetadata defaultMetadata, ValidateValueCallback validateValueCallback)");
        }

        public static DependencyPropertyKey RegisterReadOnly(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata typeMetadata)
        {
            return RegisterReadOnly(name, propertyType, ownerType, typeMetadata, null);
        }

        public static DependencyPropertyKey RegisterReadOnly(
            string name, 
            Type propertyType, 
            Type ownerType,
            PropertyMetadata typeMetadata,
            ValidateValueCallback validateValueCallback)
        {
            DependencyProperty prop = Register(name, propertyType, ownerType, typeMetadata, validateValueCallback);
            prop.ReadOnly = true;
            return new DependencyPropertyKey(prop);
        }

        public DependencyProperty AddOwner(Type ownerType)
        {
            return this.AddOwner(ownerType, null);
        }

        public DependencyProperty AddOwner(Type ownerType, PropertyMetadata typeMetadata)
        {
            if (typeMetadata == null)
            {
                typeMetadata = new PropertyMetadata();
            }

            this.OverrideMetadata(ownerType, typeMetadata);

            // MS seems to always return the same DependencyProperty
            return this;
        }

        public PropertyMetadata GetMetadata(Type forType)
        {
            if (this.metadataByType.ContainsKey(forType))
            {
                return this.metadataByType[forType];
            }
            else
            {
                return this.DefaultMetadata;
            }
        }

        public PropertyMetadata GetMetadata(DependencyObject dependencyObject)
        {
            if (this.metadataByType.ContainsKey(dependencyObject.GetType()))
            {
                return this.metadataByType[dependencyObject.GetType()];
            }
            else
            {
                return this.DefaultMetadata;
            }
        }

        public PropertyMetadata GetMetadata(DependencyObjectType dependencyObjectType)
        {
            if (this.metadataByType.ContainsKey(dependencyObjectType.SystemType))
            {
                return this.metadataByType[dependencyObjectType.SystemType];
            }
            else
            {
                return this.DefaultMetadata;
            }
        }

        public bool IsValidType(object value)
        {
            return this.PropertyType.IsInstanceOfType(value);
        }

        public bool IsValidValue(object value)
        {
            if (!this.IsValidType(value))
            {
                return false;
            }

            if (this.ValidateValueCallback == null)
            {
                return true;
            }

            return this.ValidateValueCallback(value);
        }

        public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata)
        {
            if (forType == null)
            {
                throw new ArgumentNullException("forType");
            }

            if (typeMetadata == null)
            {
                throw new ArgumentNullException("typeMetadata");
            }

            if (this.ReadOnly)
            {
                throw new InvalidOperationException(string.Format("Cannot override metadata on readonly property '{0}' without using a DependencyPropertyKey", this.Name));
            }

            typeMetadata.Merge(this.DefaultMetadata, this, forType);
            this.metadataByType.Add(forType, typeMetadata);
        }

        public void OverrideMetadata(Type forType, PropertyMetadata typeMetadata, DependencyPropertyKey key)
        {
            if (forType == null)
            {
                throw new ArgumentNullException("forType");
            }

            if (typeMetadata == null)
            {
                throw new ArgumentNullException("typeMetadata");
            }

            // further checking?  should we check
            // key.DependencyProperty == this?
            typeMetadata.Merge(this.DefaultMetadata, this, forType);
            this.metadataByType.Add(forType, typeMetadata);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.PropertyType.GetHashCode() ^ this.OwnerType.GetHashCode();
        }
    }
}