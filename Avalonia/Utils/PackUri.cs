// -----------------------------------------------------------------------
// <copyright file="PackUri.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PackUri
    {
        private Uri uri;

        public PackUri(Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                throw new NotSupportedException("Absolute pack URIs not yet supported.");
            }

            this.uri = uri;
        }

        public string Assembly
        {
            get
            {
                string s = this.uri.OriginalString;
                int component = s.IndexOf(";component");

                if (component == -1)
                {
                    return null;
                }
                else if (s.StartsWith("/"))
                {
                    return s.Substring(1, component - 1);
                }
                else
                {
                    throw new InvalidOperationException("Not a valid pack URI.");
                }
            }
        }

        public string Path
        {
            get
            {
                string s = this.uri.OriginalString;
                int component = s.IndexOf(";component");

                if (component == -1)
                {
                    return s;
                }
                else if (s.StartsWith("/"))
                {
                    return s.Substring(component + 10);
                }
                else
                {
                    throw new InvalidOperationException("Not a valid pack URI.");
                }
            }
        }

        public string GetAbsolutePath()
        {
            string path = this.Path;

            if (path.StartsWith("/"))
            {
                return path.Substring(1);
            }
            else
            {
                return path;
            }
        }
    }
}
