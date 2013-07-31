namespace Avalonia
{
    using System;
    using System.Collections.Generic;
    using Avalonia.Media;
    using Avalonia.Threading;

    internal class LayoutManager : DispatcherObject
    {
        private List<UIElement> entries = new List<UIElement>();
        private bool layoutPassQueued = false;

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
            if (!entries.Contains(e))
            {
                entries.Add(e);
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
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)DoLayout);
                this.layoutPassQueued = true;
            }
        }

        private void DoLayout()
        {
            foreach (UIElement entry in this.entries)
            {
                Window window = this.FindWindow(entry);

                if (window != null)
                {
                    window.DoMeasureArrange();
                }
            }

            this.layoutPassQueued = false;
        }

        private Window FindWindow(UIElement entry)
        {
            Visual visual = entry;

            while (visual != null && !(visual is Window))
            {
                visual = visual.VisualParent as Visual;
            }

            return visual as Window;
        }
    }
}
