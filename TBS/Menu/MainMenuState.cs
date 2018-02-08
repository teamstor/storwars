using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.TBS.Gui;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
namespace TeamStor.TBS.Menu
{
    class MainMenuState : GameState
    {


        GuiButton buttonplay;

        public override void Draw(Engine.Graphics.SpriteBatch batch, Vector2 screenSize)
        {
            buttonplay.Draw(Game, batch);
        }

        public override void FixedUpdate(long count)
        {
            throw new NotImplementedException();
        }

        public override void OnEnter(GameState previousState)
        {
            buttonplay = new GuiButton(Assets.Get<Texture2D>("textures/Menu_Icons.png"), Game.GraphicsDevice);
        }

        public override void OnLeave(GameState nextState)
        {
            throw new NotImplementedException();
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {

        }
    }
}
