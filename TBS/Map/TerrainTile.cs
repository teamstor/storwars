using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
		/// Grass tile.
		/// Can walk on - yes
		/// </summary>
		public static TerrainTile Grass = 
			new TerrainTile(0, "Grass", new Point(0, 0));

		/// <summary>
		/// Tile ID used in map data.
		/// </summary>
		public byte Id;
		
		/// <summary>
		/// Tile name.
		/// </summary>
		public virtual string Name { get; private set; }

		/// <summary>
		/// Slot on tiles.png.
		/// </summary>
		public virtual Point TextureSlot { get; private set; }

		/// <summary>
		/// Rectangle on tiles.png.
		/// </summary>
		public Rectangle TextureRectangle
		{
			get
			{
				return new Rectangle(TextureSlot.X * 16, TextureSlot.Y * 16, 16, 16);
			}
		}

		/// <summary>
		/// If units can walk through/on this tile.
		/// </summary>
		public virtual bool CanWalkOn { get; private set; }
		
		public TerrainTile(byte id, string name, Point textureSlot, bool canWalkOn = true)
		{
			Id = id;
			Name = name;
			TextureSlot = textureSlot;
			CanWalkOn = canWalkOn;
			
			Tiles.Add(id, this);
		}
	}
}