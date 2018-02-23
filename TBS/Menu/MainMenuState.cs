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

using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
using Microsoft.Xna.Framework.Media;

namespace TeamStor.TBS.Menu
{
    class MainMenuState : GameState
    {

        // button class

        GuiButton buttonplay;
        GuiButton buttonmap;
        GuiButton buttonoption;
        GuiButton buttonquit;

        public override void Draw(Engine.Graphics.SpriteBatch batch, Vector2 screenSize)
        {
            batch.Transform = Matrix.CreateScale(2);
            batch.SamplerState = SamplerState.PointWrap;
            //ändrade clamp till warp så att bg ritas ut flera gånger

            batch.Texture(new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), Assets.Get<Texture2D>("textures/bg.png"), Color.White, new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y));

            //drawing the button
            buttonplay.Draw(Game, batch);
            buttonmap.Draw(Game, batch);
            buttonoption.Draw(Game, batch);
            buttonquit.Draw(Game, batch);

            batch.Texture(new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 180 -6, 6), Assets.Get<Texture2D>("textures/logo.png"), Color.White);
        }

        public override void FixedUpdate(long count)
        {
           
        }

        public override void OnEnter(GameState previousState)
        {
            //putting the button texture on the screen
            buttonplay = new GuiButton(Assets.Get<Texture2D>("textures/Menu_Icons.png"), Game.GraphicsDevice, new Vector2 (Game.GraphicsDevice.Viewport.Width/2 - 160  - 6, Game.GraphicsDevice.Viewport.Height/2 - 12 - 6), "Play");
            buttonmap = new GuiButton(Assets.Get<Texture2D>("textures/Menu_Icons.png"), Game.GraphicsDevice, new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 160 - 6, Game.GraphicsDevice.Viewport.Height / 2 - 12 - 6), "Level Editor");
            buttonoption = new GuiButton(Assets.Get<Texture2D>("textures/Menu_Icons.png"), Game.GraphicsDevice, new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 160 - 6, Game.GraphicsDevice.Viewport.Height / 2 - 12 - 6), "Options");
            buttonquit = new GuiButton(Assets.Get<Texture2D>("textures/Menu_Icons.png"), Game.GraphicsDevice, new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 160 - 6, Game.GraphicsDevice.Viewport.Height / 2 - 12 - 6), "Quit");

            MediaPlayer.Play(Assets.Get<Song>("music/menu.ogg"));
        }

        public override void OnLeave(GameState nextState)
        {
           
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            buttonplay.Update(Game, deltaTime, totalTime, count);
            buttonplay.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 160 - 6, Game.GraphicsDevice.Viewport.Height / 2 - 6*11 - 6);
            buttonmap.Update(Game, deltaTime, totalTime, count);
            buttonmap.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 160 - 6, Game.GraphicsDevice.Viewport.Height / 2 - 6*8 - 6);
            buttonoption.Update(Game, deltaTime, totalTime, count);
            buttonoption.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 160 - 6, Game.GraphicsDevice.Viewport.Height / 2 - 6*5 - 6);
            buttonquit.Update(Game, deltaTime, totalTime, count);
            buttonquit.Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 160 - 6, Game.GraphicsDevice.Viewport.Height / 2 - 6*2 - 6);
            //knapparna stackas åtanpå varan tack vare multiplication, henke satte en knapp så lägger jag ba resten under den och sätter den på toppen
            //ändrade multiplicationen så att det blir space mellan knapparna
        }
    }
}
