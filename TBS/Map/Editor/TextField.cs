﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.Engine.Tween;
using Game = TeamStor.Engine.Game;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Map.Editor
{
	public class TextField
	{
		public string Label, Text;
		public Texture2D Icon;
		public TweenedVector2 Position;
		public int Width;
		public Font Font;
		public bool Focused;
		public Color TextColor = Color.White;

		public delegate void OnTextChanged(TextField field, string newText);
		public OnTextChanged TextChanged;
		
		public delegate void OnFocusChanged(TextField field, bool focus);
		public OnFocusChanged FocusChanged;

		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle((int)Position.Value.X, (int)Position.Value.Y, Width, 32);
			}
		}

		private bool _first = true;

		public void Update(Game game)
		{
			if(_first)
			{
				game.Window.TextInput += OnTextInput;
				game.OnStateChange += OnStateChange;
				_first = false;
			}

			if(game.Input.MousePressed(MouseButton.Left))
			{
				bool oldFocused = Focused;
				Focused = Rectangle.Contains(game.Input.MousePosition);
				
				if(Focused != oldFocused && FocusChanged != null)
					FocusChanged(this, Focused);
			}
		}

		private void OnStateChange(object sender, Game.ChangeStateEventArgs e)
		{
			((Game)sender).Window.TextInput -= OnTextInput;
			((Game)sender).OnStateChange -= OnStateChange;
		}

		private void OnTextInput(object sender, TextInputEventArgs e)
		{
			if(Focused)
			{
				string oldText = Text;
				
				if(e.Character == '\b' && Text.Length > 0)
					Text = Text.Substring(0, Text.Length - 1);
				else if(Char.IsLetterOrDigit(e.Character) || Char.IsPunctuation(e.Character) || e.Character == ' ')
					Text += e.Character;

				Text = Text.TrimStart();

				if(e.Key == Keys.Enter)
				{
					Focused = false;
					if(FocusChanged != null)
						FocusChanged(this, false);
				}
					
				if(Text.Length > 30)
					Text = Text.Substring(0, 30);

				if(Text != oldText && TextChanged != null)
					TextChanged(this, Text);
			}
		}

		public void Draw(Game game)
		{
			bool hovered = Rectangle.Contains(game.Input.MousePosition);
			SpriteBatch batch = game.Batch;

			batch.Rectangle(Rectangle, Color.Black * 0.8f);

			if(Icon != null)
				batch.Texture(new Vector2(Position.Value.X + 4, Position.Value.Y + 4), Icon, TextColor * (Focused ? 1.0f : hovered ? 0.8f : 0.6f));

			batch.Text(Font, 15, 
				Label + Text + (Focused && (int)((game.Time * 4) % 2) == 0 ? "|" : ""), 
				new Vector2(Position.Value.X + (Icon != null ? Icon.Width + 8 : 8), Position.Value.Y + 6), 
				TextColor * (Focused ? 1.0f : hovered ? 0.8f : 0.6f));
		}
	}
}