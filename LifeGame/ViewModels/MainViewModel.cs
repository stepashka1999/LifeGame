using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using System.Windows.Input;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.ComponentModel;
using System.Drawing;
using Windows.UI.Xaml.Shapes;
using LifeGame.Core.Models;
using LifeGame.Helpers;
using System.Collections.ObjectModel;

namespace II_Game.ViewModels
{
    public class MainViewModel : Observable
    {
        private LifeGameModel gameModel = new LifeGameModel();
        public ObservableCollection<PointModel> LifeDots => gameModel.LifeDots;
        public MainViewModel()
        {
           
        }

        public ICommand StartCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    gameModel.Start(obj as CoreDispatcher);
                });
            }
        }
    }
}
