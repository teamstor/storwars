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
        public Texture2D texture;
        public string Text;
        public Vector2 Position;
        public Vector2 Size;
        Rectangle rectangle;

        Color colour = new Color(255, 255, 255, 255);

        public GuiButton(Texture2D newtexture, GraphicsDevice graphics)
        {
            texture = newtexture;

            Size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);
        }


        bool down;
        public bool isClicked;
        public void update(MouseState mouse)
        {
           
        }

        public void setposition(Vector2 newposition)
        {
            Position = newposition;
        }

<<<<<<< HEAD
=======
            if (MouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if(down) colour.A += 3; else colour.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
>>>>>>> 6a8c337794ec51c7baa3878051b031a211e50ec5

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle();
            }
        }

        public void Draw(Engine.Game game, SpriteBatch batch)
        {
            Font font = game.Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf");
            Vector2 measure = font.Measure(6, Text);
            batch.Texture(Position, game.Assets.Get<Texture2D>("textures/Menu_Icons.png"),Color.White);
            batch.Text(font, 6, Text, new Vector2(Position.X + 80 - measure.X / 2, Position.Y + 4), Color.White);
        }
        
        public void Update(Engine.Game game, double deltaTime, double totalTime, long count)
        {
            rectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

            Rectangle MouseRectangle = new Rectangle((int)game.Input.MousePosition.X, (int)game.Input.MousePosition.Y, (int)Size.X, (int)Size.Y);

            if (MouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3; else colour.A -= 3;
                if (game.Input.MousePressed(Engine.MouseButton.Left)) isClicked = true;

            }

            else if (colour.A < 255)
            {
                colour.A += 3;
                isClicked = false;
            }
        }
    }
}
