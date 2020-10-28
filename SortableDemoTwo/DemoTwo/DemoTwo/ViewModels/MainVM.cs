using MintPlayer.Xamarin.Forms.SortableListView.DemoTwo.Models;
using MintPlayer.Xamarin.Forms.SortableListView.DemoTwo.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MintPlayer.Xamarin.Forms.SortableListView.DemoTwo.ViewModels
{
    public class MainVM : BaseModel
    {
        public MainVM()
        {
            LoadPlayersCommand = new Command(OnLoadPlayers);
        }

        #region Players
        private ObservableCollection<Person> players = new ObservableCollection<Person>();
        public ObservableCollection<Person> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }
        #endregion
        #region AllowReordering
        private bool allowReordering = true;
        public bool AllowReordering
        {
            get => allowReordering;
            set => SetProperty(ref allowReordering, value);
        }
        #endregion

        public ICommand LoadPlayersCommand { get; }

        private void OnLoadPlayers(object obj)
        {
            var players = PlayerService.GetPlayers();
            foreach (var player in players)
                this.players.Add(player);
        }
    }
}
