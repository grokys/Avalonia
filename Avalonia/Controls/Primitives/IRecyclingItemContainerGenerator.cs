// -----------------------------------------------------------------------
// <copyright file="IRecyclingItemContainerGenerator.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls.Primitives
{
    public interface IRecyclingItemContainerGenerator : IItemContainerGenerator
    {
        void Recycle(GeneratorPosition position, int count);
    }
}
