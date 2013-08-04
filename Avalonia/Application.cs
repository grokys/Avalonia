// -----------------------------------------------------------------------
// <copyright file="Application.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using Avalonia.Data;
    using Avalonia.Media;
    using Avalonia.Threading;

    public class Application : DispatcherObject
    {
        static Application()
        {
            RegisterDependencyProperties();
            GenericTheme = new ResourceDictionary();
            LoadComponent(GenericTheme, new Uri("Themes/Generic.xaml", UriKind.Relative));
        }

        public Application()
        {
            Application.Current = this;
            this.Resources = new ResourceDictionary();            
        }

        public static Application Current 
        { 
            get; 
            private set; 
        }

        public Window MainWindow 
        { 
            get; 
            set; 
        }

        public ResourceDictionary Resources
        {
            get;
            private set;
        }

        internal static ResourceDictionary GenericTheme
        {
            get;
            private set;
        }

        public static void LoadComponent(object component, Uri resourceLocator)
        {
            XamlReader.Load(resourceLocator.OriginalString, component);
        }
        
        public object FindResource(object resourceKey)
        {
            object result = this.Resources[resourceKey];

            if (result == null)
            {
                throw new ResourceReferenceKeyNotFoundException(
                    string.Format("'{0}' resource not found", resourceKey),
                    resourceKey);
            }

            return result;
        }
        
        public void Run()
        {
            this.Run(this.MainWindow);
        }

        public void Run(Window window)
        {
            if (window != null)
            {
                window.Closed += (s, e) => this.Dispatcher.InvokeShutdown();
                window.Show();
            }

            if (this.MainWindow == null)
            {
                this.MainWindow = window;
            }

            Dispatcher.Run();
        }

        /// <summary>
        /// Ensures that all dependency properties are registered.
        /// </summary>
        private static void RegisterDependencyProperties()
        {
            IEnumerable<Type> types = from type in Assembly.GetCallingAssembly().GetTypes()
                                      where typeof(DependencyObject).IsAssignableFrom(type)
                                      select type;

            BindingFlags flags = BindingFlags.Public | BindingFlags.Static;

            foreach (Type type in types)
            {
                FieldInfo firstStaticField = type.GetFields(flags).FirstOrDefault();

                if (firstStaticField != null)
                {
                    object o = firstStaticField.GetValue(null);
                }
            }
        }
    }
}
