// -----------------------------------------------------------------------
// <copyright file="IItemContainerGenerator.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    using System;

    public interface IItemContainerGenerator
    {
        DependencyObject GenerateNext(out bool isNewlyRealized);

        GeneratorPosition GeneratorPositionFromIndex(int itemIndex);
        
        ItemContainerGenerator GetItemContainerGeneratorForPanel(Panel panel);

        int IndexFromGeneratorPosition(GeneratorPosition position);
        
        void PrepareItemContainer(DependencyObject container);
        
        void Remove(GeneratorPosition position, int count);
        
        void RemoveAll();
        
        IDisposable StartAt(
            GeneratorPosition position,
            GeneratorDirection direction,
            bool allowStartAtRealizedItem);
    }
}
