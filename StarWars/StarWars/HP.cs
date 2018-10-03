using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace StarWars
{
    class HP : GameObject
    {
        private readonly Image _img = Image.FromFile("src/heart.png");

        public HP(Point position, Point speed, Size size) : base(position, speed, size) { }
        
        public void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_img, _Position.X, _Position.Y, _Size.Width, _Size.Height);
        }

        public void Update(int count)
        {
            for (int i = 0; i < count * 10; i += 10)
            {
                Game.Buffer.Graphics.DrawImage(_img, i + 10, 10, 30, 30);
                Game.Buffer.Render();
            }
        }
    }
}
