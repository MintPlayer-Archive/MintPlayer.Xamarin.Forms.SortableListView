using System.Linq;
using Xamarin.Forms;

namespace MintPlayer.Xamarin.Forms.SortableListView.Platforms.Common
{
    public static class Sorting
    {
        public static readonly BindableProperty IsSortableProperty =
            BindableProperty.CreateAttached("IsSortable", typeof(bool), typeof(SortableListViewEffect), false,
                propertyChanged: OnIsSortableChanged);

        public static bool GetIsSortable(BindableObject view)
        {
            return (bool)view.GetValue(IsSortableProperty);
        }

        public static void SetIsSortable(BindableObject view, bool value)
        {
            view.SetValue(IsSortableProperty, value);
        }

        static void OnIsSortableChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as ListView;
            if (view == null)
            {
                return;
            }

            if (!view.Effects.Any(item => item is SortableListViewEffect))
            {
                view.Effects.Add(new SortableListViewEffect());
            }
        }
    }
}