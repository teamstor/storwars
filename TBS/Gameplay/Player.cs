using Lidgren.Network;

namespace TeamStor.TBS.Gameplay
{
	public class Player
	{
		/// <summary>
		/// Name of the player.
		/// </summary>
		public string Name;

		/// <summary>
		/// ID of this player.
		/// </summary>
		public int Id;

		/// <summary>
		/// Selected team.
		/// </summary>
		public Team Team;

		/// <summary>
		/// The connection this player has to the server.
		/// This is null if you're not host.
		/// </summary>
		public NetConnection ConnectionToServer;
	}
}