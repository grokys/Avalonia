using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    class GroupEnumerator : IEnumerator<object>
    {

        public object Current
        {
            get;
            set;
        }

        public int CurrentGroupIndex
        {
            get;
            set;
        }

        CollectionViewGroup CurrentGroup
        {
            get;
            set;
        }

        List<object> Groups
        {
            get;
            set;
        }

        CollectionViewGroup Root
        {
            get;
            set;
        }

        public GroupEnumerator(CollectionViewGroup root)
        {
            Groups = new List<object>();
            Root = root;
            Reset();
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (CurrentGroup != null && CurrentGroupIndex < CurrentGroup.Items.Count)
            {
                Current = CurrentGroup.Items[CurrentGroupIndex++];
                return true;
            }

            while (Groups.Count > 0)
            {
                var group = Groups[0];
                Groups.RemoveAt(0);
                if (!(group is CollectionViewGroup))
                {
                    Current = group;
                    return true;
                }
                var g = (CollectionViewGroup)group;
                if (g.IsBottomLevel && g.Items.Count > 0)
                {
                    CurrentGroup = g;
                    Current = g.Items[0];
                    CurrentGroupIndex = 1;
                    return true;
                }
                else
                {
                    for (int i = 0; i < g.Items.Count; i++)
                        Groups.Insert(i, g.Items[i]);
                }
            }

            Current = null;
            return false;
        }

        public void Reset()
        {
            Groups.Clear();
            Groups.Add(Root);
        }
    }

}
