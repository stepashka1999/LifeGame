using LifeGame.Core.Models;
using LifeGame.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LifeGame.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private GameConfiguration config;

        public SettingsViewModel()
        {
            config = ConfigLoader.Load();
            foodCount = config.FoodCount;
            lifeCount = config.LifeCount;
            foodSize = config.FoodSize.Width;
        }

        public int foodCount;
        public int FoodCount
        {
            get => foodCount;
            set
            {
                foodCount = value;
                OnPropertyChanged(nameof(FoodCount));
            }
        }

        public int lifeCount;
        public int LifeCount
        {
            get => lifeCount;
            set
            {
                lifeCount = value;
                OnPropertyChanged(nameof(LifeCount));
            }
        }

        public int foodSize;
        public int FoodSize
        {
            get => foodSize;
            set
            {
                foodSize = value;
                OnPropertyChanged(nameof(FoodSize));
            }
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    config.FoodCount = foodCount;
                    config.LifeCount = lifeCount;
                    config.FoodSize = new Size(foodSize, foodSize);
                    ConfigLoader.Save(config);
                });
            }
        }
    }
}
