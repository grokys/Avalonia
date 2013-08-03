// -----------------------------------------------------------------------
// <copyright file="TemplateContentLoader.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Xaml;
    using Avalonia.Controls;

    public class TemplateContentLoader : XamlDeferringLoader
    {
        public override object Load(XamlReader xamlReader, IServiceProvider serviceProvider)
        {
            IXamlSchemaContextProvider schema = (IXamlSchemaContextProvider)serviceProvider.GetService(typeof(IXamlSchemaContextProvider));
            XamlNodeList nodeList = new XamlNodeList(schema.SchemaContext);

            using (XamlWriter writer = nodeList.Writer)
            {
                while (xamlReader.Read())
                {
                    writer.WriteNode(xamlReader);
                }
            }

            return new TemplateContent(nodeList);
        }

        public override XamlReader Save(object value, IServiceProvider serviceProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}
