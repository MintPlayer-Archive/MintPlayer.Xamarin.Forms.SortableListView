# MintPlayer.Xamarin.Forms.SortableListView
[![NuGet Version](https://img.shields.io/nuget/v/MintPlayer.Xamarin.Forms.SortableListView.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.Xamarin.Forms.SortableListView)
[![NuGet](https://img.shields.io/nuget/dt/MintPlayer.Xamarin.Forms.SortableListView.svg?style=flat)](https://www.nuget.org/packages/MintPlayer.Xamarin.Forms.SortableListView)
[![Build Status](https://travis-ci.org/MintPlayer/MintPlayer.Xamarin.Forms.SortableListView.svg?branch=master)](https://travis-ci.org/MintPlayer/MintPlayer.Xamarin.Forms.SortableListView)
![.NET Core](https://github.com/MintPlayer/MintPlayer.Xamarin.Forms.SortableListView/workflows/.NET%20Core/badge.svg)
[![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/b7c01f2a24624a1585b8efe0bbe17954)](https://www.codacy.com/gh/MintPlayer/MintPlayer.Xamarin.Forms.SortableListView/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=MintPlayer/MintPlayer.Xamarin.Forms.SortableListView&amp;utm_campaign=Badge_Grade)

This project contains an Effect for the Xamarin.Forms.ListView to make items reorderable

## Installation
### NuGet package manager
Open the NuGet package manager and install `MintPlayer.Xamarin.Forms.SortableListView` in your Xamarin.Forms project. It's not necessary to install the package in the platform projects.
### Package manager console
Install-Package MintPlayer.Xamarin.Forms.SortableListView

## Usage
### Simple
Apply the effect on a Xamarin.Forms.ListView

    <?xml version="1.0" encoding="utf-8" ?>
    <ContentPage ...
                 xmlns:effects="clr-namespace:MintPlayer.Xamarin.Forms.SortableListView.Platforms.Common;assembly=MintPlayer.Xamarin.Forms.SortableListView"
                 x:Class="MintPlayer.Xamarin.Forms.SortableListView.Demo.Pages.MainPage">
        <ListView ItemsSource="{Binding Players}" ... effects:Sorting.IsSortable="true" />
    </ContentPage>

### Advanced example
#### MainPage.xaml

    <?xml version="1.0" encoding="utf-8" ?>
    <ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
                 xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                 xmlns:vm="clr-namespace:MintPlayer.Xamarin.Forms.SortableListView.Demo.ViewModels"
                 xmlns:effects="clr-namespace:MintPlayer.Xamarin.Forms.SortableListView.Platforms.Common;assembly=MintPlayer.Xamarin.Forms.SortableListView"
                 x:Class="MintPlayer.Xamarin.Forms.SortableListView.Demo.Pages.MainPage">
        <ContentPage.BindingContext>
            <vm:MainVM x:Name="viewModel" />
        </ContentPage.BindingContext>
        <StackLayout HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">
            <Label Padding="10" HorizontalOptions="FillAndExpand" VerticalOptions="Start">Hold down an item for 1 second, in order to move it around.</Label>
            <StackLayout Orientation="Horizontal" HorizontalOptions="FillAndExpand" VerticalOptions="Start">
                <Switch IsToggled="{Binding AllowReordering, Mode=TwoWay}" HorizontalOptions="Start" VerticalOptions="Center" />
                <Label Text="Allow reordering" HorizontalOptions="Start" VerticalOptions="Center" />
            </StackLayout>
            <ListView ItemsSource="{Binding Players}" HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand" effects:Sorting.IsSortable="{Binding AllowReordering}">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <StackLayout Orientation="Vertical" HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">
                                <Label Text="{Binding Name}" Margin="5" HorizontalOptions="FillAndExpand" VerticalOptions="CenterAndExpand" />
                            </StackLayout>
                        </ViewCell>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
            <Button Text="Load Players" Command="{Binding LoadPlayersCommand}" HorizontalOptions="FillAndExpand" VerticalOptions="End" />
        </StackLayout>

    </ContentPage>

Here the effect is attached to the ListView.

#### BaseModel.cs

    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }

MainVM.cs

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

A complete example is available in this repository.
