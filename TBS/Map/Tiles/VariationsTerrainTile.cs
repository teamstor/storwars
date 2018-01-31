using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamStor.TBS.Map.Tiles
{
    /// <summary>
    /// Terrain tile with multiple variations.
    /// </summary>
    public class VariationsTerrainTile : TerrainTile
    {
        public int Variations
        {
            get;
            private set;
        }

        public VariationsTerrainTile(byte id, string name, Point textureSlot, bool decoration, int variations, bool canWalkOn = true) : 
            base(id, name, textureSlot, decoration, canWalkOn)
        {
            Variations = variations;
        }

        public override void Draw(Engine.Graphics.SpriteBatch batch, Texture2D tileTexture, double time, Point pos, MapData data)
        {
            int shift = new Random(pos.X ^ pos.Y).Next(0, Variations);

            TextureSlot = new Point(TextureSlot.X + shift, TextureSlot.Y);
            base.Draw(batch, tileTexture, time, pos, data);
            TextureSlot = new Point(TextureSlot.X - shift, TextureSlot.Y);
        }
    }
}
