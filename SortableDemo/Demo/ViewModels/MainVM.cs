using MintPlayer.Xamarin.Forms.SortableListView.Demo.Models;
using MintPlayer.Xamarin.Forms.SortableListView.Demo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MintPlayer.Xamarin.Forms.SortableListView.Demo.ViewModels
{
    public class MainVM : BaseModel
    {
        public MainVM()
        {
            LoadPlayersCommand = new Command(OnLoadPlayers);
        }

        #region Players
        private ObservableCollection<Person> players;
        public ObservableCollection<Person> Players
        {
            get => players;
            set => SetProperty(ref players, value);
        }
        #endregion

        public ICommand LoadPlayersCommand { get; }

        private void OnLoadPlayers(object obj)
        {
            Players = new ObservableCollection<Person>(PlayerService.GetPlayers());
        }
    }
}
