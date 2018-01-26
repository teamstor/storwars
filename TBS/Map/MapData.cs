using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
using Game = TeamStor.Engine.Game;
using System.Collections.Generic;

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

        private static Dictionary<Game, Texture2D> _transitionTextures = new Dictionary<Game, Texture2D>();
        private static Dictionary<Game, HashSet<byte>> _transitionTextureAvailableTiles = new Dictionary<Game, HashSet<byte>>();
        private static bool[] _transitionMask;

        /// <summary>
        /// Draws this map data (or part of it).
        /// </summary>
        /// <param name="game">Game class.</param>
        /// <param name="assets">Assets manager to use</param>
        /// <param name="rectangle">The cropped part of the map to draw. null - draw whole map</param>
        public void Draw(Game game, AssetsManager assets, Rectangle? rectangle = null)
        {
            int x = 0;
            int y = 0;

            // Draw tiles
            for(x = 0; x < Width; x++)
            {
                for(y = 0; y < Height; y++)
                {
                    if(!rectangle.HasValue || rectangle.Value.Intersects(new Rectangle(x * 16, y * 16, 16, 16)))
                    {
                        byte tile = GetTileIdAt(x, y);

                        game.Batch.Texture(
                            new Vector2(x * 16, y * 16),
                            assets.Get<Texture2D>(TerrainTile.TILE_TEXTURE),
                            Color.White,
                            Vector2.One,
                            TerrainTile.Tiles[tile].TextureRectangle(game.Time, new Point(x, y)));
                    }
                }
            }

            // Draw transitions
            for(x = 0; x < Width; x++)
            {
                for(y = 0; y < Height; y++)
                {
                    if(!rectangle.HasValue || rectangle.Value.Intersects(new Rectangle(x * 16, y * 16, 16, 16)))
                    {
                        byte tile = GetTileIdAt(x, y);

                        if(!_transitionTextures.ContainsKey(game))
                        {
                            _transitionTextures.Add(game, new Texture2D(game.GraphicsDevice, 256, 256));
                            _transitionTextureAvailableTiles.Add(game, new HashSet<byte>());
                        }

                        if(!_transitionTextureAvailableTiles[game].Contains(tile))
                        {
                            if(_transitionMask == null)
                            {
                                _transitionMask = new bool[16 * 16];
                                Color[] data = new Color[16 * 16];
                                game.Assets.Get<Texture2D>("textures/tile_transition.png").GetData(data);

                                for(int i = 0; i < data.Length; i++)
                                    _transitionMask[i] = data[i] == Color.Black;
                            }

                            Color[] tileData = new Color[16 * 16];
                            assets.Get<Texture2D>(TerrainTile.TILE_TEXTURE).GetData(0, TerrainTile.Tiles[tile].TextureRectangle(0, Point.Zero), tileData, 0, 16 * 16);

                            Color[] tileTransition = new Color[16 * 16];
                            for(int i = 0; i < tileTransition.Length; i++)
                                tileTransition[i] = _transitionMask[i] ? tileData[i] : Color.Transparent;
                            _transitionTextures[game].SetData(0, TerrainTile.Tiles[tile].TextureRectangle(0, Point.Zero), tileTransition, 0, 16 * 16);

                            _transitionTextureAvailableTiles[game].Add(tile);
                        }

                        Point[] points = new Point[]
                        {
                            new Point(x - 1, y),
                            new Point(x + 1, y),
                            new Point(x, y - 1),
                            new Point(x, y + 1)
                        };

                        foreach(Point point in points)
                        {
                            if(point.X > 0 && point.Y > 0 && point.X < Width && point.Y < Height && GetTileIdAt(point.X, point.Y) < tile)
                            {
                                float rotation = 0;
                                SpriteEffects effects = SpriteEffects.None;

                                if(point == new Point(x + 1, y))
                                    effects = SpriteEffects.FlipHorizontally;
                                if(point == new Point(x, y - 1))
                                    rotation = MathHelper.PiOver2;
                                if(point == new Point(x, y + 1))
                                    rotation = MathHelper.Pi + MathHelper.PiOver2;

                                game.Batch.Texture(
                                    new Vector2(
                                        point.X * 16 + (rotation == MathHelper.PiOver2 ? 16 : 0), 
                                        point.Y * 16 + (rotation == MathHelper.Pi + MathHelper.PiOver2 ? 16 : 0)),
                                    _transitionTextures[game],
                                    Color.White,
                                    Vector2.One,
                                    TerrainTile.Tiles[tile].TextureRectangle(0, Point.Zero),
                                    rotation, null, effects);
                            }
                        }
                    }
                }
            }
        }
	}
}