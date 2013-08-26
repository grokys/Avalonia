// -----------------------------------------------------------------------
// <copyright file="TemplateContent.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xaml;

    public class TemplateContent
    {
        private XamlNodeList nodeList;

        private Dictionary<string, Type> typesByName;

        internal TemplateContent(XamlNodeList nodeList, Dictionary<string, Type> typesByName)
        {
            this.nodeList = nodeList;
            this.typesByName = typesByName;
        }

        internal Type GetTypeForName(string name)
        {
            Type result;

            if (!this.typesByName.TryGetValue(name, out result))
            {
                throw new KeyNotFoundException(string.Format(
                    "Element '{0}' not found in TemplateContent.",
                    name));
            }

            return result;
        }

        internal object Load()
        {
            return Avalonia.Media.XamlReader.Load(this.nodeList.GetReader());
        }
    }
}
