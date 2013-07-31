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

        private int id;
        private Type systemType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyObjectType"/> class.
        /// </summary>
        private DependencyObjectType(int id, Type systemType)
        {
            this.id = id;
            this.systemType = systemType;
        }

        public DependencyObjectType BaseType
        {
            get { return DependencyObjectType.FromSystemType(this.systemType.BaseType); }
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.systemType.Name; }
        }

        public Type SystemType
        {
            get { return this.systemType; }
        }

        public static DependencyObjectType FromSystemType(Type systemType)
        {
            if (typeMap.ContainsKey(systemType))
            {
                return typeMap[systemType];
            }

            DependencyObjectType dot;

            typeMap[systemType] = dot = new DependencyObjectType(currentId++, systemType);

            return dot;
        }

        public bool IsInstanceOfType(DependencyObject dependencyObject)
        {
            return this.systemType.IsInstanceOfType(dependencyObject);
        }

        public bool IsSubclassOf(DependencyObjectType dependencyObjectType)
        {
            return this.systemType.IsSubclassOf(dependencyObjectType.SystemType);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}