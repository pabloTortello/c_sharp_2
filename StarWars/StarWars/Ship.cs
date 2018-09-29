using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StarWars
{
    class Ship : GameObject
    {
        public event Action ShipDie;

        public int HP { set; get; } = 3;

        private readonly Image _img = Image.FromFile("src/ship.png");

        public Ship(Point position, Point speed, Size size) : base(position, speed, size) { }

        public void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_img, _Position.X, _Position.Y, _Size.Width, _Size.Height);
        }

        public void Up()
        {
            if (_Position.Y > 0)
                _Position.Y -= _Speed.Y;
        }

        public void Down()
        {
            if (_Position.Y < Game.Height - _Size.Height)
                _Position.Y += _Speed.Y;
        }

        public void HP_down()
        {
            HP--;
        }

        public void HP_up()
        {
            if (HP < 3)
                HP++;
        }

        public void Die()
        {
            ShipDie?.Invoke();
        }
    }
}
