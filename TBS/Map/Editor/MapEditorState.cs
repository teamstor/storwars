using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Map.Editor
{
	public class MapEditorState : GameState
	{
		private MapData _mapData;
		
		public override void OnEnter(GameState previousState)
		{
			Game.IsMouseVisible = true;
			_mapData = new MapData(new MapInfo { Name = "Unnamed", Creator = "Unknown" }, 80, 80);
		}

		public override void OnLeave(GameState nextState)
		{
		}

		public override void Update(double deltaTime, double totalTime, long count)
		{
		}

		public override void FixedUpdate(long count)
		{
		}

		public override void Draw(SpriteBatch batch, Vector2 screenSize)
		{
			batch.Transform = Matrix.CreateScale(2);
			batch.SamplerState = SamplerState.PointClamp;
			
			batch.Reset();
			
			string str = 
				"TBS Map Editor\n" + 
				"Name: \"" + _mapData.Info.Name + "\" (made by \"" + _mapData.Info.Creator + "\")\n" +
				"Size: " + _mapData.Width + "x" + _mapData.Height;

			Vector2 measure = Game.DefaultFonts.Bold.Measure(15, str);
			batch.Rectangle(new Rectangle(10, 10, (int)(measure.X + 20), (int)(measure.Y + 20)), Color.Black * 0.85f);
			
			batch.Text(SpriteBatch.FontStyle.Bold, 15, "TBS Map Editor", new Vector2(20, 20), Color.White);
			batch.Text(SpriteBatch.FontStyle.Normal, 15, str.Replace("TBS Map Editor\n", ""), new Vector2(20, 20 + (15 * 1.25f)), Color.White * 0.8f);
			
			Rectangle iconsRectangle = new Rectangle(10, 20 + (int)(measure.Y + 20), 24 + 8, 28 * 2 + 24 + 8);
			Rectangle terrainIconRectangle = new Rectangle(iconsRectangle.X + 4, iconsRectangle.Y + 4, 24, 24);
			Rectangle spawnPointsIconRectangle = new Rectangle(iconsRectangle.X + 4, terrainIconRectangle.Y + terrainIconRectangle.Height + 4, 24, 24);
			Rectangle unitsIconRectangle = new Rectangle(iconsRectangle.X + 4, spawnPointsIconRectangle.Y + spawnPointsIconRectangle.Height + 4, 24, 24);
			
			batch.Rectangle(iconsRectangle, Color.Black * 0.85f);
			
			batch.Texture(terrainIconRectangle, Assets.Get<Texture2D>("textures/editor/icon_terrain.png"), 
				Color.White * (terrainIconRectangle.Contains(Input.MousePosition) ? 1.0f : 0.7f));
			batch.Texture(spawnPointsIconRectangle, Assets.Get<Texture2D>("textures/editor/icon_spawnpoints.png"), 
				Color.White * (spawnPointsIconRectangle.Contains(Input.MousePosition) ? 1.0f : 0.7f));
			batch.Texture(unitsIconRectangle, Assets.Get<Texture2D>("textures/editor/icon_units.png"), 
				Color.White * (unitsIconRectangle.Contains(Input.MousePosition) ? 1.0f : 0.7f));

			string tooltipText = "";
			int tooltipY = 0;
			if(terrainIconRectangle.Contains(Input.MousePosition))
			{
				tooltipText = "Edit terrain";
				tooltipY = terrainIconRectangle.Y - 4;
			}

			if(spawnPointsIconRectangle.Contains(Input.MousePosition))
			{
				tooltipText = "Edit spawn points";
				tooltipY = spawnPointsIconRectangle.Y - 4;
			}

			if(unitsIconRectangle.Contains(Input.MousePosition))
			{
				tooltipText = "Edit units";
				tooltipY = unitsIconRectangle.Y - 4;
			}

			if(tooltipText != "")
			{
				measure = Game.DefaultFonts.Normal.Measure(15, tooltipText);
				
				batch.Rectangle(new Rectangle(iconsRectangle.X + iconsRectangle.Width, tooltipY, (int)(measure.X + 8), 32), Color.Black * 0.85f);
				batch.Text(SpriteBatch.FontStyle.Normal, 15, tooltipText, new Vector2(iconsRectangle.X + iconsRectangle.Width + 4, tooltipY + 6), Color.White);
			}
		}
	}
}