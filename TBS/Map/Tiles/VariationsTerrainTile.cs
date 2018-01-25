using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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

        public VariationsTerrainTile(byte id, string name, Point textureSlot, int variations, bool canWalkOn = true) : base(id, name, textureSlot, canWalkOn)
        {
            Variations = variations;
        }

        public override Point TextureSlot(double time, Point pos)
        {
            Random rand = new Random(pos.X ^ pos.Y);
            return new Point(base.TextureSlot(time, pos).X + rand.Next(0, Variations), base.TextureSlot(time, pos).Y);
        }
    }
}
