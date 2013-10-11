// -----------------------------------------------------------------------
// <copyright file="SortDescription.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum ListSortDirection
    {
        Ascending,
        Descending,
    }

    public struct SortDescription
    {
        private string sortPropertyName;
        private ListSortDirection sortDirection;
        private bool isSealed;

        public SortDescription(string propertyName, ListSortDirection direction)
        {
            if (direction != ListSortDirection.Ascending && direction != ListSortDirection.Descending)
                throw new ArgumentException("direction");

            sortPropertyName = propertyName;
            sortDirection = direction;
            isSealed = false;
        }

        public static bool operator !=(SortDescription sd1, SortDescription sd2)
        {
            return !(sd1 == sd2);
        }

        public static bool operator ==(SortDescription sd1, SortDescription sd2)
        {
            return sd1.sortDirection == sd2.sortDirection && sd1.sortPropertyName == sd2.sortPropertyName;
        }

        public ListSortDirection Direction
        {
            get { return sortDirection; }
            set
            {
                if (isSealed)
                    throw new InvalidOperationException("Cannot change Direction once the SortDescription has been sealed.");

                if (value != ListSortDirection.Ascending && value != ListSortDirection.Descending)
                    throw new ArgumentException("direction");

                sortDirection = value;
            }
        }

        public bool IsSealed
        {
            get { return isSealed; }
        }

        public string PropertyName
        {
            get { return sortPropertyName; }
            set
            {
                if (isSealed)
                    throw new InvalidOperationException("Cannot change Direction once the SortDescription has been sealed.");

                sortPropertyName = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SortDescription))
                return false;
            return ((SortDescription)obj) == this;
        }

        public override int GetHashCode()
        {
            if (sortPropertyName == null)
                return sortDirection.GetHashCode();
            return sortPropertyName.GetHashCode() ^ sortDirection.GetHashCode();
        }

        internal void Seal()
        {
            isSealed = true;
        }
    }
}
