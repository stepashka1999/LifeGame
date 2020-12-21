using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace LifeGame.Core.Models
{
    public class LifeModel : PointModel
    {
        public int Speed { get; private set; }
        public double Saturation { get; private set; }
        private readonly int maxSize = 25;

        public LifeModel(LifeConfiguration configuration) 
            : base(configuration.Position, configuration.Size)
        {
            Speed = configuration.Speed;
            Saturation = ((double)configuration.SaturationValue)/10;
        }
        public LifeModel(int maxX, int maxY, Size size) 
            : base(maxX, maxY, size) { }

        private PointModel targetPoint = null;

        //ToDo
        private Point MoveRandom()
        {
            var rnd = new Random();
            var tempDx = rnd.Next(minMoveRange, maxMoveRange);
            var tempDy = rnd.Next(minMoveRange, maxMoveRange);

            var dx = tempDx+Position.X < 20 ? 0 : tempDx;
            var dy = tempDy+Position.Y < 20 ? 0 : tempDy;

            var pos = Position;
            pos.Offset(dx, dy);
            Position = pos;

            return Position;
        }

        private Point MoveToTargetPoint()
        {
            if (targetPoint == null) 
                return Position;

            var dx = position.X > targetPoint.position.X ? -Speed : Speed;
            var dy = position.Y > targetPoint.position.Y ? -Speed : Speed;

            var pos = Position;
            pos.Offset(dx, dy);
            Position = pos;

            return Position;
        }

        //ToDo
        private int GetMoveMode()
        {
            var rnd = new Random();
            return rnd.Next(0, 4);
        }

        private void FindTarget(IEnumerable<PointModel> lifePoints, int moveMode)
        {
            switch(moveMode)
            {
                case 0:
                    FindCloserPoint(lifePoints.Where(x => x is Food));
                    break;
                case 1:
                    FindCloserPoint(lifePoints.Where(x => x is LifeModel));
                    break;
            }
        }

        public Point Move(IEnumerable<PointModel> lifePoints)
        {
            if (targetPoint?.IsAlive == false)
                targetPoint = null;

            FindTarget(lifePoints, 0);
            MoveToTargetPoint();

            return Position;
        }
        public async Task<Point> MoveAsync()
        {
            //return await Task.Factory.StartNew(Move);
            throw new NotImplementedException();
        }

        public int CollisionWith(PointModel collisionModel)//1 - alive | 0 - nothing | -1 - dead
        {
            if (collisionModel is Food) return Eat(collisionModel as Food);
            else if (collisionModel is LifeModel) return 0;//TODO

            return 1;
        }

        private int Eat(Food food)
        {
            if (targetPoint == food) targetPoint = null;

            food.IsAlive = false;
            var tempSize = Size.Width + food.Size.Width;
            var newSize = tempSize > maxSize ? maxSize : tempSize;
            Size = new Size(newSize, newSize);

            return 1;
        }

        private void FindCloserPoint(IEnumerable<PointModel> points)
        {
            PointModel closerPoint = null;
            int lastDistance = int.MaxValue;

            foreach(var item in points)
            {
                if (item == this) continue;

                var distance = GetDistance(item);

                if (lastDistance > distance)
                {
                    lastDistance = distance;
                    closerPoint = item;
                }
            }

            targetPoint = closerPoint;
        }

        public void MakeLifeStep()
        {
            if(Size.Width > 1)
                Size = new Size(Size.Width - 1, Size.Height - 1);
        }
    }
}
