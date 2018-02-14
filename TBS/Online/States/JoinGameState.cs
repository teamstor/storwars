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
        private bool _showText;
        
        public JoinGameState(OnlineData onlineData, bool showText = true)
        {
            _onlineData = onlineData;
            _showText = showText;
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

            if(_onlineData.IsHost)
            {
                while((message = _onlineData.Server.ReadMessage()) != null)
                    _onlineData.Server.Recycle(message);
            }

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
                Game.CurrentState = new LobbyState(_onlineData, TestCreateOrJoinServerState.Name);
            if(_status == NetConnectionStatus.Disconnected)
                Game.CurrentState = new DisconnectedState(_onlineData, "Couldn't connect to server");
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

            Font font = Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf", false);

            string text = "Connecting to " + _onlineData.IP + ":" + _onlineData.Port + "...";

            if(_showText)
                batch.Text(font, 8, text, screenSize / 4 - font.Measure(8, text) / 2, Color.White);

            text = "........................";

            for(int i = 0; i < 24; i++)
            {
                float fade = 0.6f * (float)(Math.Sin((Game.Time - i * 0.15f) * 10) + 1) * 0.5f;
                fade = (int)(fade * 10) / 10f;
                
                batch.Text(font, 8, ".", screenSize / 4 - font.Measure(8, text) / 2 + new Vector2(8 * i, _showText ? 10 : 0),
                    Color.Lerp(Color.White * (0.2f + fade), Color.RoyalBlue * (0.2f + fade), fade));
            }
        }
    }
}