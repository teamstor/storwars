using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using TeamStor.Engine.Tween;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;
using System.Collections.Generic;

namespace TeamStor.TBS.Map.Editor.States
{
    public enum TerrainTool
    {
        PaintOne,
        PaintCircle,
        PaintRectangle
    }

	public class MapEditorTerrainEditState : MapEditorModeState
	{		
		public Point SelectedTile
		{
			get
			{
				Vector2 mousePos = Input.MousePosition / BaseState.Camera.Zoom;
				mousePos.X -= BaseState.Camera.Transform.Translation.X / BaseState.Camera.Zoom;
				mousePos.Y -= BaseState.Camera.Transform.Translation.Y / BaseState.Camera.Zoom;

				Point point = new Point((int)Math.Floor(mousePos.X / 16), (int)Math.Floor(mousePos.Y / 16));

				if(point.X < 0)
					point.X = 0;
				if(point.Y < 0)
					point.Y = 0;
				
				if(point.X >= BaseState.MapData.Width)
					point.X = BaseState.MapData.Width - 1;
				if(point.Y >= BaseState.MapData.Height)
					point.Y = BaseState.MapData.Height - 1;

				return point;
			}
		}
		
		public override bool PauseEditor
		{
			get { return false; }
		}

		public override void OnEnter(GameState previousState)
		{
            List<string> tiles = new List<string>();
            foreach(TerrainTile tile in TerrainTile.Tiles.Values)
                tiles.Add(tile.Name);

            BaseState.SelectionMenus.Add("select-tile-menu", new SelectionMenu
            {
                Title = "Tiles",
                Entries = tiles,
                Rectangle = new TweenedRectangle(Game, new Rectangle(48, 114, 210, 0))
            });
		}

		public override void OnLeave(GameState nextState)
		{
            BaseState.SelectionMenus.Remove("select-tile-menu");
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            BaseState.SelectionMenus["select-tile-menu"].Title = "Tiles (selected: " + BaseState.SelectionMenus["select-tile-menu"].SelectedValue + ")";

            if(!BaseState.IsPointObscured(Input.MousePosition))
            {
            }
        }

        public override void FixedUpdate(long count)
		{
		}

		public override void Draw(SpriteBatch batch, Vector2 screenSize)
		{
			if(!BaseState.IsPointObscured(Input.MousePosition))
			{
				batch.SamplerState = SamplerState.PointWrap;
				batch.Transform = BaseState.Camera.Transform;

				float alpha = Input.Mouse(MouseButton.Left) ? 1.0f : 0.6f;

				if(Input.Mouse(MouseButton.Left))
					alpha = MathHelper.Clamp(alpha + (float)Math.Sin(Game.Time * 10f) * 0.4f, 0, 1);

				batch.Outline(new Rectangle(SelectedTile.X * 16, SelectedTile.Y * 16, 16, 16),
					Color.White * alpha, 1, false);

				batch.Reset();

                batch.Text(
                    SpriteBatch.FontStyle.MonoBold, 
                    (uint)(8 * BaseState.Camera.Zoom), 
                    "(" + SelectedTile.X + ", " + SelectedTile.Y + ")", 
                    new Vector2(SelectedTile.X * 16, SelectedTile.Y * 16) * BaseState.Camera.Zoom + BaseState.Camera.Translation - new Vector2(0, 12 * BaseState.Camera.Zoom),
                    Color.White * alpha);
			}
		}
	}
}