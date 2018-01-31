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
    /// 16x32 trees tiles.
    /// </summary>
    public class TreeTerrainTile : TerrainTile
    {
        public TreeTerrainTile(byte id, string name, Point textureSlot, bool decoration, bool canWalkOn = true, bool useTransition = true) : base(id, name, textureSlot, decoration, canWalkOn, useTransition)
        {
        }
        public override void Draw(Engine.Graphics.SpriteBatch batch, Texture2D tileTexture, double time, Point pos, MapData data)
        {
            batch.Texture(
                new Vector2(pos.X * 16, pos.Y * 16 - 16),
                tileTexture,
                Color.White,
                Vector2.One,
                new Rectangle(TextureSlot.X * 16, TextureSlot.Y * 16 - 16, 16, 32),
                0,
                null,
                SpriteEffects.None);
        }
    }
}
