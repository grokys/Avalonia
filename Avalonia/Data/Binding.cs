// -----------------------------------------------------------------------
// <copyright file="Binding.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public enum BindingMode
    {
        OneWay = 1,
        OneTime = 2,
        TwoWay = 3
    }

    public class Binding : BindingBase, ISupportInitialize
    {
        private bool bindsDirectlyToSource;
        
        private IValueConverter converter;
        
        private CultureInfo converterCulture;
        
        private object converterParameter;
        
        private string elementName;
        
        private PropertyPath path;
        
        private BindingMode mode;
        
        private bool notifyOnError;
        
        private bool validatesOnExceptions;
        
        private object source;
        
        private UpdateSourceTrigger trigger;
        
        private RelativeSource relativeSource;
        
        private bool validatesOnDataErrors;
        
        private bool validatesOnNotifyDataErrors;

        public Binding()
            : this("")
        {
        }

        public Binding(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            Mode = BindingMode.OneWay;
            Path = new PropertyPath(path);
            ValidatesOnNotifyDataErrors = true;
            UpdateSourceTrigger = UpdateSourceTrigger.Default;
        }

        public bool BindsDirectlyToSource
        {
            get 
            { 
                return this.bindsDirectlyToSource; 
            }
            
            set
            {
                this.CheckSealed();
                this.bindsDirectlyToSource = value;
            }
        }

        public IValueConverter Converter
        {
            get 
            {
                return this.converter; 
            }
            
            set
            {
                this.CheckSealed();
                this.converter = value;
            }
        }

        public CultureInfo ConverterCulture
        {
            get 
            {
                return this.converterCulture; 
            }

            set
            {
                this.CheckSealed();
                this.converterCulture = value;
            }
        }

        public object ConverterParameter
        {
            get 
            {
                return this.converterParameter; 
            }

            set
            {
                this.CheckSealed();
                this.converterParameter = value;
            }
        }

        public string ElementName
        {
            get 
            {
                return this.elementName; 
            }

            set
            {
                this.CheckSealed();

                if (this.Source != null || this.RelativeSource != null)
                {
                    throw new InvalidOperationException("ElementName cannot be set if either RelativeSource or Source is set");
                }

                this.elementName = value;
            }
        }

        public BindingMode Mode
        {
            get 
            {
                return this.mode; 
            }

            set
            {
                this.CheckSealed();
                this.mode = value;
            }
        }

        public bool NotifyOnValidationError
        {
            get 
            {
                return this.notifyOnError; 
            }
            
            set
            {
                this.CheckSealed();
                this.notifyOnError = value;
            }
        }

        public RelativeSource RelativeSource
        {
            get 
            {
                return this.relativeSource; 
            }

            set
            {
                // FIXME: Check that the standard validation is done here
                this.CheckSealed();

                if (this.source != null || this.ElementName != null)
                {
                    throw new InvalidOperationException("RelativeSource cannot be set if either ElementName or Source is set");
                }

                this.relativeSource = value;
            }
        }

        [TypeConverter(typeof(PropertyPathConverter))]
        public PropertyPath Path
        {
            get
            {
                return this.path;
            }

            set
            {
                this.CheckSealed();
                this.path = value;
            }
        }

        public object Source
        {
            get 
            {
                return this.source; 
            }

            set
            {
                this.CheckSealed();
                
                if (this.ElementName != null || this.RelativeSource != null)
                {
                    throw new InvalidOperationException("Source cannot be set if either ElementName or RelativeSource is set");
                }

                this.source = value;
            }
        }

        public UpdateSourceTrigger UpdateSourceTrigger
        {
            get 
            {
                return this.trigger; 
            }

            set
            {
                this.CheckSealed();
                this.trigger = value;
            }
        }

        public bool ValidatesOnExceptions
        {
            get 
            {
                return this.validatesOnExceptions; 
            }

            set
            {
                this.CheckSealed();
                this.validatesOnExceptions = value;
            }
        }

        public bool ValidatesOnDataErrors
        {
            get 
            {
                return this.validatesOnDataErrors; 
            }

            set
            {
                this.CheckSealed();
                this.validatesOnDataErrors = value;
            }
        }

        public bool ValidatesOnNotifyDataErrors
        {
            get 
            {
                return this.validatesOnNotifyDataErrors; 
            }

            set
            {
                this.CheckSealed();
                this.validatesOnNotifyDataErrors = value;
            }
        }

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
        }
    }
}
