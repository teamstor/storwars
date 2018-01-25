using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamStor.Engine;
using TeamStor.Engine.Tween;

namespace TeamStor.TBS.Map.Editor
{
	/// <summary>
	/// Map editor camera.
	/// </summary>
	public class Camera
	{
        private bool _dragging;
		private MapEditorState _state;
		
		/// <summary>
		/// Current zoom.
		/// </summary>
		public TweenedDouble Zoom;

		/// <summary>
		/// Current translation.
		/// </summary>
		public Vector2 Translation;
				
		/// <summary>
		/// Current transform.
		/// </summary>
		public Matrix Transform
		{
			get
			{
				return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(Translation.X, Translation.Y, 0);
			}
		}

		public Camera(MapEditorState state)
		{
			_state = state;
			Zoom = new TweenedDouble(state.Game, 2);
		}

		public void Update(double deltaTime, double totalTime)
		{
			if(!_state.CurrentState.PauseEditor)
			{
				if(_state.Input.KeyPressed(Keys.D2))
				{
					if(Zoom.TargetValue <= 4)
						Zoom.TweenTo(Zoom.TargetValue * 2, TweenEaseType.EaseOutCubic, 0.25);
				}

				if(_state.Input.KeyPressed(Keys.D1))
				{
					if(Zoom.TargetValue >= 2)
						Zoom.TweenTo(Zoom.TargetValue / 2, TweenEaseType.EaseOutCubic, 0.25);
				}

                if(_state.Input.Mouse(MouseButton.Right))
                    Translation += _state.Input.MouseDelta;

                if(_state.Input.MouseReleased(MouseButton.Right))
                    _dragging = false;
			}

            if(Translation.X > 200)
                Translation.X = 200;
            if(Translation.Y > 200)
                Translation.Y = 200;
            if(Translation.X < -(_state.MapData.Width * (16 * Zoom) - _state.Game.GraphicsDevice.Viewport.Bounds.Width) - 200)
                Translation.X = -(_state.MapData.Width * (16 * Zoom) - _state.Game.GraphicsDevice.Viewport.Bounds.Width) - 200;
            if(Translation.Y < -(_state.MapData.Height * (16 * Zoom) - _state.Game.GraphicsDevice.Viewport.Bounds.Height) - 200)
                Translation.Y = -(_state.MapData.Height * (16 * Zoom) - _state.Game.GraphicsDevice.Viewport.Bounds.Height) - 200;

            if(_state.Game.GraphicsDevice.Viewport.Bounds.Width / 2 > _state.MapData.Width * 8 * Zoom)
				Translation.X = _state.Game.GraphicsDevice.Viewport.Bounds.Width / 2 - _state.MapData.Width * 8 * Zoom;
			if(_state.Game.GraphicsDevice.Viewport.Bounds.Height / 2 > _state.MapData.Height * 8 * Zoom)
				Translation.Y = _state.Game.GraphicsDevice.Viewport.Bounds.Height / 2 - _state.MapData.Height * 8 * Zoom;
		}
	}
}