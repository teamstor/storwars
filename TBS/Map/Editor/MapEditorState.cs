using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Map.Editor
{
	public class MapEditorState : GameState
	{
        private Dictionary<string, Button> _buttons = new Dictionary<string, Button>();
        private MapData _mapData;

        private TweenValue _topTextY;
        private TweenValue _fade;
        private TweenValue _topTextFade;

        private EditMode _editMode = EditMode.Terrain;

        public override void OnEnter(GameState previousState)
		{
            Game.IsMouseVisible = true;
			_mapData = new MapData(new MapInfo { Name = "Unnamed", Creator = "Unknown" }, 100, 100);

            _buttons.Add("edit-terrain-mode", new Button
            {
                Text = "",
                Icon = Assets.Get<Texture2D>("textures/editor/icon_terrain.png"),
                X = new TweenValue(Game, -200),
                Y = new TweenValue(Game, 90),
                Font = Game.DefaultFonts.Normal,
                Clicked = (btn) => { _editMode = EditMode.Terrain; },

                Active = false
            });

            _buttons.Add("edit-spawnpoints-mode", new Button
            {
                Text = "",
                Icon = Assets.Get<Texture2D>("textures/editor/icon_spawnpoints.png"),
                X = new TweenValue(Game, -200),
                Y = new TweenValue(Game, 90 + 32),
                Font = Game.DefaultFonts.Normal,
                Clicked = (btn) => { _editMode = EditMode.Spawnpoints; },

                Active = false
            });

            _buttons.Add("edit-info-mode", new Button
            {
                Text = "",
                Icon = Assets.Get<Texture2D>("textures/editor/icon_info.png"),
                X = new TweenValue(Game, -200),
                Y = new TweenValue(Game, 90 + 32 * 2),
                Font = Game.DefaultFonts.Normal,
                Clicked = (btn) => { _editMode = EditMode.Info; },

                Active = false
            });

            _buttons["edit-terrain-mode"].X.TweenTo(10, TweenEaseType.EaseOutQuad, 0.65f);
            _buttons["edit-spawnpoints-mode"].X.TweenTo(10, TweenEaseType.EaseOutQuad, 0.65f);
            _buttons["edit-info-mode"].X.TweenTo(10, TweenEaseType.EaseOutQuad, 0.65f);

            _topTextY = new TweenValue(Game, -300);
            _topTextY.TweenTo(10, TweenEaseType.EaseOutQuad, 0.65f);

            _fade = new TweenValue(Game, 1.0);
            _fade.TweenTo(0, TweenEaseType.Linear, 0.5f);

            _topTextFade = new TweenValue(Game, 1.0);
        }

        public override void OnLeave(GameState nextState)
		{
		}

		public override void Update(double deltaTime, double totalTime, long count)
		{
            _buttons["edit-terrain-mode"].Active = _editMode == EditMode.Terrain;
            _buttons["edit-spawnpoints-mode"].Active = _editMode == EditMode.Spawnpoints;
            _buttons["edit-info-mode"].Active = _editMode == EditMode.Info;

            foreach(Button button in _buttons.Values)
                button.Update(Game);

            string str =
               "TBS Map Editor\n" +
               "Name: \"" + _mapData.Info.Name + "\" (made by \"" + _mapData.Info.Creator + "\")\n" +
               "Size: " + _mapData.Width + "x" + _mapData.Height;

            Vector2 measure = Game.DefaultFonts.Bold.Measure(15, str);
            Rectangle topTextRectangle = new Rectangle(10, (int)_topTextY.Value, (int)(measure.X + 20), (int)(measure.Y + 20));

            if(topTextRectangle.Contains(Input.MousePosition) && !topTextRectangle.Contains(Input.PreviousMousePosition))
                _topTextFade.TweenTo(0.6f, TweenEaseType.Linear, 0.05f);
            else if(!topTextRectangle.Contains(Input.MousePosition) && topTextRectangle.Contains(Input.PreviousMousePosition))
                _topTextFade.TweenTo(1.0f, TweenEaseType.Linear, 0.05f);
        }

        public override void FixedUpdate(long count)
		{
		}

		public override void Draw(SpriteBatch batch, Vector2 screenSize)
		{
			batch.Transform = Matrix.CreateScale(2);
			batch.SamplerState = SamplerState.PointClamp;

            _mapData.Draw(batch, Assets, new Rectangle(0, 0, (int)screenSize.X / 2, (int)screenSize.Y / 2));
			
			batch.Reset();
			
			string str = 
				"TBS Map Editor\n" + 
				"Name: \"" + _mapData.Info.Name + "\" (made by \"" + _mapData.Info.Creator + "\")\n" +
				"Size: " + _mapData.Width + "x" + _mapData.Height;

			Vector2 measure = Game.DefaultFonts.Bold.Measure(15, str);
			batch.Rectangle(new Rectangle(10, (int)_topTextY.Value, (int)(measure.X + 20), (int)(measure.Y + 20)), Color.Black * ((float)_topTextFade.Value * 0.85f));
			
			batch.Text(SpriteBatch.FontStyle.Bold, 15, "TBS Map Editor", new Vector2(20, (int)_topTextY.Value + 10), Color.White * (float)_topTextFade.Value);
			batch.Text(SpriteBatch.FontStyle.Bold, 15, str.Replace("TBS Map Editor\n", ""), new Vector2(20, (int)_topTextY.Value + 10 + (15 * 1.25f)), Color.White * ((float)_topTextFade.Value * 0.8f));

            foreach(Button button in _buttons.Values)
                button.Draw(Game);

            batch.Rectangle(new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y), Color.Black * (float)_fade.Value);
		}
	}
}