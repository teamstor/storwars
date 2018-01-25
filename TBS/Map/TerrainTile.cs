using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.TBS.Map.Tiles;

namespace TeamStor.TBS.Map
{
	/// <summary>
	/// Map terrain tiles.
	/// 16x16
	/// </summary>
	public class TerrainTile
	{
		public const string TILE_TEXTURE = "textures/tiles.png";
		
		/// <summary>
		/// Available terrain tiles.
		/// </summary>
		public static Dictionary<byte, TerrainTile> Tiles { get; } = new Dictionary<byte, TerrainTile>();

        /// <summary>
        /// Deep water tile.
        /// </summary>
        public static AnimatedTerrainTile DeepWater =
            new AnimatedTerrainTile(0, "Deep Water", new Point(0, 0), 2, 3);

        /// <summary>
        /// Shallow water tile.
        /// </summary>
        public static TerrainTile ShallowWater =
            new TerrainTile(1, "Shallow Water", new Point(0, 1));

        /// <summary>
        /// Grass tile.
        /// </summary>
        public static VariationsTerrainTile Grass = 
			new VariationsTerrainTile(2, "Grass", new Point(3, 0), 2);

        /// <summary>
        /// Sand.
        /// </summary>
        public static TerrainTile Sand =
            new TerrainTile(3, "Sand", new Point(5, 0));

        /// <summary>
        /// Raked sand.
        /// </summary>
        public static TerrainTile RakedSand =
            new TerrainTile(4, "Raked Sand", new Point(6, 0));

        /// <summary>
        /// Sandstone.
        /// </summary>
        public static TerrainTile Sandstone =
            new TerrainTile(5, "Sandstone", new Point(7, 0));

        /// <summary>
        /// Tile ID used in map data.
        /// </summary>
        public byte Id;
		
		/// <summary>
		/// Tile name.
		/// </summary>
		public virtual string Name { get; private set; }

        private Point _textureSlot;

		/// <summary>
		/// Slot on tiles.png.
		/// </summary>
		public virtual Point TextureSlot(double time, Point pos) { return _textureSlot;  }

		/// <summary>
		/// Rectangle on tiles.png.
		/// </summary>
		public Rectangle TextureRectangle(double time, Point pos)
		{
			return new Rectangle(TextureSlot(time, pos).X * 16, TextureSlot(time, pos).Y * 16, 16, 16);
		}

		/// <summary>
		/// If units can walk through/on this tile.
		/// </summary>
		public virtual bool CanWalkOn { get; private set; }
		
		public TerrainTile(byte id, string name, Point textureSlot, bool canWalkOn = true)
		{
			Id = id;
			Name = name;
            _textureSlot = textureSlot;
			CanWalkOn = canWalkOn;
			
			Tiles.Add(id, this);
		}

        public static TerrainTile FindByName(string name)
        {
            foreach(TerrainTile tile in Tiles.Values)
            {
                if(tile.Name == name)
                    return tile;
            }

            return null;
        }
	}
}