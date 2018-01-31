using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine.Graphics;

namespace TeamStor.TBS.Map.Tiles
{
    /// <summary>
    /// Mountain terrain tile that changes based on tiles around it.
    /// </summary>
    public class MountainTerrainTile : TerrainTile
    {
        public MountainTerrainTile(byte id, string name, Point textureSlot, bool decoration, bool canWalkOn = true, bool useTransition = true) : 
            base(id, name, textureSlot, decoration, canWalkOn, useTransition)
        {
        }

        private bool IsCompleteMountain(Point pos, MapData data)
        {
            for(int x = pos.X - 1; x <= pos.X + 1; x++)
            {
                for(int y = pos.Y - 1; y <= pos.Y + 1; y++)
                {
                    if(x < 0 || y < 0 || x >= data.Width || y >= data.Height)
                        return false;
                    if(data.GetTileIdAt(true, x, y) != Id)
                        return false;
                }
            }

            return true;
        }

        public override void Draw(Engine.Graphics.SpriteBatch batch, Texture2D tileTexture, double time, Point pos, MapData data)
        {
            Point textureSlot = new Point(10, 0);
            Vector2 offset = Vector2.Zero;
            float rotation = 0;
            SpriteEffects effect = SpriteEffects.None;
            
            if(IsCompleteMountain(pos, data))
                textureSlot = new Point(11, 0);
            else
            {
                if(IsCompleteMountain(pos + new Point(1, 0), data) && 
                   IsCompleteMountain(pos + new Point(0, 1), data))
                    textureSlot = new Point(13, 0);
                else if(IsCompleteMountain(pos + new Point(-1, 0), data) &&
                        IsCompleteMountain(pos + new Point(0, 1), data))
                {
                    textureSlot = new Point(13, 0);
                    effect = SpriteEffects.FlipHorizontally;
                }
                else if(IsCompleteMountain(pos + new Point(1, 0), data) &&
                        IsCompleteMountain(pos + new Point(0, -1), data))
                {
                    textureSlot = new Point(13, 0);
                    effect = SpriteEffects.FlipVertically;
                }
                else if(IsCompleteMountain(pos + new Point(-1, 0), data) &&
                        IsCompleteMountain(pos + new Point(0, -1), data))
                {
                    textureSlot = new Point(13, 0);
                    effect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
                }
                else if(IsCompleteMountain(pos + new Point(1, 0), data))
                {
                    textureSlot = new Point(9, 0);
                    effect = SpriteEffects.FlipHorizontally;
                }
                else if(IsCompleteMountain(pos + new Point(0, -1), data))
                {
                    textureSlot = new Point(9, 0);
                    rotation = MathHelper.PiOver2;
                    offset = new Vector2(16, 0);
                }
                else if(IsCompleteMountain(pos + new Point(0, 1), data))
                {
                    textureSlot = new Point(9, 0);
                    rotation = MathHelper.Pi + MathHelper.PiOver2;
                    offset = new Vector2(0, 16);
                }
                else if(IsCompleteMountain(pos + new Point(-1, 0), data))
                    textureSlot = new Point(9, 0);
                else if(IsCompleteMountain(pos + new Point(-1, -1), data))
                {
                    textureSlot = new Point(12, 0);
                    rotation = MathHelper.PiOver2;
                    offset = new Vector2(16, 0);
                }
                else if(IsCompleteMountain(pos + new Point(1, -1), data))
                {
                    textureSlot = new Point(12, 0);
                    rotation = MathHelper.Pi;
                    offset = new Vector2(16, 16);
                }
                else if(IsCompleteMountain(pos + new Point(1, 1), data))
                {
                    textureSlot = new Point(12, 0);
                    effect = SpriteEffects.FlipHorizontally;
                }
                else if(IsCompleteMountain(pos + new Point(-1, 1), data))
                    textureSlot = new Point(12, 0);
            }

            batch.Texture(
                new Vector2(pos.X * 16, pos.Y * 16) + offset,
                tileTexture,
                Color.White,
                Vector2.One,
                new Rectangle(textureSlot.X * 16, textureSlot.Y * 16, 16, 16),
                rotation,
                null,
                effect);
        }
    }
}
