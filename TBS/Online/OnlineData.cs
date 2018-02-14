using System.Net;
using System.Net.Sockets;
using Lidgren.Network;
using SharpFont;

namespace TeamStor.TBS.Online
{
    /// <summary>
    /// Data about server and other online things.
    /// </summary>
    public class OnlineData
    {
        private OnlineData() { }

        /// <summary>
        /// Name of the server.
        /// </summary>
        public string ServerName { get; private set; } = "";

        /// <summary>
        /// IP of the server.
        /// </summary>
        public IPAddress IP { get; private set; }

        /// <summary>
        /// Port of the server.
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Network client. Connected to the server.
        /// </summary>
        public NetClient Client { get; private set; }
        
        /// <summary>
        /// Network server. This is null if IsHost is false.
        /// </summary>
        public NetServer Server { get; private set; }

        /// <summary>
        /// If you are the host of this game.
        /// </summary>
        public bool IsHost
        {
            get { return Server != null; }
        }

        /// <summary>
        /// Starts a server and creates online data from it.
        /// </summary>
        /// <returns>Online data with a <code>NetServer</code> hosting the game.</returns>
        public static OnlineData StartServer(string name, int port = 9210)
        {            
            OnlineData onlineData = new OnlineData();

            NetPeerConfiguration serverConfig =
                new NetPeerConfiguration("team-stor-tbs " + Version.VERSION_NAME);
            serverConfig.EnableUPnP = true;
            serverConfig.Port = port;
            
            onlineData.Server = new NetServer(serverConfig);
            onlineData.Server.Start();
            // TODO: onlineData.Server.UPnP.ForwardPort(port, "team-stor-tbs");
            
            onlineData.Client = new NetClient(new NetPeerConfiguration("team-stor-tbs " + Version.VERSION_NAME));
            onlineData.Client.Start();
            onlineData.Client.Connect(new IPEndPoint(IPAddress.Loopback, port));
            
            onlineData.IP = IPAddress.Loopback;
            onlineData.Port = port;

            return onlineData;
        }
        
        /// <summary>
        /// Starts a client and connects to a server.
        /// </summary>
        /// <returns>Online data with a <code>NetServer</code> connecting to the game.</returns>
        public static OnlineData StartConnection(IPEndPoint ip)
        {
            NetPeerConfiguration config = new NetPeerConfiguration("team-stor-tbs " + Version.VERSION_NAME);
            
            OnlineData onlineData = new OnlineData();
                        
            onlineData.Client = new NetClient(config);
            onlineData.Client.Start();
            onlineData.Client.Connect(ip);
            
            onlineData.IP = ip.Address;
            onlineData.Port = ip.Port;

            return onlineData;
        }
    }
}