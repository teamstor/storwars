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
    /// Animated terrain tile.
    /// </summary>
    public class AnimatedTerrainTile : TerrainTile
    {
        public int FPS
        {
            get;
            private set;
        } = 10;

        public int Frames
        {
            get;
            private set;
        }

        public AnimatedTerrainTile(byte id, string name, Point textureSlot, bool decoration, int fps, int frames, bool canWalkOn = true) : 
            base(id, name, textureSlot, decoration, canWalkOn)
        {
            FPS = fps;
            Frames = frames;
        }

        public override void Draw(Engine.Graphics.SpriteBatch batch, Texture2D tileTexture, double time, Point pos, MapData data)
        {
            TextureSlot = new Point(TextureSlot.X + (int)((time * FPS) % Frames), TextureSlot.Y);
            base.Draw(batch, tileTexture, time, pos, data);
            TextureSlot = new Point(TextureSlot.X - (int)((time * FPS) % Frames), TextureSlot.Y);
        }
    }
}
