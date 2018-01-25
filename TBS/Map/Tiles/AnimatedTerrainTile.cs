using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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

        public AnimatedTerrainTile(byte id, string name, Point textureSlot, int fps, int frames, bool canWalkOn = true) : base(id, name, textureSlot, canWalkOn)
        {
            FPS = fps;
            Frames = frames;
        }

        public override Point TextureSlot(double time, Point pos)
        {
            return new Point(base.TextureSlot(time, pos).X + (int)((time * FPS) % Frames), base.TextureSlot(time, pos).Y);
        }
    }
}
