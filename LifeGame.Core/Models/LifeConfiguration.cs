using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Windows.Media.Devices.Core;

namespace LifeGame.Core.Models
{
    public class LifeConfiguration
    {
        public Size Size { get; set; } = new Size(10, 10);
        public Point Position { get; set; }
        public int Speed { get; set; } = 1;
        public int HungerValue { get; set; } = 1;
        public int SaturationValue { get; set; } = 1;

        public static class Factory
        {
            public static LifeModel CreateRandomLife()
            {
                var rnd = new Random();
                var size = rnd.Next(5, 50);
                var posX = rnd.Next(0, 800);
                var posY = rnd.Next(0, 800);

                var lifeConfig = new LifeConfiguration()
                {
                    Size = new Size(size, size),
                    Position = new Point(posX, posY),
                    Speed = rnd.Next(2, 10),
                    HungerValue = rnd.Next(1, 30),
                    SaturationValue = rnd.Next(1, 30)
                };

                return new LifeModel(lifeConfig);
            }
        }

    }
}
