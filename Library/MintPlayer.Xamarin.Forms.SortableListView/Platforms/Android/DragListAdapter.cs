using MintPlayer.Xamarin.Forms.SortableListView.Platforms.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MintPlayer.Xamarin.Forms.SortableListView.Platforms.Android
{
    internal class DragListAdapter : global::Android.Widget.BaseAdapter, global::Android.Widget.IWrapperListAdapter, global::Android.Views.View.IOnDragListener, global::Android.Widget.AdapterView.IOnItemLongClickListener
    {
        private global::Android.Widget.IListAdapter _listAdapter;
        private global::Android.Widget.ListView _listView;
        private global::Xamarin.Forms.ListView _element;
        private List<global::Android.Views.View> _translatedItems = new List<global::Android.Views.View>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DragListAdapter"/> class.
        /// </summary>
        /// <param name="listAdapter">
        /// The list adapter.
        /// </param>
        /// <param name="listView">
        /// The list view.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        public DragListAdapter(global::Android.Widget.ListView listView, global::Xamarin.Forms.ListView element)
        {
            _listView = listView;
            _listAdapter = ((global::Android.Widget.IWrapperListAdapter)_listView.Adapter).WrappedAdapter;
            _element = element;
        }

        public bool DragDropEnabled { get; set; } = false;

        #region IWrapperListAdapter Members

        public global::Android.Widget.IListAdapter WrappedAdapter => _listAdapter;

        public override int Count => WrappedAdapter.Count;

        public override bool HasStableIds => WrappedAdapter.HasStableIds;

        public override bool IsEmpty => WrappedAdapter.IsEmpty;

        public override int ViewTypeCount => WrappedAdapter.ViewTypeCount;

        public override bool AreAllItemsEnabled() => WrappedAdapter.AreAllItemsEnabled();

        public override Java.Lang.Object GetItem(int position)
        {
            return WrappedAdapter.GetItem(position);
        }

        public override long GetItemId(int position)
        {
            return WrappedAdapter.GetItemId(position);
        }

        public override int GetItemViewType(int position)
        {
            return WrappedAdapter.GetItemViewType(position);
        }

        public override global::Android.Views.View GetView(int position, global::Android.Views.View convertView, global::Android.Views.ViewGroup parent)
        {
            var view = WrappedAdapter.GetView(position, convertView, parent);
            view.SetOnDragListener(this);
            return view;
        }

        public override bool IsEnabled(int position)
        {
            return WrappedAdapter.IsEnabled(position);
        }

        private global::Android.Database.DataSetObserver _observer;

        public override void RegisterDataSetObserver(global::Android.Database.DataSetObserver observer)
        {
            _observer = observer;
            base.RegisterDataSetObserver(observer);
            WrappedAdapter.RegisterDataSetObserver(observer);
        }

        public override void UnregisterDataSetObserver(global::Android.Database.DataSetObserver observer)
        {
            base.UnregisterDataSetObserver(observer);
            WrappedAdapter.UnregisterDataSetObserver(observer);
        }

        #endregion

        public bool OnDrag(global::Android.Views.View v, global::Android.Views.DragEvent e)
        {
            switch (e.Action)
            {
                case global::Android.Views.DragAction.Started:
                    break;
                case global::Android.Views.DragAction.Entered:
                    System.Diagnostics.Debug.WriteLine($"DragAction.Entered from {v.GetType()}");

                    if (!(v is global::Android.Widget.ListView))
                    {
                        var dragItem = (DragItem)e.LocalState;

                        var targetPosition = InsertOntoView(v, dragItem);

                        dragItem.Index = targetPosition;

                        // Keep a list of items that has translation so we can reset
                        // them once the drag'n'drop is finished.
                        _translatedItems.Add(v);
                        _listView.Invalidate();
                    }
                    break;
                case global::Android.Views.DragAction.Location:
                    //_currentPosition = (int)e.GetY();
                    //System.Diagnostics.Debug.WriteLine($"DragAction.Location from {v.GetType()} => {_currentPosition}");
                    break;
                case global::Android.Views.DragAction.Exited:
                    System.Diagnostics.Debug.WriteLine($"DragAction.Entered from {v.GetType()}");

                    if (!(v is global::Android.Widget.ListView))
                    {
                        var positionEntered = GetListPositionForView(v);
                        var item1 = _listAdapter.GetItem(positionEntered);

                        System.Diagnostics.Debug.WriteLine($"DragAction.Exited index {positionEntered}");
                    }
                    break;
                case global::Android.Views.DragAction.Drop:


                    System.Diagnostics.Debug.WriteLine($"DragAction.Drop from {v.GetType()}");

                    //}

                    break;
                case global::Android.Views.DragAction.Ended:
                    if (!(v is global::Android.Widget.ListView))
                    {
                        return false;
                    }

                    System.Diagnostics.Debug.WriteLine($"DragAction.Ended from {v.GetType()}");

                    var mobileItem = (DragItem)e.LocalState;

                    mobileItem.View.Visibility = global::Android.Views.ViewStates.Visible;

                    foreach (var view in _translatedItems)
                    {
                        view.TranslationY = 0;
                    }

                    _translatedItems.Clear();

                    var itemsSourceType = _element.ItemsSource.GetType();
                    if (itemsSourceType.IsGenericType && itemsSourceType.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
                    {
                        var elementType = itemsSourceType.GenericTypeArguments.First();
                        var method = typeof(ObservableCollection<>).MakeGenericType(elementType).GetMethod("Move");
                        method.Invoke(_element.ItemsSource, new object[] { mobileItem.OriginalIndex, mobileItem.Index });
                    }

                    break;
            }

            return true;
        }

        /// <summary>
        /// Handler for Long Click event from <paramref name="view"/>
        /// </summary>
        /// <param name="parent">
        /// The parent list view .
        /// </param>
        /// <param name="view">
        /// The view that triggered the long click event
        /// </param>
        /// <param name="position">
        /// The position of the view in the list (has to be normalized, includes headers).
        /// </param>
        /// <param name="id">
        /// The id of the item that triggered the event (must be bigger than 0 under normal circumstances).
        /// </param>
        /// <returns>
        /// The <see cref="bool"/> flag that identifies whether the event is handled.
        /// </returns>
        public bool OnItemLongClick(global::Android.Widget.AdapterView parent, global::Android.Views.View view, int position, long id)
        {
            var selectedItem = ((IList)_element.ItemsSource)[(int)id];

            DragItem dragItem = new DragItem(NormalizeListPosition(position), view, selectedItem);

            var data = global::Android.Content.ClipData.NewPlainText(string.Empty, string.Empty);

            global::Android.Views.View.DragShadowBuilder shadowBuilder = new global::Android.Views.View.DragShadowBuilder(view);

            view.Visibility = global::Android.Views.ViewStates.Invisible;

            if (global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.N)
            {
                view.StartDragAndDrop(data, shadowBuilder, dragItem, 0);
            }
            else
            {
                view.StartDrag(data, shadowBuilder, id, 0);
            }

            return true;
        }

        private int InsertOntoView(global::Android.Views.View view, DragItem item)
        {
            var positionEntered = GetListPositionForView(view);
            var correctedPosition = positionEntered;

            // If the view already has a translation, we need to adjust the position
            // If the view has a positive translation, that means that the current position
            // is actually one index down then where it started.
            // If the view has a negative translation, that means it actually moved
            // up previous now we will need to move it down.
            if (view.TranslationY > 0)
            {
                correctedPosition += 1;
            }
            else if (view.TranslationY < 0)
            {
                correctedPosition -= 1;
            }

            // If the current index of the dragging item is bigger than the target
            // That means the dragging item is moving up, and the target view should
            // move down, and vice-versa
            var translationCoef = item.Index > correctedPosition ? 1 : -1;

            // We translate the item as much as the height of the drag item (up or down)
            var translationTarget = view.TranslationY + (translationCoef * item.View.Height);

            var anim = global::Android.Animation.ObjectAnimator.OfFloat(view, "TranslationY", view.TranslationY, translationTarget);
            anim.SetDuration(100);
            anim.Start();

            return correctedPosition;
        }

        private int GetListPositionForView(global::Android.Views.View view)
        {
            return NormalizeListPosition(_listView.GetPositionForView(view));
        }

        private int NormalizeListPosition(int position)
        {
            return position - _listView.HeaderViewsCount;
        }

    }
}
