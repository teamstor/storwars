using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.Engine.Tween;
using TeamStor.TBS.Map.Editor.States;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Map.Editor
{
	public class MapEditorState : GameState
	{
		private enum EditMode
		{
			Terrain,
			Spawnpoints,
			Info,
			Keybinds
		}
		
        private TweenedDouble _topTextY;
        private TweenedDouble _fade;
        private TweenedDouble _topTextFade;

        private EditMode _editMode = EditMode.Terrain;

		private MapEditorModeState _state;
		
		public MapData MapData;
		public Dictionary<string, Button> Buttons = new Dictionary<string, Button>();
		public Dictionary<string, SelectionMenu> SelectionMenus = new Dictionary<string, SelectionMenu>();

		public Camera Camera { get; private set; }
		
		/// <summary>
		/// Current map editor state.
		/// </summary>
		public MapEditorModeState CurrentState
		{
			get
			{
				return _state;
			}
			set
			{
				if(_state != null)
					_state.OnLeave(value);

				if(value != null)
				{
					value.Game = Game;
					value.BaseState = this;
					value.OnEnter(_state);
				}
                
				_state = value;
			}
		}

        public override void OnEnter(GameState previousState)
		{
            Game.IsMouseVisible = true;
			MapData = new MapData(new MapInfo { Name = "Unnamed", Creator = "Unknown" }, 50, 50);
			
			Camera = new Camera(this);

            Buttons.Add("edit-terrain-mode", new Button
            {
                Text = "",
                Icon = Assets.Get<Texture2D>("textures/editor/icon_terrain.png"),
                Position = new TweenedVector2(Game, new Vector2(-200, 114)),
                Font = Game.DefaultFonts.Normal,
                Clicked = (btn) =>
                {
	                _editMode = EditMode.Terrain;
	                CurrentState = new MapEditorTerrainEditState();
                },

                Active = false
            });

            Buttons.Add("edit-spawnpoints-mode", new Button
            {
                Text = "",
                Icon = Assets.Get<Texture2D>("textures/editor/icon_spawnpoints.png"),
	            Position = new TweenedVector2(Game, new Vector2(-200, 114 + 32)),
                Font = Game.DefaultFonts.Normal,
                Clicked = (btn) =>
                {
	                _editMode = EditMode.Spawnpoints; 
	                CurrentState = new MapEditorSpawnPointEditState();
                },

                Active = false
            });

            Buttons.Add("edit-info-mode", new Button
            {
                Text = "",
                Icon = Assets.Get<Texture2D>("textures/editor/icon_info.png"),
	            Position = new TweenedVector2(Game, new Vector2(-200, 114 + 32 * 2)),
                Font = Game.DefaultFonts.Normal,
                Clicked = (btn) =>
                {
	                _editMode = EditMode.Info; 
	                CurrentState = new MapEditorEditInfoState();
                },

                Active = false
            });
			
			Buttons.Add("keybinds-help-mode", new Button
			{
				Text = "",
				Icon = Assets.Get<Texture2D>("textures/editor/icon_keybinds.png"),
				Position = new TweenedVector2(Game, new Vector2(-200, 114 + 32 * 3)),
				Font = Game.DefaultFonts.Normal,
				Clicked = (btn) =>
				{
					_editMode = EditMode.Keybinds; 
					CurrentState = new MapEditorShowKeybindsState();
				},

				Active = false
			});
			
			Buttons.Add("load", new Button
			{
				Text = "",
				Icon = Assets.Get<Texture2D>("textures/editor/icon_load.png"),
				Position = new TweenedVector2(Game, new Vector2(-200, 118 + 32 * 4)),
				Font = Game.DefaultFonts.Normal,
				Clicked = (btn) => { /* TODO */ },

				Active = false
			});
			
			Buttons.Add("exit-save", new Button
			{
				Text = "",
				Icon = Assets.Get<Texture2D>("textures/editor/icon_save.png"),
				Position = new TweenedVector2(Game, new Vector2(-200, 118 + 32 * 5)),
				Font = Game.DefaultFonts.Normal,
				Clicked = (btn) => { /* TODO */ },

				Active = false
			});
			
			Buttons.Add("exit", new Button
			{
				Text = "",
				Icon = Assets.Get<Texture2D>("textures/editor/icon_exit.png"),
				Position = new TweenedVector2(Game, new Vector2(-200, 118 + 32 * 6)),
				Font = Game.DefaultFonts.Normal,
				Clicked = (btn) => { },

				Active = false
			});

            Buttons["edit-terrain-mode"].Position.TweenTo(new Vector2(10, 114), TweenEaseType.EaseOutQuad, 0.65f);
            Buttons["edit-spawnpoints-mode"].Position.TweenTo(new Vector2(10, 114 + 32), TweenEaseType.EaseOutQuad, 0.65f);
            Buttons["edit-info-mode"].Position.TweenTo(new Vector2(10, 114 + 32 * 2), TweenEaseType.EaseOutQuad, 0.65f);
			Buttons["keybinds-help-mode"].Position.TweenTo(new Vector2(10, 114 + 32 * 3), TweenEaseType.EaseOutQuad, 0.65f);
			
			Buttons["load"].Position.TweenTo(new Vector2(10, 118 + 32 * 4), TweenEaseType.EaseOutQuad, 0.65f);
			Buttons["exit-save"].Position.TweenTo(new Vector2(10, 118 + 32 * 5), TweenEaseType.EaseOutQuad, 0.65f);
			Buttons["exit"].Position.TweenTo(new Vector2(10, 118 + 32 * 6), TweenEaseType.EaseOutQuad, 0.65f);

            _topTextY = new TweenedDouble(Game, -300);
            _topTextY.TweenTo(10, TweenEaseType.EaseOutQuad, 0.65f);

            _fade = new TweenedDouble(Game, 1.0);
            _fade.TweenTo(0, TweenEaseType.Linear, 0.5f);

            _topTextFade = new TweenedDouble(Game, 2.0);
			
			CurrentState = new MapEditorTerrainEditState();
        }

        public override void OnLeave(GameState nextState)
		{
			CurrentState.OnLeave(null);
		}

		private string CurrentHelpText
		{
			get
			{
                if(CurrentState.CurrentHelpText != "")
                    return CurrentState.CurrentHelpText;

				if(!Buttons["edit-terrain-mode"].Active && Buttons["edit-terrain-mode"].Rectangle.Contains(Input.MousePosition))
					return "Edit terrain";
				if(!Buttons["edit-spawnpoints-mode"].Active && Buttons["edit-spawnpoints-mode"].Rectangle.Contains(Input.MousePosition))
					return "Edit spawn points";
				if(!Buttons["edit-info-mode"].Active && Buttons["edit-info-mode"].Rectangle.Contains(Input.MousePosition))
					return "Edit map info";
				if(!Buttons["keybinds-help-mode"].Active && Buttons["keybinds-help-mode"].Rectangle.Contains(Input.MousePosition))
					return "Show key bindings";
				
				if(!Buttons["load"].Active && Buttons["load"].Rectangle.Contains(Input.MousePosition))
					return "Load map file";
				if(!Buttons["exit-save"].Active && Buttons["exit-save"].Rectangle.Contains(Input.MousePosition))
					return "Save map file and exit";
				if(!Buttons["exit"].Active && Buttons["exit"].Rectangle.Contains(Input.MousePosition))
					return "Exit without saving";

				return "No action selected";
			}
		}

		public override void Update(double deltaTime, double totalTime, long count)
		{
			if(Input.KeyPressed(Keys.D3))
				MapData.Resize(MapData.Width + 1, MapData.Height);
			
			if(Input.KeyPressed(Keys.D5))
				MapData.Resize(MapData.Width, MapData.Height + 1);
			
			if(Input.KeyPressed(Keys.D4) && MapData.Width > 1)
				MapData.Resize(MapData.Width - 1, MapData.Height);
			
			if(Input.KeyPressed(Keys.D6) && MapData.Height > 1)
				MapData.Resize(MapData.Width, MapData.Height - 1);

			Camera.Update(deltaTime, totalTime);
			
            Buttons["edit-terrain-mode"].Active = _editMode == EditMode.Terrain;
            Buttons["edit-spawnpoints-mode"].Active = _editMode == EditMode.Spawnpoints;
            Buttons["edit-info-mode"].Active = _editMode == EditMode.Info;
			Buttons["keybinds-help-mode"].Active = _editMode == EditMode.Keybinds;

            foreach(Button button in Buttons.Values)
                button.Update(Game);

            foreach(SelectionMenu menu in SelectionMenus.Values)
                menu.Update(Game);

            string str =
               "TBS Map Editor\n" +
               "Name: \"" + MapData.Info.Name + "\" (made by \"" + MapData.Info.Creator + "\")\n" +
               "Size: " + MapData.Width + "x" + MapData.Height + "\n" +
			   "[LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL]";

            Vector2 measure = Game.DefaultFonts.Bold.Measure(15, str);
            Rectangle topTextRectangle = new Rectangle(10, (int)_topTextY, (int)(measure.X + 20), (int)(measure.Y + 20));

            if(topTextRectangle.Contains(Input.MousePosition) && !topTextRectangle.Contains(Input.PreviousMousePosition))
                _topTextFade.TweenTo(0.6, TweenEaseType.EaseOutQuad, 0.4f);
            else if(!topTextRectangle.Contains(Input.MousePosition) && topTextRectangle.Contains(Input.PreviousMousePosition))
                _topTextFade.TweenTo(2.0, TweenEaseType.EaseOutQuad, 0.4f);
			
			CurrentState.Update(deltaTime, totalTime, count);
        }

		public bool IsPointObscured(Vector2 point)
		{
			foreach(Button btn in Buttons.Values)
			{
				if(btn.Rectangle.Contains(point))
					return true;
			}

            foreach(SelectionMenu menu in SelectionMenus.Values)
            {
                if(menu.Rectangle.Value.Contains(point))
                    return true;
            }

            return false;
		}

        public override void FixedUpdate(long count)
		{
			CurrentState.FixedUpdate(count);
		}

		public override void Draw(SpriteBatch batch, Vector2 screenSize)
		{
			batch.Transform = Camera.Transform;
			batch.SamplerState = SamplerState.PointClamp;

            MapData.Draw(Game, Assets, new Rectangle(
                (int)-(Camera.Translation.X / Math.Ceiling(Camera.Zoom.Value)),
                (int)-(Camera.Translation.Y / Math.Ceiling(Camera.Zoom.Value)), 
                (int)(screenSize.X / Math.Floor(Camera.Zoom.Value)), 
                (int)(screenSize.Y / Math.Floor(Camera.Zoom.Value))));
			
			batch.Outline(new Rectangle(0, 0, MapData.Width * 16, MapData.Height * 16),
				Color.White, 1, false);
			
			//batch.Texture(new Vector2(64, 64), Assets.Get<Texture2D>("textures/buildings/mine_soviet.png"), Color.White);
			
			batch.Reset();
			
			CurrentState.Draw(batch, screenSize);
			
			string str = 
				"TBS Map Editor\n" +
				"Name: \"" + MapData.Info.Name + "\" (made by \"" + MapData.Info.Creator + "\")\n" +
				"Size: " + MapData.Width + "x" + MapData.Height + "\n" +
				"[LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL]";

			Vector2 measure = Game.DefaultFonts.Bold.Measure(15, str);
			batch.Rectangle(new Rectangle(10, (int)_topTextY, (int)(measure.X + 20), (int)(measure.Y + 20)),
				Color.Black * (MathHelper.Clamp(_topTextFade, 0, 1) * 0.85f));

			str = str.Replace("[LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL]", CurrentHelpText)
				.Replace("TBS Map Editor\n", "");

			batch.Text(SpriteBatch.FontStyle.Bold, 15, "TBS Map Editor", new Vector2(20, (int)_topTextY + 10),
				Color.White * MathHelper.Clamp(_topTextFade, 0, 1));
			batch.Text(SpriteBatch.FontStyle.Bold, 15, str, new Vector2(20, (int)_topTextY + 10 + (15 * 1.25f)),
				Color.White * (MathHelper.Clamp(_topTextFade, 0, 1) * 0.8f));
			
			foreach(Button button in Buttons.Values)
                button.Draw(Game);

            foreach(SelectionMenu menu in SelectionMenus.Values)
                menu.Draw(Game);

            batch.Rectangle(new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), Color.Black * _fade);
		}
	}
}