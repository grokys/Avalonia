using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public class Binding : BindingBase
    {
        public Binding()
        {
        }

        public Binding(string path)
        {
            this.Path = new PropertyPath(path);
        }

        public PropertyPath Path { get; set; }
        public object Source { get; set; }
    }
}
