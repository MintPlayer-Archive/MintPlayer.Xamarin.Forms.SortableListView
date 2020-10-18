//using MintPlayer.Xamarin.Forms.SortableListView.Platforms.Android;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;

//[assembly: ResolutionGroupName("MintPlayer")]
//[assembly: ExportEffect(typeof(MintPlayer.Xamarin.Forms.SortableListView.Demo.Droid.SortableListViewEffect), nameof(MintPlayer.Xamarin.Forms.SortableListView.Demo.Droid.SortableListViewEffect))]
//namespace MintPlayer.Xamarin.Forms.SortableListView.Demo.Droid
//{
//    public class SortableListViewEffect : PlatformEffect
//    {
//        private DragListAdapter dragListAdapter = null;
//        public SortableListViewEffect()
//        {
//        }

//        protected override void OnAttached()
//        {
//            var element = Element as ListView;

//            if (Control is global::Android.Widget.ListView listView)
//            {
//                dragListAdapter = new DragListAdapter(listView, element);
//                listView.Adapter = dragListAdapter;
//                listView.SetOnDragListener(dragListAdapter);
//                listView.OnItemLongClickListener = dragListAdapter;
//            }
//        }

//        protected override void OnDetached()
//        {
//            if (Control is global::Android.Widget.ListView listView)
//            {
//                listView.Adapter = dragListAdapter.WrappedAdapter;

//                // TODO: Remove the attached listeners
//            }
//        }

//        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
//        {
//            if (args.PropertyName == Sorting.IsSortableProperty.PropertyName)
//            {
//                dragListAdapter.DragDropEnabled = Sorting.GetIsSortable(Element);
//            }
//        }
//    }
//}
