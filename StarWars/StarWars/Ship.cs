using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StarWars
{
    class Ship : GameObject
    {
        static Image _img = Image.FromFile("src/ship.png");

        public Ship(Point position, Point speed, Size size) : base(position, speed, size) { }

        public void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_img, _Position.X, _Position.Y, _Size.Width, _Size.Height);
        }

    }
}
