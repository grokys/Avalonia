// -----------------------------------------------------------------------
// <copyright file="PropertyPathParser.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Parses a property path into a set of <see cref="PropertyPathToken"/>s.
    /// </summary>
    internal class PropertyPathParser : IPropertyPathParser
    {
        /// <summary>
        /// Parses the property path.
        /// </summary>
        /// <param name="source">The source object at which the property path starts.</param>
        /// <param name="path">The property path.</param>
        /// <returns>
        /// A chain of <see cref="PropertyPathToken"/>s describing the objects referred to in the
        /// property path.
        /// </returns>
        public IEnumerable<PropertyPathToken> Parse(object source, string path)
        {
            Validate(source, path);

            StringReader reader = new StringReader(path);
            string token = string.Empty;
            int read;
            char c;

            if (!string.IsNullOrWhiteSpace(path))
            {
                do
                {
                    read = reader.Read();
                    c = (char)read;

                    if (read == -1 || c == '.')
                    {
                        object next;
                        bool found = GetPropertyValue(source, token, out next);

                        yield return new PropertyPathToken
                        {
                            Type = found ? PropertyPathTokenType.Valid : PropertyPathTokenType.Broken,
                            Object = source,
                            PropertyName = token,
                        };

                        if (!found)
                        {
                            yield break;
                        }

                        source = next;
                        token = string.Empty;
                    }
                    else
                    {
                        token += c;
                    }
                }
                while (read != -1);
            }

            yield return new PropertyPathToken
            {
                Type = PropertyPathTokenType.FinalValue,
                Object = source,
            };
        }

        private static bool GetPropertyValue(object source, string name, out object value)
        {
            if (source != null)
            {
                PropertyInfo p = source.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);

                if (p != null)
                {
                    value = p.GetValue(source);
                    return true;
                }
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Validates the arguments to <see cref="Parse."/>
        /// </summary>
        /// <param name="source">The source object at which the property path starts.</param>
        /// <param name="path">The property path.</param>
        private static void Validate(object source, string path)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            else if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            else if (path.Contains('['))
            {
                throw new NotSupportedException("Property path indexers not yet supported.");
            }
            else if (path.Contains('('))
            {
                throw new NotSupportedException("Partially qualified property paths not yet supported.");
            }
            else if (path.Contains('/'))
            {
                throw new NotSupportedException("Source traversal in property paths not yet supported.");
            }
            else if (path.Contains('\\') || path.Contains('^'))
            {
                throw new NotSupportedException("Escape characters in property paths not yet supported.");
            }
        }
    }
}
