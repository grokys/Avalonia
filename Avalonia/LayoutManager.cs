// -----------------------------------------------------------------------
// <copyright file="LayoutManager.cs" company="Steven Kirk">
// Copyright 2013 MIT Licence. See licence.md for more information.
// </copyright>
// -----------------------------------------------------------------------

namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using Avalonia.Media;
    using Avalonia.Threading;

    internal class LayoutManager : DispatcherObject
    {
        private List<UIElement> entries = new List<UIElement>();
        private bool layoutPassQueued;

        static LayoutManager()
        {
            Instance = new LayoutManager();
        }

        public static LayoutManager Instance
        {
            get;
            private set;
        }

        public void QueueMeasure(UIElement e)
        {
            if (!this.entries.Contains(e))
            {
                this.entries.Add(e);
            }

            this.QueueLayoutPass();
        }

        public void QueueArrange(UIElement e)
        {
            this.QueueMeasure(e);
        }

        private void QueueLayoutPass()
        {
            if (!this.layoutPassQueued)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)this.DoLayout);
                this.layoutPassQueued = true;
            }
        }

        private void DoLayout()
        {
            List<ITopLevelWindow> windows = new List<ITopLevelWindow>();

            foreach (UIElement entry in this.entries)
            {
                ITopLevelWindow window = VisualTreeHelper.GetTopLevelWindow(entry);

                if (window != null)
                {
                    windows.Add(window);
                }
            }

            foreach (ITopLevelWindow window in windows)
            {
                window.DoLayoutPass();
            }

            this.entries.Clear();
            this.layoutPassQueued = false;
        }
    }
}
