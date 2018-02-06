using System.Collections.Generic;
using TeamStor.Engine;
using TeamStor.TBS.Map;

namespace TeamStor.TBS.Gameplay
{
	/// <summary>
	/// Game data.
	/// </summary>
	public class GameData
	{
		/// <summary>
		/// All players in this game.
		/// </summary>
		public Dictionary<int, Player> Players { get; private set; } = new Dictionary<int, Player>();

		/// <summary>
		/// The local player.
		/// </summary>
		public Player LocalPlayer;

		/// <summary>
		/// The map data.
		/// </summary>
		public MapData Map;
		
		public GameData(MapData map)
		{
			Map = map;
		}
	}
}