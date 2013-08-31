// -----------------------------------------------------------------------
// <copyright file="DataTemplate.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System.Windows.Markup;

    [DictionaryKeyPropertyAttribute("DataTemplateKey")]
    public class DataTemplate : FrameworkTemplate
    {
        public DataTemplate()
        {
            this.Triggers = new TriggerCollection();
        }

        public DataTemplate(object dataType)
            : this()
        {
            this.DataType = dataType;
        }

        public object DataTemplateKey
        {
            get { return (this.DataType != null) ? new DataTemplateKey(this.DataType) : null; }
        }

        [AmbientAttribute]
        public object DataType 
        { 
            get; 
            set; 
        }

        public TriggerCollection Triggers
        {
            get;
            private set;
        }
    }
}
