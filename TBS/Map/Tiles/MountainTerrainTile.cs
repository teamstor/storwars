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

        public override void Draw(Engine.Graphics.SpriteBatch batch, Texture2D tileTexture, double time, Point pos, MapData data)
        {
            int tileLeft = -1;
            int tileRight = -1;
            int tileUp = -1;
            int tileDown = -1;

            if(pos.X - 1 >= 0)
                tileLeft = data.GetTileIdAt(true, pos.X - 1, pos.Y);

            if(pos.X + 1 < data.Width)
                tileRight = data.GetTileIdAt(true, pos.X + 1, pos.Y);

            if(pos.Y - 1 >= 0)
                tileUp = data.GetTileIdAt(true, pos.X, pos.Y - 1);

            if(pos.Y + 1 < data.Height)
                tileDown = data.GetTileIdAt(true, pos.X, pos.Y + 1);

            Point textureSlot = new Point(10, 0);
            Vector2 offset = Vector2.Zero;
            float rotation = 0;
            SpriteEffects effect = SpriteEffects.None;

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
