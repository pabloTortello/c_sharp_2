using System;
using System.Drawing;
using System.Windows.Forms;

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

        /// <summary>Массив графических игровых объекотв</summary>
        private static GameObject[] __GameObjects;

        private static Asteroid[] __Asteroids;

        private static Bullet __Bullet;

        private static Ship __Ship;

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

            const int asteroids_count = 10;
            __Asteroids = new Asteroid[asteroids_count];

            for (var i = 0; i < asteroids_count; i++)
            {
                var speed = __Rnd.Next(3, 7);
                var size = __Rnd.Next(30, 50);
                __Asteroids[i] = new Asteroid(
                    new Point(__Rnd.Next(Width + 600), __Rnd.Next(0, Height)),
                    new Point(-speed, speed),
                    new Size(size, size));
            }

            __Ship = new Ship(
                new Point(10, 300),
                new Point(5, 5),
                new Size(38, 34));
            __Ship.ShipDie += OnShipDie;

            //__Bullet = new Bullet(new Point(0, 200), new Size(4, 1));
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
        }

        private static void OnGameFormKeyPress(object sender, KeyEventArgs args)
        {
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

        /// <summary>Метод, вызываемвый таймером всякий раз при истечении указанного интервала времени</summary>
        private static void OnTimerTick(object Sender, EventArgs e)
        {
            Update();
            Draw();
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

            foreach (var asteroid in __Asteroids)
                asteroid.Draw();

            __Ship?.Draw();

            __Bullet?.Draw();

            Buffer.Render(); // Переносим содержимое буфера на экран
        }

        /// <summary>Метод обновления состояния игры между кадрами</summary>
        private static void Update()
        {
            // Пробегаемся по всем игровым объектам
            foreach (var game_object in __GameObjects)
                game_object.Update(); // И вызываем у каждого метод обновления состояния

            foreach (var asteroid in __Asteroids)
            {
                asteroid.Update();
                if (__Bullet != null)
                    if (asteroid.Collision(__Bullet))
                    {
                        asteroid.Spawn();
                        __Bullet = null;
                    }
                if (__Ship != null)
                    if (__Ship.Collision(asteroid))
                    {
                        asteroid.Spawn();
                        __Ship.Damage();
                        if (__Ship.Eneregy < 1)
                        {
                            __Ship.Die();
                            //break;
                        }
                    }
            }

            __Bullet?.Update();
        }
    }
}
