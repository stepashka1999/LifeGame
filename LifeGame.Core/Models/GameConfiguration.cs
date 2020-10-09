using System;
using System.Drawing;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace LifeGame.Core.Models
{
    public class GameConfiguration
    {
        public int FoodCount { get; set; }
        public int LifeCount { get; set; }
        public Size FoodSize { get; set; }
        public double FoodSpawnTic { get; set; }
        public double LifeStepTic { get; set; }
        public double MoveTic { get; set; }

        public GameConfiguration()
        {
            if(FoodCount == default(int))
            {
                FoodCount = 10;
                LifeCount = 10;
                FoodSize = new Size(10, 10);
                FoodSpawnTic = 1.5;
                LifeStepTic = 0.5;
                MoveTic = 0.01;
            }
        }
    }
}