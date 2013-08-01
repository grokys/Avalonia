// -----------------------------------------------------------------------
// <copyright file="PropertyPathParser.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    [AvaloniaSpecific]
    public class PropertyPathParser : IPropertyPathParser
    {
        public PropertyPathToken[] Parse(object source, string path)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

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
