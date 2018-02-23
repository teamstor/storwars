using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.TBS.Gui;
using TeamStor.TBS.Map;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Gameplay.Ingame
{
    /// <summary>
    /// Fog of war.
    /// </summary>
    public class FogOfWar
    {
        private BlendState _blend = new BlendState();
        
        /// <summary>
        /// The fog of war texture.
        /// </summary>
        public RenderTarget2D Texture
        {
            get;
            private set;
        }

        /// <summary>
        /// Areas (in tiles) that are fully lit up.
        /// </summary>
        public List<Rectangle> RevealedAreas
        {
            get;
            private set;
        } = new List<Rectangle>();

        public FogOfWar(Game game, MapData map)
        {
            Texture = new RenderTarget2D(game.GraphicsDevice, map.Width + 200, map.Height + 100);
            
            // https://gamedev.stackexchange.com/questions/45667/multiply-mode-in-spritebatch
            _blend.ColorBlendFunction = BlendFunction.Add;
            _blend.ColorSourceBlend = Blend.DestinationColor;
            _blend.ColorDestinationBlend = Blend.Zero;
        }

        /// <summary>
        /// If the point (in tiles) is revealed.
        /// </summary>
        public bool IsPointRevealed(Point point)
        {
            return RevealedAreas.Any(a => new Rectangle(a.X + 100, a.Y + 50, a.Width, a.Height).Contains(point));
        }
        
        /// <summary>
        /// If the rectangle (in tiles) is revealed.
        /// </summary>
        public bool IsRectangleRevealed(Rectangle rectangle)
        {
            return RevealedAreas.Any(a => new Rectangle(a.X + 100, a.Y + 50, a.Width, a.Height).Contains(rectangle));
        }
        
        public void UpdateRenderTarget(SpriteBatch batch)
        {
            SamplerState oldSamplerState = batch.SamplerState;
            Matrix oldTransform = batch.Transform;
            
            batch.SamplerState = SamplerState.PointWrap;
            batch.RenderTarget = Texture;
            batch.Transform = Matrix.Identity;
                        
            batch.Rectangle(Texture.Bounds, new Color(0.4f, 0.4f, 0.4f));

            foreach(Rectangle rectangle in RevealedAreas)
            {
                batch.Rectangle(new Rectangle(rectangle.X + 100 - 2, rectangle.Y + 50 - 2, rectangle.Width + 4, rectangle.Height + 4), 
                    new Color(0.6f, 0.6f, 0.6f));
                batch.Rectangle(new Rectangle(rectangle.X + 100 - 1, rectangle.Y + 50 - 1, rectangle.Width + 2, rectangle.Height + 2), 
                    new Color(0.7f, 0.7f, 0.7f));
                batch.Rectangle(new Rectangle(rectangle.X + 100, rectangle.Y + 50, rectangle.Width, rectangle.Height),
                    Color.White);
            }

            batch.RenderTarget = null;
            batch.Transform = oldTransform;
            batch.SamplerState = oldSamplerState;
        }

        public void Draw(SpriteBatch batch)
        {
            BlendState oldBlendState = batch.BlendState;

            batch.BlendState = _blend;
            batch.Texture(new Rectangle(-100 * 16, -50 * 16, Texture.Width * 16, Texture.Height * 16), Texture, Color.White);

            batch.BlendState = oldBlendState;
        }
    }
}