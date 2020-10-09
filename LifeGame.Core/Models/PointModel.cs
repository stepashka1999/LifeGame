using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Windows.UI.Xaml.Media;

namespace LifeGame.Core.Models
{
    public abstract class PointModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private protected int size = 10;
        private protected int minMoveRange = -1;
        private protected int maxMoveRange = 2;
        public bool IsAlive = true;

        public Brush Color { get; set; }
       
        private Size _size;
        public Size Size { get => _size; private protected set { _size = value; OnPropertyChanged(nameof(Size)); } }
        
        public Point position;
        public Point Position
        {
            get => position; private protected set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public PointModel(int maxX, int maxY, Size size)
        {
            Initialize(maxX, maxY, size);
        }

        public PointModel(Point position, Size size)
        {
            Position = position;
            Size = size;
        }

        private void Initialize(int x, int y, Size size)
        {
            Size = size;
            InitPosition(x, y);
        }
        private void InitPosition(int maxX, int maxY)
        {
            var rnd = new Random();
            var x = rnd.Next(maxX);
            var y = rnd.Next(maxY);
            Position = new Point(x, y);
        }



        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static class Factory
        {
            public static IEnumerable<LifeModel> CreateRangomLifeDots(int count)
            {
                var resList = new List<LifeModel>();
                for (int i = 0; i < count; i++)
                {
                    var item = LifeConfiguration.Factory.CreateRandomLife();
                    resList.Add(item);
                }
                return resList;
            }

            public static IEnumerable<LifeModel> CreateLifeDots(int maxX, int maxY, int count, Size size)
            {
                var resList = new List<LifeModel>();
                for (int i = 0; i < count; i++)
                {
                    var item = new LifeModel(maxX, maxY, size);
                    resList.Add(item);
                }
                return resList;
            }

            public static IEnumerable<Food> CreateFoodDots(int maxX, int maxY, int count, Size size)
            {
                var resList = new List<Food>();
                for (int i = 0; i < count; i++)
                {
                    var item = new Food(maxX, maxY, size);
                    resList.Add(item);
                }
                return resList;
            }
        }

        public bool IntersectsWith(PointModel point)
        {
            var rect1 = new Rectangle(Position, Size);
            var rect2 = new Rectangle(point.Position, point.Size);

            return rect1.IntersectsWith(rect2);
        }

        public int GetDistance(PointModel point)
        {
            var dx = point.Position.X - Position.X;
            var dy = point.Position.Y - Position.Y;

            var dx2 = Math.Pow(dx, 2);
            var dy2 = Math.Pow(dy, 2);

            return (int)Math.Sqrt(dx2 + dy2);
        }
    }
}
