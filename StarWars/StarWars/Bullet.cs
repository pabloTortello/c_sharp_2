using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StarWars
{
    class Bullet : GameObject
    {
        private Random __Rnd = new Random();

        public Bullet(Point Position, Size Size) : base(Position, new Point(), Size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Orange, new Rectangle(_Position, _Size));
        }

        public void Spawn()
        {
            _Position.X = 0;
            _Position.Y = __Rnd.Next(0, Game.Height);
        }

        public override void Update()
        {
            if (_Position.X < Game.Width)
                _Position.X += 30;
        }
    }
}
