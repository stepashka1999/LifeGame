using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Media;

namespace LifeGame.Core.Models
{
    class LifeGameModel
    {
        private int canvasSize = 800;
        private GameConfiguration gameConfig;
        public ObservableCollection<PointModel> LifeDots { get; private set; } = new ObservableCollection<PointModel>();

        #region Constructors
        public LifeGameModel()
        {
            gameConfig = ConfigLoader.Load();
        }

        #endregion

        public void Start(CoreDispatcher dispatcher)
        {
            InitializeGame();
            CreateTimers(dispatcher);
        }

        private void InitializeGame()
        {
            gameConfig = ConfigLoader.Load();
            FillWithLife();
            FillWithFood();
        }

        private void CreateTimers(CoreDispatcher dispatcher)
        {
            var _timer = ThreadPoolTimer.CreatePeriodicTimer(
                        async source =>
                        {
                            await dispatcher.RunAsync(
                                CoreDispatcherPriority.High,
                                async () =>
                                {
                                    MoveDots();
                                    OnCollisionEnter();
                                    //await OnCollisionEnterAsync();
                                });
                        }, TimeSpan.FromSeconds(gameConfig.MoveTic));

            var _foodTimer = ThreadPoolTimer.CreatePeriodicTimer(
                        async source =>
                        {
                            await dispatcher.RunAsync(
                                CoreDispatcherPriority.High,
                                async () =>
                                {
                                    FillWithFood();
                                    //await OnCollisionEnterAsync();
                                });
                        }, TimeSpan.FromSeconds(gameConfig.FoodSpawnTic));

            var _lifeTimer = ThreadPoolTimer.CreatePeriodicTimer(
                       async source =>
                       {
                           await dispatcher.RunAsync(
                               CoreDispatcherPriority.High,
                               async () =>
                               {
                                   MakeLifeStep();
                                   //await OnCollisionEnterAsync();
                               });
                       }, TimeSpan.FromSeconds(gameConfig.LifeStepTic));
        }

        private void MakeLifeStep()
        {
            for (int i = 0; i < LifeDots.Count; i++)
            {
                if (LifeDots[i].Size.Width == 1)
                    LifeDots.Remove(LifeDots[i]);
                else if (LifeDots[i] is LifeModel)
                    (LifeDots[i] as LifeModel).MakeLifeStep();
            }
        }

        private void OnCollisionEnter()
        {
            var checkedPoints = new List<int>();

            for (int i = 0; i < LifeDots.Count; i++)
            {
                if (checkedPoints.Contains(i)) continue;

                var currentItem = LifeDots[i];
                if (currentItem is Food) continue;

                for (int j = i + 1; j < LifeDots.Count; j++)
                {
                    var compareItem = LifeDots[j];

                    var result = currentItem.IntersectsWith(compareItem);

                    if (result)
                    {
                        var res = (currentItem as LifeModel).CollisionWith(compareItem);
                        if (res == 1) LifeDots.Remove(compareItem);
                        //else TODO
                        checkedPoints.Add(j);
                    }
                }

                checkedPoints.Add(i);
            }
        }

        private void MoveDots()
        {
            var lifeList = LifeDots.Where(x => x is LifeModel).ToList();
            if(lifeList.Count == 3)
            {
                var generator = new SimpleGA((LifeModel)lifeList[0], (LifeModel)lifeList[1]);
                var lifes = generator.CreateNewGeneration(10);
                foreach(var life in lifes)
                {
                    life.Color = new SolidColorBrush(Colors.Red);
                    LifeDots.Add(life);
                }
                LifeDots.Remove(lifeList[1]);
                LifeDots.Remove(lifeList[0]);
            }

            foreach (var item in LifeDots)
            {
                if (item is LifeModel)
                    (item as LifeModel).Move(LifeDots);
            }
        }

        #region Fill LifeDots

        private void FillWithLife()
        {
            //var tempList = PointModel.Factory.CreateLifeDots(canvasSize,
            //                                                 canvasSize,
            //                                                 gameConfig.LifeCount,
            //                                                 gameConfig.LifeSize);

            var tempList = PointModel.Factory.CreateRangomLifeDots(gameConfig.LifeCount);

            foreach(var item in tempList)
            {
                item.Color = new SolidColorBrush(Colors.Red);
                LifeDots.Add(item);
            }
        }

        private void FillWithFood()
        {
            var tempList = PointModel.Factory.CreateFoodDots(canvasSize,
                                                             canvasSize,
                                                             gameConfig.FoodCount,
                                                             gameConfig.FoodSize);
            foreach (var item in tempList)
            {
                item.Color = new SolidColorBrush(Colors.LightYellow);
                LifeDots.Add(item);
            }
        }

        #endregion
    }
}
