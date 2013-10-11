using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public enum NewItemPlaceholderPosition
    {
        None,
        AtEnd = 2,
    }

    public interface IEditableCollectionView
    {
        object AddNew();
        void CancelEdit();
        void CancelNew();
        void CommitEdit();
        void CommitNew();
        void EditItem(object item);
        void Remove(object item);
        void RemoveAt(int index);
        bool CanAddNew { get; }
        bool CanCancelEdit { get; }
        bool CanRemove { get; }
        object CurrentAddItem { get; }
        object CurrentEditItem { get; }
        bool IsAddingNew { get; }
        bool IsEditingItem { get; }
        NewItemPlaceholderPosition NewItemPlaceholderPosition { get; set; }
    }
}
