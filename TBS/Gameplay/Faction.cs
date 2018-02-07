using Microsoft.Xna.Framework;

namespace TeamStor.TBS.Gameplay
{
	/// <summary>
	/// Player factions.
	/// </summary>
	public enum Faction
	{
		Soviet,
		ARLA,
		Cartel
	}

	/// <summary>
	/// Info about each faction.
	/// </summary>
	public static class FactionInfo
	{
		public static readonly string DescriptionSoviet = "BLABLABLA";
		public static readonly string DescriptionARLA = "BLABLABLA... They once shot down a helicopter with only rocks, spears and bows.";
		public static readonly string DescriptionCartel = "BLABLABLA... Kokain.";

		public static string DescriptionFromEnum(Faction faction)
		{
			switch(faction)
			{
				case Faction.Soviet:
					return DescriptionSoviet;
				case Faction.ARLA:
					return DescriptionARLA;
				case Faction.Cartel:
					return DescriptionCartel;
			}

			return "?";
		}
	}
}