using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MintPlayer.Xamarin.Forms.SortableListView.DemoTwo
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new Pages.MainPage();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}
