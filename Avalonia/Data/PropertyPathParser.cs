using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    [AvaloniaSpecific]
    public class PropertyPathParser : IPropertyPathParser
    {
        public PropertyPathToken[] Parse(object source, string path)
        {
            List<PropertyPathToken> result = new List<PropertyPathToken>();
            string[] tokens = path.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                PropertyInfo property = source.GetType().GetProperty(token);

                if (property != null)
                {
                    result.Add(new PropertyPathToken(source, token));
                    source = property.GetValue(source);
                }
                else
                {
                    return null;
                }
            }

            result.Add(new PropertyPathToken(source, null));

            return result.ToArray();
        }
    }
}
