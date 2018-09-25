using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars
{
    internal class Asteroid : GameObject, ICloneable, IIMyCloneable
    {
        private static Random __Rnd = new Random();

        private static readonly Image __Img = Image.FromFile(@"src\asteroid.png");

        public int Power { get; set; } = 1;

        public Asteroid(Point Position, Point Speed, Size Size) : base(Position, Speed, Size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(__Img, new Rectangle(_Position, _Size));
        }

        public object Clone()
        {
            var new_asteroid = (Asteroid)MemberwiseClone();
            new_asteroid.Power = Power;
            return new_asteroid;
        }

        object ICloneable.Clone()
        {
            return new Asteroid(_Position, _Speed, _Size) { Power = Power };
        }

        Asteroid IIMyCloneable.Clone()
        {
            return (Asteroid)this.Clone();
        }

        public void Update()
        {
            _Position.X += _Speed.X;  // Перемещаем объект на сцене в соответствии с вектором скорости

            
            if (_Position.X < -_Size.Width)
            {
                _Position.X = Game.Width;
                _Position.Y = __Rnd.Next(0, Game.Height);
            }
        }
    }

    interface IIMyCloneable : ICloneable
    {
        new Asteroid Clone();
    }
}
