using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

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
			return Tiles[(y * Width) + x];
		}
		
		/// <summary>
		/// Sets the tile ID at the specified position.
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="newId">The new ID</param>
		public void SetTileIdAt(int x, int y, byte newId)
		{
			Tiles[(y * Width) + x] = newId;
		}

		/// <summary>
		/// Resizes this map. New tiles will be filled with grass.
		/// </summary>
		/// <param name="newWidth">The new width</param>
		/// <param name="newHeight">The new height</param>
		public void Resize(int newWidth, int newHeight)
		{
			Width = newWidth;
			Height = newHeight;
			
			byte[] tiles = Tiles;
			Array.Resize(ref tiles, newWidth * newHeight);
			Tiles = tiles;
		}

        /// <summary>
        /// Draws this map data (or part of it).
        /// </summary>
        /// <param name="batch">The batch to use when drawing.</param>
        /// <param name="assets">Assets manager to use</param>
        /// <param name="rectangle">The cropped part of the map to draw. null - draw whole map</param>
        public void Draw(SpriteBatch batch, AssetsManager assets, Rectangle? rectangle = null)
        {
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    if(!rectangle.HasValue || rectangle.Value.Intersects(new Rectangle(x * 16, y * 16, 16, 16)))
                    {
                        batch.Texture(
                            new Vector2(x * 16, y * 16),
                            assets.Get<Texture2D>(TerrainTile.TILE_TEXTURE),
                            Color.White,
                            Vector2.One,
                            TerrainTile.Tiles[GetTileIdAt(x, y)].TextureRectangle);
                    }
                }
            }
        }
	}
}