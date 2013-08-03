// -----------------------------------------------------------------------
// <copyright file="XamlReader.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Media
{
    using System;
    using System.IO;
    using System.Windows.Markup;
    using System.Xaml;
    using System.Xml;
    using Avalonia.Controls;
    using Avalonia.Data;

    public class XamlReader
    {
        public static object Load(Stream stream)
        {
            XmlReader xml = XmlReader.Create(stream);
            return Load(xml);
        }

        public static object Load(XmlReader reader)
        {
            XamlXmlReader xaml = new XamlXmlReader(reader);
            return Load(xaml);
        }

        public static object Load(System.Xaml.XamlReader reader)
        {
            XamlObjectWriter writer = new XamlObjectWriter(
                new XamlSchemaContext(),
                new XamlObjectWriterSettings
                {
                    XamlSetValueHandler = SetValue,
                });

            while (reader.Read())
            {
                writer.WriteNode(reader);
            }

            object result = writer.Result;
            DependencyObject dependencyObject = result as DependencyObject;

            if (dependencyObject != null)
            {
                NameScope.SetNameScope(dependencyObject, writer.RootNameScope);
            }

            return result;
        }

        [AvaloniaSpecific]
        public static void Load(string url, object component)
        {
            // Yep, we're only handling files for the moment.
            using (FileStream stream = new FileStream(url, FileMode.Open))
            {
                Load(stream, component);
            }
        }

        [AvaloniaSpecific]
        public static void Load(Stream stream, object component)
        {
            DependencyObject dependencyObject = component as DependencyObject;
            NameScope nameScope = new NameScope();

            if (dependencyObject != null)
            {
                NameScope.SetNameScope(dependencyObject, nameScope);
            }

            XmlReader xml = XmlReader.Create(stream);
            XamlXmlReader reader = new XamlXmlReader(xml);
            XamlObjectWriter writer = new XamlObjectWriter(
                new XamlSchemaContext(),
                new XamlObjectWriterSettings
                {
                    RootObjectInstance = component,
                    ExternalNameScope = nameScope,
                    RegisterNamesOnExternalNamescope = true,
                    XamlSetValueHandler = SetValue,
                });

            while (reader.Read())
            {
                writer.WriteNode(reader);
            }
        }

        private static void SetValue(object sender, XamlSetValueEventArgs e)
        {
            BindingBase binding = e.Value as BindingBase;

            if (binding != null)
            {
                DependencyProperty dp = DependencyObject.FromName(
                    e.Member.DeclaringType.UnderlyingType, 
                    e.Member.Name);
                ((FrameworkElement)sender).SetBinding(dp, binding);
                e.Handled = true;
            }
        }
    }
}
