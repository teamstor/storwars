using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.TBS.Map.Tiles;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

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
            new AnimatedTerrainTile(0, "Deep Water", new Point(0, 0), false, 2, 3);

        /// <summary>
        /// Shallow water tile.
        /// </summary>
        public static TerrainTile ShallowWater =
            new TerrainTile(1, "Shallow Water", new Point(0, 1), false);

        /// <summary>
        /// Grass tile.
        /// </summary>
        public static VariationsTerrainTile Grass = 
			new VariationsTerrainTile(2, "Grass", new Point(3, 0), false, 2);

        /// <summary>
        /// Sand.
        /// </summary>
        public static TerrainTile Sand =
            new TerrainTile(3, "Sand", new Point(5, 0), false);

        /// <summary>
        /// Raked sand.
        /// </summary>
        public static TerrainTile RakedSand =
            new TerrainTile(4, "Raked Sand", new Point(6, 0), false);

        /// <summary>
        /// Sandstone.
        /// </summary>
        public static TerrainTile Sandstone =
            new TerrainTile(5, "Sandstone", new Point(7, 0), false);

        /// <summary>
        /// Stone road.
        /// </summary>
        public static TerrainTile StoneRoad =
            new TerrainTile(6, "Stone Road", new Point(8, 0), false);

        /// <summary>
        /// Mountain.
        /// </summary>
        public static MountainTerrainTile Mountain =
            new MountainTerrainTile(7, "Mountain", new Point(9, 0), true, false, false);

        /// <summary>
        /// Trees.
        /// </summary>
        public static TreeTerrainTile Trees =
            new TreeTerrainTile(8, "Trees", new Point(15, 1), true, false, false);

        /// <summary>
        /// A stone. :O
        /// </summary>
        public static TerrainTile Stone =
            new TerrainTile(9, "Stone", new Point(10, 0), true, false, false);

        /// <summary>
        /// Empty tile for the decoration layer.
        /// </summary>
        public static TerrainTile DecorationEmpty =
            new TerrainTile(100, "Empty", new Point(15), true);

        /// <summary>
        /// Tile ID used in map data.
        /// </summary>
        public byte Id;
		
		/// <summary>
		/// Tile name.
		/// </summary>
		public virtual string Name { get; private set; }

        /// <summary>
        /// Texture slot on tiles.png.
        /// </summary>
        public Point TextureSlot
        {
            get; protected set;
        }

		/// <summary>
		/// Draws this tile.
		/// </summary>
		public virtual void Draw(SpriteBatch batch, Texture2D tileTexture, double time, Point pos, MapData data)
        {
            batch.Texture(
                new Vector2(pos.X * 16, pos.Y * 16),
                tileTexture,
                Color.White,
                Vector2.One,
                new Rectangle(TextureSlot.X * 16, TextureSlot.Y * 16, 16, 16),
                0,
                null,
                SpriteEffects.None);
        }

        /// <summary>
        /// If units can walk through/on this tile.
        /// </summary>
        public virtual bool CanWalkOn { get; private set; }

        /// <summary>
        /// If this tile should use a transition with tiles around it (lower ID = will be overlapped).
        /// </summary>
        public virtual bool UseTransition { get; private set; }

        /// <summary>
        /// If this tile should be put on the decoration layer.
        /// </summary>
        public virtual bool Decoration { get; private set; }
		
		public TerrainTile(byte id, string name, Point textureSlot, bool decoration, bool canWalkOn = true, bool useTransition = true)
		{
			Id = id;
			Name = name;
            TextureSlot = textureSlot;
			CanWalkOn = canWalkOn;
            UseTransition = useTransition;
            Decoration = decoration;
			
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