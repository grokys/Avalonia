// -----------------------------------------------------------------------
// <copyright file="ItemsPanelTemplate.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    public class ItemsPanelTemplate : FrameworkTemplate
    {
        public ItemsPanelTemplate()
        {
        }

        public ItemsPanelTemplate(FrameworkElementFactory root)
        {
            this.VisualTree = root;
        }
    }
}
