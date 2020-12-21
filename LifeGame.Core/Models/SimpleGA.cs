using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LifeGame.Core.Models
{
    public class SimpleGA
    {
        public double Saturation { get; set; }
        public Size Size { get; set; }
        public int Speed { get; set; }

        public SimpleGA(LifeModel obj1, LifeModel obj2)
        {
            Speed = obj1.Speed < obj2.Speed ? obj2.Speed : obj1.Speed;
            Size = obj1.Size.Width < obj2.Size.Width ? obj2.Size : obj1.Size;
            Saturation = obj1.Saturation < obj2.Saturation ? obj2.Saturation : obj1.Saturation;
        }


        public IEnumerable<LifeModel> CreateNewGeneration(int generationCount)
        {
            var rnd = new Random();
            var newGeneration = new List<LifeModel>();
            for (int i = 0; i < generationCount; i++)
            {
                var tempValue = ((double)rnd.Next(10, 14))/10;
                var isPlus = rnd.Next(2);
                var speed = isPlus > 0 ? Speed * (2 - tempValue) : Speed * tempValue;

                tempValue = ((double)rnd.Next(10, 14)) / 10;
                isPlus = rnd.Next(2);
                var width = isPlus > 0 ? Size.Width * (2 - tempValue) : Size.Width * tempValue;
                var size = new Size((int)width, (int)width);

                tempValue = ((double)rnd.Next(10, 14)) / 10;
                isPlus = rnd.Next(2);
                var saturation = isPlus > 0 ? Saturation * (2 - tempValue) : Saturation * tempValue;

                var config = new LifeConfiguration()
                {
                    HungerValue = 0,
                    Size = size,
                    Speed = (int)speed,
                    SaturationValue = (int)saturation
                };
                var nextgenObj = new LifeModel(config);

                newGeneration.Add(nextgenObj);
            }

            return newGeneration;
        }
    }
}
