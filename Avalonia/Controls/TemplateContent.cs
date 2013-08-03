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

        internal TemplateContent(XamlNodeList nodeList)
        {
            this.nodeList = nodeList;
        }

        internal object Load()
        {
            return Avalonia.Media.XamlReader.Load(this.nodeList.GetReader());
        }
    }
}
