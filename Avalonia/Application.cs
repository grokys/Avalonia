// -----------------------------------------------------------------------
// <copyright file="Application.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using Avalonia.Media;
    using Avalonia.Resources;
    using Avalonia.Threading;
    using Avalonia.Utils;

    public class Application : DispatcherObject
    {
        static Application()
        {
            RegisterDependencyProperties();
            GenericTheme = new ResourceDictionary();
            LoadComponent(GenericTheme, new Uri("/Avalonia;component/Themes/Generic.xaml", UriKind.Relative));
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

        public static StreamResourceInfo GetResourceStream(Uri uriResource)
        {
            if (uriResource == null)
            {
                throw new ArgumentNullException("uriResource");
            }

            if (uriResource.OriginalString == null)
            {
                throw new ArgumentException("uriResource.OriginalString is null.");
            }

            if (uriResource.IsAbsoluteUri)
            {
                if (uriResource.Scheme == "pack")
                {
                    throw new NotSupportedException("pack: resources not yet supported.");
                }
                else
                {
                    throw new ArgumentException("uriResource is not relative and doesn't use the pack: scheme.");
                }
            }

            PackUri pack = new PackUri(uriResource);
            string assemblyName = pack.Assembly;
            Assembly assembly = (assemblyName != null) ? Assembly.Load(assemblyName) : Assembly.GetEntryAssembly();
            string resourceName = assembly.GetName().Name + ".g";
            ResourceManager manager = new ResourceManager(resourceName, assembly);

            using (ResourceSet resourceSet = manager.GetResourceSet(CultureInfo.CurrentCulture, true, true))
            {
                Stream s = (Stream)resourceSet.GetObject(pack.GetAbsolutePath(), true);

                if (s == null)
                {
                    throw new IOException(
                        "The requested resource could not be found: " + 
                        uriResource.OriginalString);
                }

                return new StreamResourceInfo(s, null);
            }
        }

        public static void LoadComponent(object component, Uri resourceLocator)
        {
            if (!resourceLocator.IsAbsoluteUri)
            {
                StreamResourceInfo sri = GetResourceStream(resourceLocator);
                XamlReader.Load(sri.Stream, component);
            }
            else
            {
                throw new ArgumentException("Cannot use absolute URI.");
            }
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
