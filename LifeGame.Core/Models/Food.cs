using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Windows.UI.Xaml.Media;

namespace LifeGame.Core.Models
{
    public class Food : PointModel
    {
        public Food(int maxX, int maxY, Size size)
            : base(maxX, maxY, size) { }
    }
}
