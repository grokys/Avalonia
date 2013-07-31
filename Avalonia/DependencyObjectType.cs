// -----------------------------------------------------------------------
// <copyright file="DependencyObjectType.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence
// See licence.md for more information
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;

    public class DependencyObjectType
    {
        private static Dictionary<Type, DependencyObjectType> typeMap = new Dictionary<Type, DependencyObjectType>();
        private static int currentId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyObjectType"/> class.
        /// </summary>
        private DependencyObjectType(int id, Type systemType)
        {
            this.Id = id;
            this.SystemType = systemType;
        }

        public DependencyObjectType BaseType
        {
            get { return DependencyObjectType.FromSystemType(this.SystemType.BaseType); }
        }

        public int Id
        {
            get;
            private set;
        }

        public string Name
        {
            get { return this.SystemType.Name; }
        }

        public Type SystemType
        {
            get;
            private set;
        }

        public static DependencyObjectType FromSystemType(Type systemType)
        {
            if (typeMap.ContainsKey(systemType))
            {
                return typeMap[systemType];
            }

            DependencyObjectType dot = new DependencyObjectType(currentId++, systemType);
            typeMap[systemType] = dot;
            return dot;
        }

        public bool IsInstanceOfType(DependencyObject dependencyObject)
        {
            return this.SystemType.IsInstanceOfType(dependencyObject);
        }

        public bool IsSubclassOf(DependencyObjectType dependencyObjectType)
        {
            return this.SystemType.IsSubclassOf(dependencyObjectType.SystemType);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}