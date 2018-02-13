using System;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.TBS.Map;
using TeamStor.TBS.Online;
using TeamStor.TBS.Online.States;

namespace TeamStor.TBS.Gameplay.States
{
    /// <summary>
    /// Game lobby.
    /// Players can only join the game while it's still in the lobby.
    /// </summary>
    public class LobbyState : GameState
    {
        private OnlineData _onlineData;
        private GameData _gameData;
        
        public LobbyState(OnlineData onlineData)
        {
            _onlineData = onlineData;
        }
        
        public LobbyState(OnlineData onlineData, MapData mapData, string name)
        {
            _onlineData = onlineData;
            _gameData = new GameData(mapData);
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
                {
                    Player player = _gameData.FindPlayerByNetConnection(message.SenderConnection);
                    
                    switch(message.MessageType)
                    {
                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)message.ReadByte();

                            if(status == NetConnectionStatus.Disconnected && player != null)
                            {
                                _gameData.Players.Remove(player.Id);
                                
                                NetOutgoingMessage sendMsg = _onlineData.Server.CreateMessage();
                                sendMsg.Write((byte)PacketType.PlayerDisconnected);
                                sendMsg.Write(player.Id);
                                
                                _onlineData.Server.SendToAll(sendMsg, NetDeliveryMethod.ReliableUnordered);
                            }
                            break;
                    }
                
                    _onlineData.Server.Recycle(message);
                }
            }
            
            while((message = _onlineData.Client.ReadMessage()) != null)
            {
                switch(message.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)message.ReadByte();

                        if(status == NetConnectionStatus.Disconnected)
                            Game.CurrentState = new DisconnectedState(_onlineData);
                        break;
                }
                
                _onlineData.Client.Recycle(message);
            }
        }

        public override void FixedUpdate(long count)
        {
        }

        public override void Draw(SpriteBatch batch, Vector2 screenSize)
        {
        }
    }
}