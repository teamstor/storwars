using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.Engine.Tween;
using Game = TeamStor.Engine.Game;

namespace TeamStor.TBS.Map.Editor
{
    public class Slider
    {
        public string Text;
        public float Value;
        public Font Font;

        public TweenedVector2 Position;
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.Value.X, (int)Position.Value.Y, 200 + 8, 32);
            }
        }

        public void Update(Game game)
        {

        }

        public void Draw(Game game)
        {
            game.Batch.Rectangle(Rectangle, Color.Black * 0.85f);
        }
    }
}
