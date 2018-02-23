using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TeamStor.Engine.Graphics;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
using Microsoft.Xna.Framework.Input;

namespace TeamStor.TBS.Gui
{
    public class GuiButton : GuiElement
    {
        //the variables.
        public Texture2D texture;
        public string Text;
        public Vector2 Position;
        public Vector2 Size = new Vector2(160, 12);
       

        //colour value.
        Color colour = new Color(255, 255, 255, 255);

        public GuiButton(Texture2D newtexture, GraphicsDevice graphics, Vector2 position, string text)
        {
            //texture methode.
            texture = newtexture;

            Position = position;
            Text = text;
        }


        bool down;
        public bool isClicked;
        public void update(MouseState mouse)
        {
           
        }

        public void setposition(Vector2 newposition)
        {
            //button position.
            Position = newposition;
        }

        public Rectangle HitBox
        {
            //button hitbox.
            get
            {
                return new Rectangle();
            }
        }

        public void Draw(Engine.Game game, SpriteBatch batch)
        {
            //the button itself.
            Font font = game.Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf");
            Vector2 measure = font.Measure(6, Text);
            batch.Texture(Position, game.Assets.Get<Texture2D>("textures/Menu_Icons.png"),Color.White, null, new Rectangle(96, 24, 160, 12));
            batch.Text(font, 6, Text, new Vector2(Position.X + 80 - measure.X / 2, Position.Y + 2), Color.White);
        }
        
        public void Update(Engine.Game game, double deltaTime, double totalTime, long count)
        {
            //the button position.
            Rectangle rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

            Rectangle TextureRectangle = new Rectangle(96, 0, 160, 12);
            if (Rectangle.Contains(game.Input.MousePosition/2))
            {
                TextureRectangle = new Rectangle(96, 0, 160, 12);
            }
            //look for if the button is pressed. 
            else if (colour.A < 255)
            {
                colour.A += 3;
                isClicked = false;
            }*/
        }
    }
}
