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

        private static int[] _randomValues = new int[100 * 100];
        private bool _hasValues = false;

        public override void Draw(Engine.Graphics.SpriteBatch batch, Texture2D tileTexture, double time, Point pos, MapData data)
        {
            if(!_hasValues)
            {
                Random random = new Random();
                for(int i = 0; i < _randomValues.Length; i++)
                    _randomValues[i] = random.Next();

                _hasValues = true;
            }
            int shift = _randomValues[((pos.Y * data.Width) + pos.X) % _randomValues.Length] % Variations;

            TextureSlot = new Point(TextureSlot.X + shift, TextureSlot.Y);
            base.Draw(batch, tileTexture, time, pos, data);
            TextureSlot = new Point(TextureSlot.X - shift, TextureSlot.Y);
        }
    }
}
