using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Data
{
    public partial class CollectionViewSource : DependencyObject, IDeferRefresh, ISupportInitialize
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(CollectionViewSource),
                             new PropertyMetadata(null, SourceChanged));

        static void SourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((CollectionViewSource)o).OnSourceChanged(e.OldValue, e.NewValue);
        }

        public static readonly DependencyProperty ViewProperty =
            DependencyProperty.Register("View", typeof(ICollectionView), typeof(CollectionViewSource), null);

        CultureInfo culture;
        FilterEventHandler filter;

        public event FilterEventHandler Filter
        {
            add
            {
                filter += value;
                Refresh();
            }
            remove
            {
                filter -= value;
                Refresh();
            }
        }

        // FIXME: This should be a ConditionalWeakTable. DRT 232 shows that the CollectionViewSource
        // does not hold strong references to the source collections or the ICollectionViews it caches.
        // We could fake it with an IDictionary<WeakHandle<K>, WeakHandle<V>> maybe.
        IDictionary<object, ICollectionView> CachedViews
        {
            get;
            set;
        }

        public CultureInfo Culture
        {
            get { return culture; }
            set
            {
                culture = value;
                Refresh();
            }
        }

        int IDeferRefresh.DeferLevel
        {
            get;
            set;
        }

        Predicate<object> FilterCallback
        {
            get;
            set;
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get;
            private set;
        }

        bool Initializing
        {
            get;
            set;
        }

        public SortDescriptionCollection SortDescriptions
        {
            get;
            private set;
        }

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public ICollectionView View
        {
            get { return (ICollectionView)GetValue(ViewProperty); }
            private set { SetValue(ViewProperty, value); }
        }

        public CollectionViewSource()
        {
            CachedViews = new Dictionary<object, ICollectionView>();
            SortDescriptions = new SortDescriptionCollection();
            GroupDescriptions = new ObservableCollection<GroupDescription>();

            GroupDescriptions.CollectionChanged += (o, e) => Refresh();
            ((INotifyCollectionChanged)SortDescriptions).CollectionChanged += (o, e) => Refresh();
            FilterCallback = (o) =>
            {
                var h = filter;
                if (h != null)
                {
                    var args = new FilterEventArgs(o);
                    h(this, args);
                    return args.Accepted;
                }
                return true;
            };
        }

        public IDisposable DeferRefresh()
        {
            return new Deferrer(this);
        }

        void CreateView()
        {
            CreateView(Source);
        }

        void CreateView(object source)
        {
            if (source == null)
            {
                View = null;
            }
            else
            {
                ICollectionViewFactory factory = source as ICollectionViewFactory;
                if (factory != null)
                {
                    View = factory.CreateView();
                    if (View == null)
                        throw new InvalidOperationException(string.Format("ICollectionViewFactory.CreateView must not return null", source.GetType().Name));
                }
                else
                {
                    ICollectionView view = null;
                    if (CachedViews.TryGetValue(source, out view))
                    {
                        View = view;
                    }
                    else
                    {
                        view = CollectionView.Create((IEnumerable)source);
                        CachedViews.Add(source, view);
                        View = view;
                    }
                }
            }

            Refresh();
        }

        protected virtual void OnCollectionViewTypeChanged(Type oldCollectionViewType, Type newCollectionViewType)
        {

        }

        protected virtual void OnSourceChanged(object oldSource, object newSource)
        {
            if (newSource is ICollectionView)
                throw new ArgumentException("Source cannot be an ICollectionView");

            if (!Initializing)
                CreateView(newSource);
        }

        void Refresh()
        {
            if (((IDeferRefresh)this).DeferLevel != 0 || View == null)
                return;

            using (View.DeferRefresh())
            {
                if (Culture != null)
                    View.Culture = Culture;
                View.GroupDescriptions.Clear();
                for (int i = 0; i < GroupDescriptions.Count; i++)
                    View.GroupDescriptions.Add(GroupDescriptions[i]);

                View.SortDescriptions.Clear();
                for (int i = 0; i < SortDescriptions.Count; i++)
                    View.SortDescriptions.Add(SortDescriptions[i]);

                View.Filter = (filter == null) ? null : FilterCallback;
            }
        }

        void IDeferRefresh.Refresh()
        {
            Refresh();
        }

        void ISupportInitialize.BeginInit()
        {
            Initializing = true;
        }

        void ISupportInitialize.EndInit()
        {
            Initializing = false;
            CreateView();
        }
    }

}
