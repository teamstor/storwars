using Microsoft.Xna.Framework;

namespace TeamStor.TBS.Gameplay
{
	/// <summary>
	/// Player team.
	/// </summary>
	public enum Team
	{
		Red,
		Blue,
		Green,
		Yellow
	}

	/// <summary>
	/// Colors for each player team.
	/// </summary>
	public static class TeamColors
	{
		public static readonly Color Red = new Color(145, 38, 38);
		public static readonly Color Blue = new Color(40, 42, 143);
		public static readonly Color Green = new Color(27, 137, 41);
		public static readonly Color Yellow = new Color(197, 169, 5);

		public static Color FromEnum(Team team)
		{
			switch(team)
			{
				case Team.Red:
					return Red;
					
				case Team.Blue:
					return Blue;
					
				case Team.Green:
					return Green;
					
				case Team.Yellow:
					return Yellow;
			}

			return Color.White;
		}
	}
}