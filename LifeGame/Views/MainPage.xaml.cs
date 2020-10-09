using System;

using II_Game.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LifeGame.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
