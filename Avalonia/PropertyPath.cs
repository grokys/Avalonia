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
        private string path;
        private string expanded_path;

        private DependencyProperty property;

        public PropertyPath(string path, params object[] pathParameters)
        {
            if (pathParameters == null)
                throw new NullReferenceException();
            else if (pathParameters.Length > 0)
                throw new ArgumentOutOfRangeException();

            this.path = path;
        }

        internal PropertyPath(string path, string expanded_path)
        {
            this.path = path;
            this.expanded_path = expanded_path;
        }

        public PropertyPath(object parameter)
        {
            property = parameter as DependencyProperty;
            path = parameter as string;
        }

        public string Path
        {
            get { return property == null ? path : "(0)"; }
        }
 
        internal string ExpandedPath
        {
            get { return property == null ? expanded_path : "(0)"; }
        }
        
        internal string ParsePath
        {
            get
            {
                if (property != null)
                    return "(0)";
                if (expanded_path != null)
                    return expanded_path;
                return path;
            }
        }
    }

}
