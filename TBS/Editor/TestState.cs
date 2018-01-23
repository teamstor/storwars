using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS
{
	public class TestState : GameState
	{		
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
			batch.Transform = Matrix.CreateScale(2);
			batch.SamplerState = SamplerState.PointClamp;
			
			string[] list = 
			{
				"textures/barrack_soviet.png",
				"textures/blacksmith_soviet.png",
				"textures/hq_arla.png",
				"textures/hq_cartel.png",
				"textures/hq_soviet.png",
				"textures/mill_soviet.png",
				"textures/mine_soviet.png"
			};

			string selected = list[(Game.TotalFixedUpdates / 40) % list.Length];
			
			batch.Texture(Input.MousePosition / 2, Assets.Get<Texture2D>(selected), Color.White);
			batch.Text(Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf"), 8, selected.Replace("textures/", "").Replace(".png", ""), 
				Input.MousePosition / 2 + new Vector2(10, 40), Color.White);
		}
	}
}