using System;

namespace TeamStor.TBS.Map
{
	/// <summary>
	/// Map data.
	/// </summary>
	public class MapData
	{
		/// <summary>
		/// Map info such as name and creator.
		/// </summary>
		public MapInfo Info { get; private set; }
		
		/// <summary>
		/// Width of the map in tiles.
		/// </summary>
		public int Width { get; private set; }
		
		/// <summary>
		/// Height of the map in tiles.
		/// </summary>
		public int Height { get; private set; }
		
		/// <summary>
		/// Lowest layer - terrain.
		/// </summary>
		public byte[] Tiles { get; private set; }

		public MapData(MapInfo info, int width, int height)
		{
			Info = info;
			Width = width;
			Height = height;
			Tiles = new byte[width * height];
		}

		/// <summary>
		/// Gets the tile ID at the specified position.
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		public byte GetTileIdAt(int x, int y)
		{
			return Tiles[(y * Height) + x];
		}
		
		/// <summary>
		/// Sets the tile ID at the specified position.
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="newId">The new ID</param>
		public void SetTileIdAt(int x, int y, byte newId)
		{
			Tiles[(y * Height) + x] = newId;
		}

		/// <summary>
		/// Resizes this map. New tiles will be filled with grass.
		/// </summary>
		/// <param name="newWidth">The new width</param>
		/// <param name="newHeight">The new height</param>
		public void Resize(int newWidth, int newHeight)
		{
			Width = Width;
			Height = Height;
			
			byte[] tiles = Tiles;
			Array.Resize(ref tiles, newWidth * newHeight);
			Tiles = tiles;
		}
	}
}