using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using TeamStor.Engine.Tween;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Map.Editor.States
{
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
			}
		}
	}
}