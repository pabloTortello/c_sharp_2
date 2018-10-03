using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StarWars
{
    class Medicine : GameObject
    {
        private readonly Image _img = Image.FromFile("src/heart.png");

        public Medicine(Point position, Point speed, Size size) : base(position, speed, size) { }

        public void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_img, _Position.X, _Position.Y, _Size.Width, _Size.Height);
        }

        public void Update()
        {
            if (_Position.X + _Size.Width > 0) _Position.X += _Speed.X;
            
        }

        public void Spawn()
        {
            _Position.X = Game.Width;
            _Position.Y = new Random().Next(0, Game.Height);
        }

    }
}
