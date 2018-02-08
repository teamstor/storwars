using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamStor.Engine.Graphics;
using Game = TeamStor.Engine.Game;

namespace TeamStor.TBS.Gui
{
    public interface GuiElement
    {
        void Update(Game game, double deltaTime, double totalTime, long count);

        void Draw(Game game, SpriteBatch batch);

        Rectangle HitBox
        {
            get;
        }

    }
}
