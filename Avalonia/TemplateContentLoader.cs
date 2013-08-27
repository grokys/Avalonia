// -----------------------------------------------------------------------
// <copyright file="TemplateContentLoader.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Xaml;
    using Avalonia.Controls;

    public class TemplateContentLoader : XamlDeferringLoader
    {
        public override object Load(XamlReader xamlReader, IServiceProvider serviceProvider)
        {
            IXamlSchemaContextProvider schema = (IXamlSchemaContextProvider)serviceProvider.GetService(typeof(IXamlSchemaContextProvider));
            XamlNodeList nodeList = new XamlNodeList(schema.SchemaContext);
            Dictionary<string, Type> typesByName = new Dictionary<string, Type>();
            bool nextValueIsName = false;
            Type currentType = null;

            using (XamlWriter writer = nodeList.Writer)
            {
                while (xamlReader.Read())
                {
                    switch (xamlReader.NodeType)
                    {
                        case XamlNodeType.StartObject:
                            currentType = xamlReader.Type.UnderlyingType;
                            break;

                        case XamlNodeType.StartMember:
                            // HACK: This matches any Name property. Should probably just match
                            // FrameworkElement and x:Name but this'll do for now...
                            if (xamlReader.Member.Name == "Name")
                            {
                                nextValueIsName = true;
                            }

                            break;

                        case XamlNodeType.Value:
                            if (nextValueIsName)
                            {
                                typesByName.Add((string)xamlReader.Value, currentType);
                                nextValueIsName = false;
                            }
                            
                            break;
                    }

                    writer.WriteNode(xamlReader);
                }
            }

            return new TemplateContent(nodeList, typesByName);
        }

        public override XamlReader Save(object value, IServiceProvider serviceProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}
