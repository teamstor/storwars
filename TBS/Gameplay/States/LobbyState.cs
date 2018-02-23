using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;
using TeamStor.TBS.Map;
using TeamStor.TBS.Online;
using TeamStor.TBS.Online.States;
using SpriteBatch = TeamStor.Engine.Graphics.SpriteBatch;

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

        private int _currentId;

        private void SendInitialPacket(string name)
        {
            NetOutgoingMessage message = _onlineData.Client.CreateMessage();
            message.Write((byte)PacketType.PlayerConnected);
            message.Write(name);

            _onlineData.Client.SendMessage(message, NetDeliveryMethod.ReliableUnordered);
        }
        
        public LobbyState(OnlineData onlineData, string name)
        {
            _onlineData = onlineData;
            // Replaced when we get info from server
            _gameData = new GameData(new MapData(new MapInfo { Creator = "...", Name = "..." }, 1, 1));
                        
            SendInitialPacket(name);
        }

        public override void OnEnter(GameState previousState)
        {
        }

        public override void OnLeave(GameState nextState)
        {
            if(nextState == null)
            {
                _onlineData.Client.Shutdown("Shutdown");
                if(_onlineData.IsHost)
                    _onlineData.Server.Shutdown("Shutdown");
            }
        }

        public override void Update(double deltaTime, double totalTime, long count)
        {
            if(_onlineData.IsHost)
            {
                int i = 0;
                foreach(Player p in _gameData.Players.Values)
                {
                    if(Input.Key((Microsoft.Xna.Framework.Input.Keys) ((int)Keys.D1 + i)))
                        p.ConnectionToServer.Disconnect("Kicked by host");    
                    i++;
                }
            }

            NetIncomingMessage message;
            
            if(_onlineData.IsHost)
            {
                while((message = _onlineData.Server.ReadMessage()) != null)
                {
                    Player player = _gameData.FindPlayerByNetConnection(message.SenderConnection);
                    NetOutgoingMessage sendMsg;
                    
                    switch(message.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            PacketType type = (PacketType)message.ReadByte();
                            
                            switch(type)
                            {
                                case PacketType.PlayerConnected:
                                    Player newPlayer = new Player();
                                    newPlayer.Name = message.ReadString();
                                    newPlayer.Id = _currentId++;
                                    newPlayer.ConnectionToServer = message.SenderConnection;
                                    _gameData.Players.Add(newPlayer.Id, newPlayer);
                                    
                                    // send to other players
                                    sendMsg = _onlineData.Server.CreateMessage();
                                    sendMsg.Write((byte)PacketType.PlayerConnected);
                                    sendMsg.Write(newPlayer.Name);
                                    sendMsg.Write(newPlayer.Id);
                                    sendMsg.Write((byte)newPlayer.Team);
                                    sendMsg.Write(false /* local player */);
                                
                                    _onlineData.Server.SendToAll(sendMsg, message.SenderConnection, NetDeliveryMethod.ReliableUnordered, 0);

                                    foreach(Player p in _gameData.Players.Values)
                                    {
                                        sendMsg = _onlineData.Server.CreateMessage();
                                        sendMsg.Write((byte)PacketType.PlayerConnected);
                                        sendMsg.Write(p.Name);
                                        sendMsg.Write(p.Id);
                                        sendMsg.Write((byte)p.Team);
                                        sendMsg.Write(message.SenderConnection == newPlayer.ConnectionToServer);
                                        
                                        _onlineData.Server.SendMessage(sendMsg, message.SenderConnection, NetDeliveryMethod.ReliableUnordered);
                                    }
                                    
                                    sendMsg = _onlineData.Server.CreateMessage();
                                    sendMsg.Write((byte)PacketType.LoadMap);
                                    sendMsg.Write("test");
                                        
                                    _onlineData.Server.SendMessage(sendMsg, message.SenderConnection, NetDeliveryMethod.ReliableUnordered);
                                    break;
                                
                                default:
                                    message.SenderConnection.Disconnect("Unknown packet");

                                    if(player != null)
                                    {
                                        sendMsg = _onlineData.Server.CreateMessage();
                                        sendMsg.Write((byte)PacketType.PlayerDisconnected);
                                        sendMsg.Write(player.Id);
                                
                                        _onlineData.Server.SendToAll(sendMsg, NetDeliveryMethod.ReliableUnordered);
                                    }
                                    break;
                            }

                            break;
                        case NetIncomingMessageType.StatusChanged:
                            NetConnectionStatus status = (NetConnectionStatus)message.ReadByte();

                            if(status == NetConnectionStatus.Disconnected && player != null)
                            {                           
                                sendMsg = _onlineData.Server.CreateMessage();
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
                    case NetIncomingMessageType.Data:
                        PacketType type = (PacketType)message.ReadByte();
                            
                        switch(type)
                        {
                            case PacketType.PlayerConnected:
                                string name = message.ReadString();
                                int id = message.ReadInt32();
                                Team team = (Team)message.ReadByte();
                                bool local = message.ReadBoolean();

                                Player p;
                                if(!_gameData.Players.TryGetValue(id, out p))
                                {
                                    p = new Player();
                                    p.Name = name;
                                    p.Id = id;
                                    p.Team = team;
                                    
                                    _gameData.Players.Add(p.Id, p);
                                }

                                if(local)
                                    _gameData.LocalPlayer = p;
                                break;
                            
                            case PacketType.PlayerDisconnected:
                                try
                                {
                                    _gameData.Players.Remove(message.ReadInt32());
                                }
                                catch
                                {
                                }
                                break;
                            
                            case PacketType.LoadMap:
                                string map = message.ReadString();
                                
                                if(File.Exists("data/maps/" + map + ".tsmap"))
                                    _gameData.Map = MapData.Load("data/maps/" + map + ".tsmap");
                                else
                                    _onlineData.Client.Disconnect("Couldn't find map \"data/maps/" + map + ".tsmap\"");
                                
                                break;
                            
                            case PacketType.StartGameplay:
                                Game.CurrentState = new GameplayState();
                                break;
                                
                            default:
                                _onlineData.Client.Disconnect("Got unknown packet");
                                break;
                        }

                        break;
                    
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)message.ReadByte();

                        if(status == NetConnectionStatus.Disconnected)
                        {
                            try
                            {
                                Game.CurrentState =
                                    new DisconnectedState(_onlineData, message.ReadString());
                            }
                            catch
                            {
                                Game.CurrentState =
                                    new DisconnectedState(_onlineData, "Unknown reason, disconnected from server");
                            }
                        }

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
            batch.SamplerState = SamplerState.PointClamp;
            batch.Transform = Matrix.CreateScale(2);
            
            Font font = Assets.Get<Font>("fonts/PxPlus_IBM_BIOS.ttf", false);
            
            string text = "ENTER to enter game\nIs Host: " + _onlineData.IsHost + "\nMap: " + _gameData.Map.Info.Name + " by " + _gameData.Map.Info.Creator + "\nPlayers: " + _gameData.Players.Count;

            int i = 0;
            foreach(Player p in _gameData.Players.Values)
            {
                text += "\nID " + p.Id + ", Name " + p.Name + ", Local " + (_gameData.LocalPlayer == p);

                if(_onlineData.IsHost)
                    text += " (press " + (i + 1) + " to kick)";
                i++;
            }

            Vector2 measure = font.Measure(8, text);
            batch.Text(font, 8, text, screenSize / 4 - measure / 2, Color.White);
        }
    }
}