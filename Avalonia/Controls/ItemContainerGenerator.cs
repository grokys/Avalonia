// -----------------------------------------------------------------------
// <copyright file="ItemContainerGenerator.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Avalonia.Controls.Primitives;
    using Avalonia.Utils;

    public sealed class ItemContainerGenerator : IRecyclingItemContainerGenerator
    {
        internal ItemContainerGenerator(ItemsControl owner)
        {
            this.Cache = new Queue<DependencyObject>();
            this.ContainerIndexMap = new DoubleKeyedDictionary<DependencyObject, int>();
            this.ContainerItemMap = new Dictionary<DependencyObject, object>();
            this.Owner = owner;
            this.RealizedElements = new RangeCollection();
        }

        public event ItemsChangedEventHandler ItemsChanged;

        internal GenerationState GenerationState
        {
            get;
            set;
        }

        private RangeCollection RealizedElements
        {
            get;
            set;
        }

        private Queue<DependencyObject> Cache
        {
            get;
            set;
        }

        private DoubleKeyedDictionary<DependencyObject, int> ContainerIndexMap
        {
            get;
            set;
        }

        private Dictionary<DependencyObject, object> ContainerItemMap
        {
            get;
            set;
        }

        private ItemsControl Owner
        {
            get;
            set;
        }

        private Panel Panel
        {
            get { return this.Owner.Panel; }
        }

        public DependencyObject ContainerFromIndex(int index)
        {
            DependencyObject container;
            this.ContainerIndexMap.TryMap(index, out container);
            return container;
        }

        public DependencyObject ContainerFromItem(object item)
        {
            if (item != null)
            {
                foreach (var v in this.ContainerItemMap)
                {
                    if (object.Equals(v.Value, item))
                    {
                        return v.Key;
                    }
                }
            }

            return null;
        }

        public GeneratorPosition GeneratorPositionFromIndex(int itemIndex)
        {
            if (itemIndex < 0)
            {
                return new GeneratorPosition(-1, 0);
            }
            else if (this.RealizedElements.Contains(itemIndex))
            {
                return new GeneratorPosition(this.RealizedElements.IndexOf(itemIndex), 0);
            }
            else if (itemIndex > this.Owner.Items.Count)
            {
                return new GeneratorPosition(-1, 0);
            }

            if (this.RealizedElements.Count == 0)
            {
                return new GeneratorPosition(-1, itemIndex + 1);
            }

            int index = -1;
            
            for (int i = 0; i < this.RealizedElements.Count; i++)
            {
                if (this.RealizedElements[i] > itemIndex)
                {
                    break;
                }

                index = i;
            }
            
            if (index == -1)
            {
                return new GeneratorPosition(index, itemIndex + 1);
            }
            else
            {
                return new GeneratorPosition(index, itemIndex - this.RealizedElements[index]);
            }
        }

        public int IndexFromContainer(DependencyObject container)
        {
            int index;
            
            if (!this.ContainerIndexMap.TryMap(container, out index))
            {
                index = -1;
            }

            return index;
        }

        public int IndexFromGeneratorPosition(GeneratorPosition position)
        {
            // We either have everything realised or nothing realised, so we can
            // simply just add Index and Offset together to get the right index (i think)
            if (position.Index == -1)
            {
                if (position.Offset < 0)
                {
                    return this.Owner.Items.Count + position.Offset;
                }
                else
                {
                    return position.Offset - 1;
                }
            }
            else
            {
                if (position.Index > this.Owner.Items.Count)
                {
                    return -1;
                }

                if (position.Index >= 0 && position.Index < this.RealizedElements.Count)
                {
                    return this.RealizedElements[position.Index] + position.Offset;
                }

                return position.Index + position.Offset;
            }
        }

        public object ItemFromContainer(DependencyObject container)
        {
            object item;
            this.ContainerItemMap.TryGetValue(container, out item);
            return item ?? DependencyProperty.UnsetValue;
        }

        DependencyObject IItemContainerGenerator.GenerateNext(out bool isNewlyRealized)
        {
            return this.GenerateNext(out isNewlyRealized);
        }

        ItemContainerGenerator IItemContainerGenerator.GetItemContainerGeneratorForPanel(Panel panel)
        {
            return this.GetItemContainerGeneratorForPanel(panel);
        }

        void IItemContainerGenerator.PrepareItemContainer(DependencyObject container)
        {
            this.PrepareItemContainer(container);
        }

        void IItemContainerGenerator.Remove(GeneratorPosition position, int count)
        {
            this.Remove(position, count);
        }

        void IItemContainerGenerator.RemoveAll()
        {
            this.RemoveAll();
        }

        IDisposable IItemContainerGenerator.StartAt(
            GeneratorPosition position,
            GeneratorDirection direction,
            bool allowStartAtRealizedItem)
        {
            return this.StartAt(position, direction, allowStartAtRealizedItem);
        }

        void IRecyclingItemContainerGenerator.Recycle(GeneratorPosition position, int count)
        {
            this.Recycle(position, count);
        }

        internal DependencyObject GenerateNext(out bool isNewlyRealized)
        {
            if (GenerationState == null)
            {
                throw new InvalidOperationException("Cannot call GenerateNext before calling StartAt");
            }

            int index;
            
            // This is relative to the realised elements.
            int startAt = GenerationState.Position.Index;
            
            if (startAt == -1)
            {
                if (GenerationState.Position.Offset < 0)
                {
                    index = this.Owner.Items.Count + GenerationState.Position.Offset;
                }
                else if (GenerationState.Position.Offset == 0)
                {
                    index = 0;
                }
                else
                {
                    index = GenerationState.Position.Offset - 1;
                }
            }
            else if (startAt >= 0 && startAt < this.RealizedElements.Count)
            {
                // We're starting relative to an already realised element
                index = this.RealizedElements[startAt] + GenerationState.Position.Offset;
            }
            else
            {
                index = -1;
            }

            bool alreadyRealized = this.RealizedElements.Contains(index);

            if (!GenerationState.AllowStartAtRealizedItem && alreadyRealized && GenerationState.Position.Offset == 0)
            {
                index += GenerationState.Step;
                alreadyRealized = this.RealizedElements.Contains(index);
            }

            if (index < 0 || index >= this.Owner.Items.Count)
            {
                isNewlyRealized = false;
                return null;
            }

            if (alreadyRealized)
            {
                GenerationState.Position = new GeneratorPosition(this.RealizedElements.IndexOf(index), GenerationState.Step);
                isNewlyRealized = false;

                return this.ContainerIndexMap[index];
            }

            DependencyObject container;
            object item = this.Owner.Items[index];
            
            if (this.Owner.IsItemItsOwnContainer(item))
            {
                container = (DependencyObject)item;
                isNewlyRealized = true;
            }
            else
            {
                if (this.Cache.Count == 0)
                {
                    container = this.Owner.GetContainerForItem();
                    isNewlyRealized = true;
                }
                else
                {
                    container = this.Cache.Dequeue();
                    isNewlyRealized = false;
                }
            }

            FrameworkElement f = container as FrameworkElement;

            if (f != null && !(item is UIElement))
            {
                f.DataContext = item;
            }

            this.RealizedElements.Add(index);
            this.ContainerIndexMap.Add(container, index);
            this.ContainerItemMap.Add(container, item);

            GenerationState.Position = new GeneratorPosition(this.RealizedElements.IndexOf(index), GenerationState.Step);
            
            return container;
        }

        internal ItemContainerGenerator GetItemContainerGeneratorForPanel(Panel panel)
        {
            // FIXME: Double check this, but i think it's right
            return panel == Panel ? this : null;
        }

        internal void OnOwnerItemsItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int itemCount;
            int itemUICount;
            GeneratorPosition oldPosition = new GeneratorPosition(-1, 0);
            GeneratorPosition position;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if ((e.NewStartingIndex + 1) != this.Owner.Items.Count)
                    {
                        this.MoveExistingItems(e.NewStartingIndex, 1);
                    }

                    itemCount = 1;
                    itemUICount = 0;
                    position = this.GeneratorPositionFromIndex(e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    itemCount = 1;
                    itemUICount = this.RealizedElements.Contains(e.OldStartingIndex) ? 1 : 0;
                    position = this.GeneratorPositionFromIndex(e.OldStartingIndex);
                    
                    if (itemUICount == 1)
                    {
                        this.Remove(position, 1);
                    }

                    this.MoveExistingItems(e.OldStartingIndex, -1);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (!this.RealizedElements.Contains(e.NewStartingIndex))
                    {
                        return;
                    }

                    itemCount = 1;
                    itemUICount = 1;
                    position = this.GeneratorPositionFromIndex(e.NewStartingIndex);
                    
                    this.Remove(position, 1);

                    bool fresh;
                    var newPos = this.GeneratorPositionFromIndex(e.NewStartingIndex);
                    using (this.StartAt(newPos, GeneratorDirection.Forward, true))
                    {
                        this.PrepareItemContainer(this.GenerateNext(out fresh));
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    itemCount = e.OldItems == null ? 0 : e.OldItems.Count;
                    itemUICount = this.RealizedElements.Count;
                    position = new GeneratorPosition(-1, 0);
                    this.RemoveAll();
                    break;

                default:
                    Console.WriteLine("*** Critical error in ItemContainerGenerator.OnOwnerItemsItemsChanged. NotifyCollectionChangedAction.{0} is not supported", e.Action);
                    return;
            }

            ItemsChangedEventArgs args = new ItemsChangedEventArgs
            {
                Action = e.Action,
                ItemCount = itemCount,
                ItemUICount = itemUICount,
                OldPosition = oldPosition,
                Position = position
            };

            var h = this.ItemsChanged;

            if (h != null)
            {
                h(this, args);
            }
        }

        internal void PrepareItemContainer(DependencyObject container)
        {
            var index = this.ContainerIndexMap[container];
            var item = this.Owner.Items[index];

            this.Owner.PrepareContainerForItem(container, item);
        }

        internal void Remove(GeneratorPosition position, int count)
        {
            this.CheckOffsetAndRealized(position, count);

            int index = this.IndexFromGeneratorPosition(position);
            for (int i = 0; i < count; i++)
            {
                var container = this.ContainerIndexMap[index + i];
                var item = this.ContainerItemMap[container];
                this.ContainerIndexMap.Remove(container, index + i);
                this.ContainerItemMap.Remove(container);
                this.RealizedElements.Remove(index + i);
                this.Owner.ClearContainerForItem(container, item);
            }
        }

        internal IDisposable StartAt(
            GeneratorPosition position,
            GeneratorDirection direction,
            bool allowStartAtRealizedItem)
        {
            if (GenerationState != null)
            {
                throw new InvalidOperationException("Cannot call StartAt while a generation operation is in progress");
            }

            GenerationState = new GenerationState
            {
                AllowStartAtRealizedItem = allowStartAtRealizedItem,
                Direction = direction,
                Position = position,
                Generator = this
            };

            return GenerationState;
        }

        internal void Recycle(GeneratorPosition position, int count)
        {
            this.CheckOffsetAndRealized(position, count);

            int index = this.IndexFromGeneratorPosition(position);
            
            for (int i = 0; i < count; i++)
            {
                this.Cache.Enqueue(this.ContainerIndexMap[index + i]);
            }

            this.Remove(position, count);
        }

        internal void RemoveAll()
        {
            foreach (var pair in this.ContainerItemMap)
            {
                this.Owner.ClearContainerForItem(pair.Key, pair.Value);
            }
 
            this.RealizedElements.Clear();
            this.ContainerIndexMap.Clear();
            this.ContainerItemMap.Clear();
        }

        private void CheckOffsetAndRealized(GeneratorPosition position, int count)
        {
            if (position.Offset != 0)
            {
                throw new ArgumentException("position.Offset must be zero as the position must refer to a realized element.");
            }

            int index = this.IndexFromGeneratorPosition(position);
            int rangeIndex = this.RealizedElements.FindRangeIndexForValue(index);
            RangeCollection.Range range = this.RealizedElements.Ranges[rangeIndex];

            if (index < range.Start || (index + count) > range.Start + range.Count)
            {
                throw new InvalidOperationException("Only items which have been Realized can be removed.");
            }
        }

        private void MoveExistingItems(int index, int offset)
        {
            // This is a little horrible. I should really collapse the existing
            // RangeCollection so that every > the current index is decremented by 1.
            // This is easier for now though. I may think of a better way later on.
            RangeCollection newRanges = new RangeCollection();
            List<int> list = new List<int>();

            for (int i = 0; i < this.RealizedElements.Count; i++)
            {
                list.Add(this.RealizedElements[i]);
            }

            if (offset > 0)
            {
                list.Reverse();
            }

            foreach (int i in list)
            {
                int oldIndex = i;
             
                if (oldIndex < index)
                {
                    newRanges.Add(oldIndex);
                }
                else
                {
                    newRanges.Add(oldIndex + offset);
                    var container = this.ContainerIndexMap[oldIndex];
                    this.ContainerIndexMap.Remove(container, oldIndex);
                    this.ContainerIndexMap.Add(container, oldIndex + offset);
                }
            }

            this.RealizedElements = newRanges;
        }
    }
}
