using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace StarWars
{
    /// <summary>Класс игровой логики</summary>
    internal static class Game
    {
        private static Random __Rnd = new Random();

        /// <summary>Конекст буфера отрисовки графики</summary>
        private static BufferedGraphicsContext __Context;

        /// <summary>Таймер обновления игрового интерфейса</summary>
        private static readonly Timer __Timer = new Timer { Interval = 50 };

        private static readonly Timer __Timer_for_medicine = new Timer { Interval = 20000 };

        /// <summary>Массив графических игровых объекотв</summary>
        private static GameObject[] __GameObjects;

        //private static Asteroid[] __Asteroids;

        private static Bullet __Bullet;

        private static Ship __Ship;

        private static Medicine __Medicine; //аптечка

        private static HP[] __hp;

        private static int __Score = 0;

        public static string user_name;

        private static int Asteroids_count { set; get; } = 10;

        private static List<Asteroid> __Asteroids;
        
        /// <summary>Буфер, в который будем проводить отрисовку графики очередного кадра</summary>
        public static BufferedGraphics Buffer { get; private set; }

        /// <summary>Ширина игрового поля</summary>
        public static int Width { get; private set; }
        /// <summary>Высота игрового поля</summary>
        public static int Height { get; private set; }

        /// <summary>Загрузка данных игровой логики</summary>
        public static void Load(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __GameObjects = new GameObject[50];

            for (var i = 0; i < __GameObjects.Length; i++)
                __GameObjects[i] = new Star(
                    new Point(__Rnd.Next(0, Height), i * 20),
                    new Point(__Rnd.Next(5, 7), 0),
                    new Size(5, 5));

            New_asteroids();

            __Ship = new Ship(
                new Point(10, 300),
                new Point(5, 5),
                new Size(38, 34));
            __Ship.ShipDie += OnShipDie;

            __hp = new HP[__Ship.HP];
            for (int i = 0; i < __hp.Length; i++)
            {
                __hp[i] = new HP(
                    new Point(10 + i * 40, 10),
                    new Point(0, 0),
                    new Size(30, 30));
            }
        }

        private static void New_asteroids()
        {
            __Asteroids = new List<Asteroid>(Asteroids_count);

            for (var i = 0; i < Asteroids_count; i++)
            {
                var speed = __Rnd.Next(3, 7);
                var size = __Rnd.Next(30, 50);
                __Asteroids.Add(new Asteroid(
                    new Point(__Rnd.Next(Width, Width + 600), __Rnd.Next(0, Height)),
                    new Point(-speed, speed),
                    new Size(size, size)));
            }
        }

        public static void New_Game(object sender, EventArgs e)
        {
            

        }

        private static void OnShipDie()
        {
            __Timer.Enabled = false;
            var g = Buffer.Graphics;
            g.Clear(Color.Black);
            g.DrawString("GAME OVER", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold), Brushes.White, 200, 100);
            Buffer.Render();
            __Ship = null;
        }

        /// <summary>Инициализация игровой логики</summary>
        /// <param name="form">Игровая форма</param>
        public static void Init(Form form)
        {
            Width = form.Width;
            Height = form.Height;
            form.KeyDown += OnGameFormKeyPress;

            __Context = BufferedGraphicsManager.Current;

            var graphics = form.CreateGraphics();
            Buffer = __Context.Allocate(graphics, new Rectangle(0, 0, Width, Height));

            __Timer.Tick += OnTimerTick;
            __Timer.Enabled = true;

            __Timer_for_medicine.Tick += OnTimerTick_for_medicine;
            __Timer_for_medicine.Enabled = true;

            //__hp = new HP(__Ship.HP);
        }

        private static void OnGameFormKeyPress(object sender, KeyEventArgs args)
        {
            if (__Timer.Enabled)
                switch (args.KeyCode)
                {
                    case Keys.ControlKey:
                        var ship_location = __Ship.Rect.Location;
                        __Bullet = new Bullet(new Point(ship_location.X + 38, ship_location.Y + 17), new Size(4, 1));
                        break;
                    case Keys.Up:
                        __Ship.Up();
                        break;
                    case Keys.Down:
                        __Ship.Down();
                        break;
                    case Keys.W:
                        __Ship.Die();
                        break;
                }   
        }

        private static void OnTimerTick_for_medicine(object Sender, EventArgs e)
        {
            //int z = __Rnd.Next(1, 3);
            //if (z == 2)
                __Medicine = new Medicine(
                    new Point(Width, __Rnd.Next(0, Height)),
                    new Point(-5, 5),
                    new Size(30, 30));
        }

        /// <summary>Метод, вызываемвый таймером всякий раз при истечении указанного интервала времени</summary>
        private static void OnTimerTick(object Sender, EventArgs e)
        {
            Update();
            if (__Timer.Enabled) Draw();
        }

        /// <summary>Метод отрисовки очередного кадра игры</summary>
        public static void Draw()
        {
            var g = Buffer.Graphics; // Извлекаем графический контекст для рисования
            g.Clear(Color.Black);    // Заливаем всю поверхность одним цветом (чёрным)

            g.FillEllipse(Brushes.Orange, 600, 100, 100, 100);   // Солнце

            // Пробегаемся по всем графическим объектам и вызываем у каждого метод отрисовки
            foreach (var game_object in __GameObjects)
                game_object.Draw();

            if (__Asteroids != null)
                foreach (var asteroid in __Asteroids)
                    asteroid.Draw();

            if (__Ship != null)
                for (int i = 0; i < __Ship.HP; i++)
                    __hp[i].Draw();

            __Ship?.Draw();

            __Bullet?.Draw();

            __Medicine?.Draw();

            Buffer.Graphics.DrawString(__Score.ToString(), new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold), Brushes.White, 700, 10);

            Buffer.Render(); // Переносим содержимое буфера на экран
        }

        /// <summary>Метод обновления состояния игры между кадрами</summary>
        private static void Update()
        {
            // Пробегаемся по всем игровым объектам
            foreach (var game_object in __GameObjects)
                game_object.Update(); // И вызываем у каждого метод обновления состояния

            if (__Asteroids.Count > 0)
                for (int i = 0; i < __Asteroids.Count; i++)
                {
                    __Asteroids[i].Update();
                    if (__Bullet != null && __Asteroids[i].Collision(__Bullet))
                    {
                        __Score++;
                        __Asteroids.Remove(__Asteroids[i]);
                        for (int z = i; z < __Asteroids.Count; z++) __Asteroids[z].Update();
                        __Bullet = null;
                        break;
                    }

                    if (__Ship != null && __Ship.Collision(__Asteroids[i]))
                    {
                        __Asteroids[i].Spawn();
                        __Ship.HP_down();
                        if (__Ship.HP < 1)
                        {
                            __Ship.Die();
                            //break;
                        }
                    }
                }
            else
            {
                Asteroids_count++;
                New_asteroids();
            }

            //foreach (var asteroid in __Asteroids)
            //{
            //    asteroid.Update();
            //    if (__Bullet != null && asteroid.Collision(__Bullet))
            //    {
            //        __Score++;
            //        //asteroid.Spawn();
            //        __Asteroids.Remove(asteroid);
            //        __Bullet = null;
            //        break;
            //    }
            //    if (__Ship != null && __Ship.Collision(asteroid))
            //    {
            //        asteroid.Spawn();
            //        __Ship.HP_down();
            //        if (__Ship.HP < 1)
            //        {
            //            __Ship.Die();
            //            //break;
            //        }
            //    }                 
            //}

            if (__Ship != null && __Medicine != null && __Ship.Collision(__Medicine))
            {
                __Ship.HP_up();
                __Medicine = null;
            }


            __Medicine?.Update();

            __Bullet?.Update();

        }

        
    }
}
