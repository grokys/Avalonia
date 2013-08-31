// -----------------------------------------------------------------------
// <copyright file="DataTemplateKey.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    public class DataTemplateKey : TemplateKey
    {
        public DataTemplateKey()
            : base(TemplateType.DataTemplate)
        {
        }

        public DataTemplateKey(object dataType)
            : base(TemplateType.DataTemplate, dataType)
        {
        }
    }
}
