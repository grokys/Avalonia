// -----------------------------------------------------------------------
// <copyright file="CollectionViewSource.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class CollectionViewSource : DependencyObject
    {
        private static ConditionalWeakTable<object, ICollectionView> defaultViews =
            new ConditionalWeakTable<object, ICollectionView>();

        public static ICollectionView GetDefaultView(object source)
        {
            ICollectionView result = null;

            if (source != null && !defaultViews.TryGetValue(source, out result))
            {
                IList list = source as IList;
                IEnumerable enumerable = source as IEnumerable;

                if (list != null)
                {
                    result = new ListCollectionView(list);
                }
                else if (enumerable != null)
                {
                    result = new EnumerableCollectionView(enumerable);
                }

                if (result != null)
                {
                    defaultViews.Add(source, result);
                }
            }

            return result;
        }
    }
}
