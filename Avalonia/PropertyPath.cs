// -----------------------------------------------------------------------
// <copyright file="PropertyPath.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    public sealed class PropertyPath
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyPath"/> class.
        /// </summary>
        public PropertyPath(string path)
        {
            this.Path = path;
            this.PathParameters = new Collection<object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyPath"/> class.
        /// </summary>
        public PropertyPath(string path, params object[] pathParameters)
        {
            this.Path = path;
            this.PathParameters = new Collection<object>(pathParameters);
        }

        public string Path { get; private set; }

        public Collection<object> PathParameters { get; private set; }
    }
}
