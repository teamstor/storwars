﻿using Microsoft.Xna.Framework;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;

namespace TeamStor.TBS.Map.Editor.States
{
	public class MapEditorShowKeybindsState : MapEditorModeState
	{
		public const string KEY_BINDINGS = 
			"Left-click: Select or edit\n" + 
			"Right-click: Move camera\n\n" +
			"1: Zoom out\n" +
			"2: Zoom in\n\n" +
			"3: Grow map horizontally\n" +
			"4: Shrink map horizontally\n" +
			"5: Grow map vertically\n" +
			"6: Shrink map vertically";
		
		public override bool PauseEditor
		{
			get { return true; }
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
			batch.Rectangle(new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), Color.Black * 0.8f);

			Vector2 measure = Game.DefaultFonts.Normal.Measure(16, KEY_BINDINGS);
			batch.Text(SpriteBatch.FontStyle.Normal, 16, KEY_BINDINGS, new Vector2(screenSize.X / 2 - measure.X / 2, screenSize.Y / 2 - measure.Y / 2), Color.White);
		}
	}
}