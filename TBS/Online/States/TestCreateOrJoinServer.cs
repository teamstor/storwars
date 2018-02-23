using System;
using System.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.TBS.Gameplay.States;
using TeamStor.TBS.Map;
using TeamStor.TBS.Menu;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Online.States
{
    /// <summary>
    /// Bara för att testa
    /// </summary>
    public class TestCreateOrJoinServerState : GameState
    {
        public static string Name = new Random().Next().ToString();
        
        public override void OnEnter(GameState previousState)
        {
        }

        public override void OnLeave(GameState nextState)
        {
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            if(Input.Key(Keys.D1))
                Game.CurrentState = new JoinGameState(OnlineData.StartConnection(new IPEndPoint(IPAddress.Loopback, 9210)));
            if(Input.Key(Keys.D2))
                Game.CurrentState = new JoinGameState(OnlineData.StartServer("Test server"), false);
            if(Input.KeyPressed(Keys.Escape))
                Game.CurrentState = new MainMenuState();
        }

        public override void FixedUpdate(long count)
        {
        }

        public override void Draw(SpriteBatch batch, Vector2 screenSize)
        {
            batch.Transform = Matrix.CreateScale(2);
            batch.SamplerState = SamplerState.PointClamp;

            Font font = Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf", false);

            Vector2 measure =
                font.Measure(8, "1 - Join Server\n2 - Host Server\n\n(name: " + Name + ")");
            
            batch.Text(font, 8, "1 - Join Server\n2 - Host Server\n\n(name: " + Name + ")", screenSize / 4 - measure / 2, Color.White);
        }
    }
}