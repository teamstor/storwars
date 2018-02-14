using System;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.TBS.Gameplay.States;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

namespace TeamStor.TBS.Online.States
{
    public class DisconnectedState : GameState
    {
        private string _reason; 
        
        public DisconnectedState(OnlineData onlineData, string reason)
        {
            onlineData.Client.Shutdown("Disconnected");
            
            if(onlineData.IsHost)
                onlineData.Server.Shutdown("Disconnected");

            _reason = reason;
        }

        public override void OnEnter(GameState previousState)
        {
        }

        public override void OnLeave(GameState nextState)
        {
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            if(Input.Key(Keys.Escape))
                Game.CurrentState = new TestCreateOrJoinServerState();
        }

        public override void FixedUpdate(long count)
        {
        }

        public override void Draw(SpriteBatch batch, Vector2 screenSize)
        {
            batch.Transform = Matrix.CreateScale(2);
            batch.SamplerState = SamplerState.PointClamp;

            Font font = Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf", false);

            string text = "ERROR: disconnected from server\n" + _reason + "\nPress ESC to return to main menu";
            Vector2 measure = font.Measure(8, text);

            batch.Text(font, 8, text, screenSize / 4 - measure / 2, Color.IndianRed);
        }
    }
}