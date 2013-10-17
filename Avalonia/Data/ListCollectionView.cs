// -----------------------------------------------------------------------
// <copyright file="ListCollectionView.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System.Collections;

    public class ListCollectionView : CollectionView
    {
        public ListCollectionView(IList list)
            : base(list)
        {
        }
    }
}
