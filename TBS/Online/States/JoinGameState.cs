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
    public class JoinGameState : GameState
    {
        private OnlineData _onlineData;
        private NetConnectionStatus _status = NetConnectionStatus.None;
        
        public JoinGameState(OnlineData onlineData)
        {
            _onlineData = onlineData;
        }

        public override void OnEnter(GameState previousState)
        {
        }

        public override void OnLeave(GameState nextState)
        {
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            NetIncomingMessage message;
            while((message = _onlineData.Client.ReadMessage()) != null)
            {
                switch(message.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        _status = (NetConnectionStatus)message.ReadByte();
                        break;
                }
                
                _onlineData.Client.Recycle(message);
            }

            if(_status == NetConnectionStatus.Connected)
                Game.CurrentState = new LobbyState(_onlineData);
            if(_status == NetConnectionStatus.Disconnected)
                Game.CurrentState = new DisconnectedState(_onlineData);
            if(Input.Key(Keys.Escape))
            {
                _onlineData.Client.Shutdown("Disconnected");
                Game.CurrentState = new TestCreateOrJoinServerState();
            }
        }

        public override void FixedUpdate(long count)
        {
        }

        public override void Draw(SpriteBatch batch, Vector2 screenSize)
        {
            batch.Transform = Matrix.CreateScale(2);
            batch.SamplerState = SamplerState.PointClamp;

            Font font = Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf");

            string text = "Connecting to " + _onlineData.IP + ":" + _onlineData.Port + "...";

            float fade = 0.4f + ((float)(Math.Sin(Game.Time * 10) + 1) * 0.5f) * 0.4f;
            batch.Text(font, 8, text, screenSize / 4 - font.Measure(8, text) / 2, Color.White * fade);

            text = "...";

            switch(_status)
            {
                case NetConnectionStatus.InitiatedConnect:
                    text = "Initiated connect";
                    break;
                    
                case NetConnectionStatus.RespondedConnect:
                    text = "Responded to connect";
                    break;
                    
                case NetConnectionStatus.ReceivedInitiation:
                    text = "Recieved initiation";
                    break;
                    
                case NetConnectionStatus.RespondedAwaitingApproval:
                    text = "Awaiting approval";
                    break;
                    
                case NetConnectionStatus.Connected:
                    text = "Connected";
                    break;
                    
                case NetConnectionStatus.Disconnected:
                    text = "Disconnected";
                    break;
            }
                        
            batch.Text(font, 8, text, screenSize / 4 - font.Measure(8, text) / 2 + new Vector2(0, 10), Color.White * 0.2f);
        }
    }
}