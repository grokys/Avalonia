using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Avalonia
{
    public sealed class PropertyPath
    {
        public PropertyPath(string path)
        {
            this.Path = path;
            this.PathParameters = new Collection<object>();
        }

        public PropertyPath(string path, params object[] pathParameters)
        {
            this.Path = path;
            this.PathParameters = new Collection<object>(pathParameters);
        }

        public string Path { get; private set; }
        public Collection<Object> PathParameters { get; private set; }
    }
}
