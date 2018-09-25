using System;
using System.Drawing;

namespace StarWars
{
    /// <summary>Игровой объект (инкапсулирующий логику риосвания на игровой сцене, перемещение и взаимодействие с другими объектами)</summary>
    internal abstract class GameObject : ICollision
    {
        /// <summary>Положение на экране</summary>
        protected Point _Position;

        /// <summary>Скорость движения между кадрами</summary>
        protected Point _Speed;

        /// <summary>Размер на игровой сцене</summary>
        protected Size _Size;

        public Rectangle Rect => new Rectangle(_Position, _Size);

        /// <summary>Инициализация нового игрового объекта</summary>
        /// <param name="Position">ПОложение на игровой сцене</param>
        /// <param name="Speed">Скорость перемещения между кадрами</param>
        /// <param name="Size">Размер на игровой сцене</param>
        public GameObject(Point Position, Point Speed, Size Size)
        {
            _Position = Position;
            _Speed = Speed;
            _Size = Size;
        }

        public bool Collision(ICollision obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return Rect.IntersectsWith(obj.Rect);
        }

        /// <summary>Метод отрисовки графики объекта на игровой сцене</summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, new Rectangle(_Position, _Size));
        }

        /// <summary>Метод обновления состояния объекта при смене кадров</summary>
        public virtual void Update()
        {
            _Position.X += _Speed.X;  // Перемещаем объект на сцене в соответствии с вектором скорости

            // Проверяем граничные условия выхдода объекта за пределы сцены (меняем знак соответствующей составляющей вектора скорости)
            if (_Position.X < 0)
            {
                _Position.X = Game.Width;
            }
        }
    }
}
