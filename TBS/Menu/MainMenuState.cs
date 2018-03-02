using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.TBS.Gui;

using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
using Microsoft.Xna.Framework.Media;
using TeamStor.TBS.Map.Editor;
using TeamStor.TBS.Online.States;

namespace TeamStor.TBS.Menu
{
    class MainMenuState : GameState
    {
        private string[] _splashLines;
        private int _selectedSplashText;
        
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
            batch.Rectangle(new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), Color.Black * 0.2f);
            
            //drawing the button
            buttonplay.Draw(Game, batch);
            buttonmap.Draw(Game, batch);
            buttonoption.Draw(Game, batch);
            buttonquit.Draw(Game, batch);
            //batch ritar ut saker

            batch.Texture(new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 180 -6, 6), Assets.Get<Texture2D>("textures/logo.png"), Color.White);
            //Tar fram logo:n och mappar ut den på menyn
            batch.Transform = 
                Matrix.CreateScale(2 + (float) (Math.Sin(Game.Time * 6f) + 1) * 0.04f) *
                Matrix.CreateRotationZ(MathHelper.Pi * 0.01f);
            
            Font font = Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf");
            batch.Text(font, 8, _splashLines[_selectedSplashText], new Vector2(20, 20), Color.White);
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

            MediaPlayer.Play(Assets.Get<Song>("music/menu.ogg")); //music to the main menu
            MediaPlayer.Volume = 0.1f; //volume
            MediaPlayer.IsRepeating = true; //makes the song repeat

            _splashLines = File.ReadAllLines("data/splash_text.txt");
            _selectedSplashText = new Random().Next() % _splashLines.Length;

            // Dela upp linjerna med \n (ny linje) i splashLines så att dem inte blir för långa
            // - Hannes
            for(int i = 0; i < _splashLines.Length; i++)
            {
                string result = "";
                int i2 = 0;

                foreach(char c in _splashLines[i])
                {
                    result += c;
                    
                    // om den här linjen har gått över 25 bokstäver OCH vi är på ett mellanslag så delar vi upp till en ny linje
                    // - Hannes
                    if(i2++ >= 25 && c == ' ')
                    {
                        result += "\n";
                        i2 = 0;
                    }
                }

                _splashLines[i] = result;
            }
        }

        public override void OnLeave(GameState nextState)
        {
           MediaPlayer.Stop();
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

            if(buttonplay.HitBox.Contains(Input.MousePosition / 2) && Input.MousePressed(MouseButton.Left))
            {
                Assets.Get<SoundEffect>("soundfx/menu_click.wav", true).Play(0.1f, 0.0f, 1.0f);
                Game.CurrentState = new TestCreateOrJoinServerState();
            }
            if(buttonmap.HitBox.Contains(Input.MousePosition / 2) && Input.MousePressed(MouseButton.Left))
            {
                Assets.Get<SoundEffect>("soundfx/menu_click.wav", true).Play(0.1f, 0.0f, 1.0f);
                Game.CurrentState = new MapEditorState();
            }
            //mapeditor knappen funkar
            if(buttonquit.HitBox.Contains(Input.MousePosition / 2) && Input.MousePressed(MouseButton.Left))
            {
                Assets.Get<SoundEffect>("soundfx/menu_click.wav", true).Play(0.1f, 0.0f, 1.0f);
                Game.Exit();
            }
            //exit knappen funkar
            
            buttonoption.Deactivated = true;
            //makes the usuable buttons greyed out
        }
    }
}
