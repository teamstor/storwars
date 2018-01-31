using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
using Game = TeamStor.Engine.Game;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
		public MapInfo Info;
		
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

        /// <summary>
        /// Lower layer - terrain decorations.
        /// </summary>
        public byte[] DecorationTiles { get; private set; }

		/// <summary>
		/// Team spawn points.
		/// </summary>
		public Dictionary<Team, Point> SpawnPoints { get; private set; } = new Dictionary<Team, Point>();

		public MapData(MapInfo info, int width, int height)
		{
			Info = info;
			Width = width;
			Height = height;

			Tiles = new byte[width * height];
            DecorationTiles = new byte[width * height];

            for(int i = 0; i < Tiles.Length; i++)
                Tiles[i] = TerrainTile.DeepWater.Id;
            for(int i = 0; i < DecorationTiles.Length; i++)
                DecorationTiles[i] = TerrainTile.DecorationEmpty.Id;
			
			SpawnPoints.Add(Team.Red, Point.Zero);
			SpawnPoints.Add(Team.Blue, new Point(2, 0));
			SpawnPoints.Add(Team.Green, new Point(4, 0));
			SpawnPoints.Add(Team.Yellow, new Point(6, 0));
		}

		/// <summary>
		/// Saves this map to a file.
		/// </summary>
		/// <param name="filename">The file name to use.</param>
		public void Save(string filename)
		{
			using(BinaryWriter writer = new BinaryWriter(new FileStream(filename, FileMode.Create), Encoding.UTF8))
			{
				writer.Write("STOR Map");
				writer.Write(Info.Name);
				writer.Write(Info.Creator);
				writer.Write(Width);
				writer.Write(Height);

				foreach(byte b in Tiles)
					writer.Write(b);
				
				foreach(byte b in DecorationTiles)
					writer.Write(b);

				foreach(Team t in Enum.GetValues(typeof(Team)))
				{
					writer.Write(SpawnPoints[t].X);
					writer.Write(SpawnPoints[t].Y);
				}
			}
		}
		
		/// <summary>
		/// Loads a map from a file.
		/// </summary>
		/// <param name="filename">The file name to use.</param>
		public static MapData Load(string filename)
		{
			MapInfo info = new MapInfo();
			int width = 0;
			int height = 0;
			
			using(BinaryReader reader = new BinaryReader(new FileStream(filename, FileMode.Open), Encoding.UTF8))
			{
				if(reader.ReadString() != "STOR Map")
					throw new Exception("Not a valid map file");

				info.Name = reader.ReadString();
				info.Creator = reader.ReadString();
				width = reader.ReadInt32();
				height = reader.ReadInt32();
				
				MapData data = new MapData(info, width, height);
				for(int i = 0; i < width * height; i++)
					data.SetTileIdAt(false, i % width, i / width, reader.ReadByte());
				for(int i = 0; i < width * height; i++)
					data.SetTileIdAt(true, i % width, i / width, reader.ReadByte());
				
				foreach(Team t in Enum.GetValues(typeof(Team)))
					data.SpawnPoints[t] = new Point(reader.ReadInt32(), reader.ReadInt32());

				return data;
			}
		}

        /// <summary>
        /// Gets the tile ID at the specified position.
        /// </summary>
        /// <param name="decorationLayer">If this should use the decoration layer.</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        public byte GetTileIdAt(bool decorationLayer, int x, int y)
		{
            if(decorationLayer)
                return DecorationTiles[(y * Width) + x];
			return Tiles[(y * Width) + x];
		}

        /// <summary>
        /// Sets the tile ID at the specified position.
        /// </summary>
        /// <param name="decorationLayer">If this should use the decoration layer.</param>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="newId">The new ID</param>
        public void SetTileIdAt(bool decorationLayer, int x, int y, byte newId)
		{
            if(decorationLayer)
                DecorationTiles[(y * Width) + x] = newId;
            else
			    Tiles[(y * Width) + x] = newId;
		}

		/// <summary>
		/// Resizes this map. New tiles will be filled with grass.
		/// </summary>
		/// <param name="newWidth">The new width</param>
		/// <param name="newHeight">The new height</param>
		public void Resize(int newWidth, int newHeight)
		{
            int oldWidth = Width;
            int oldHeight = Height;

			Width = newWidth;
			Height = newHeight;
			
			byte[] tiles = Tiles;
            byte[] oldTiles = new byte[oldWidth * oldHeight];
            Array.Copy(tiles, oldTiles, oldWidth * oldHeight);

			Array.Resize(ref tiles, newWidth * newHeight);

            Tiles = tiles;
            for(int i = 0; i < Tiles.Length; i++)
                Tiles[i] = TerrainTile.DeepWater.Id;

            for(int x = 0; x < Math.Min(oldWidth, Width); x++)
            {
                for(int y = 0; y < Math.Min(oldHeight, Height); y++)
                    SetTileIdAt(false, x, y, oldTiles[(y * oldWidth) + x]);
            }

            tiles = DecorationTiles;
            Array.Copy(DecorationTiles, oldTiles, oldWidth * oldHeight);

            Array.Resize(ref tiles, newWidth * newHeight);

            DecorationTiles = tiles;
            for(int i = 0; i < DecorationTiles.Length; i++)
                DecorationTiles[i] = TerrainTile.DecorationEmpty.Id;

            for(int x = 0; x < Math.Min(oldWidth, Width); x++)
            {
                for(int y = 0; y < Math.Min(oldHeight, Height); y++)
                    SetTileIdAt(true, x, y, oldTiles[(y * oldWidth) + x]);
            }

			foreach(Team team in Enum.GetValues(typeof(Team)))
			{
				if(SpawnPoints[team].X >= Width)
					SpawnPoints[team] = new Point(Width - 1, SpawnPoints[team].Y);
				if(SpawnPoints[team].Y >= Height)
					SpawnPoints[team] = new Point(SpawnPoints[team].X, Height - 1);
			}
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
        public void Draw(bool decorationLayer, Game game, AssetsManager assets, Rectangle? rectangle = null)
        {
            int x = 0;
            int y = 0;
            Texture2D tileTexture = assets.Get<Texture2D>(TerrainTile.TILE_TEXTURE);

            // Draw tiles
            for(x = 0; x < Width; x++)
            {
                for(y = 0; y < Height; y++)
                {
                    if(!rectangle.HasValue || rectangle.Value.Intersects(new Rectangle(x * 16, y * 16, 16, 16)))
                    {
                        byte tile = GetTileIdAt(decorationLayer, x, y);
                        if(tile != TerrainTile.DecorationEmpty.Id)
                            TerrainTile.Tiles[tile].Draw(game.Batch, tileTexture, game.Time, new Point(x, y), this);
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
                        byte tile = GetTileIdAt(decorationLayer, x, y);

                        if(tile != TerrainTile.DecorationEmpty.Id)
                        {
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

                                Rectangle textureRectangle = new Rectangle(
                                    TerrainTile.Tiles[tile].TextureSlot.X * 16,
                                    TerrainTile.Tiles[tile].TextureSlot.Y * 16,
                                    16,
                                    16);

                                Color[] tileData = new Color[16 * 16];
                                assets.Get<Texture2D>(TerrainTile.TILE_TEXTURE).GetData(0, textureRectangle, tileData, 0, 16 * 16);

                                Color[] tileTransition = new Color[16 * 16];
                                for(int i = 0; i < tileTransition.Length; i++)
                                    tileTransition[i] = _transitionMask[i] ? tileData[i] : Color.Transparent;
                                _transitionTextures[game].SetData(0, textureRectangle, tileTransition, 0, 16 * 16);

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
                                if(point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height &&
                                    GetTileIdAt(decorationLayer, point.X, point.Y) < tile &&
                                    TerrainTile.Tiles[GetTileIdAt(decorationLayer, point.X, point.Y)].UseTransition &&
                                    TerrainTile.Tiles[tile].UseTransition)
                                {
                                    float rotation = 0;
                                    SpriteEffects effects = SpriteEffects.None;

                                    if(point == new Point(x + 1, y))
                                        effects = SpriteEffects.FlipHorizontally;
                                    if(point == new Point(x, y - 1))
                                        rotation = MathHelper.PiOver2;
                                    if(point == new Point(x, y + 1))
                                        rotation = MathHelper.Pi + MathHelper.PiOver2;

                                    Rectangle textureRectangle = new Rectangle(
                                        TerrainTile.Tiles[tile].TextureSlot.X * 16,
                                        TerrainTile.Tiles[tile].TextureSlot.Y * 16,
                                        16,
                                        16);

                                    game.Batch.Texture(
                                        new Vector2(
                                            point.X * 16 + (rotation == MathHelper.PiOver2 ? 16 : 0),
                                            point.Y * 16 + (rotation == MathHelper.Pi + MathHelper.PiOver2 ? 16 : 0)),
                                        _transitionTextures[game],
                                        Color.White,
                                        Vector2.One,
                                        textureRectangle,
                                        rotation, null, effects);
                                }
                            }
                        }
                    }
                }
            }
        }
	}
}