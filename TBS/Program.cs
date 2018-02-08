using System;
using TeamStor.Engine;
using TeamStor.TBS.Map.Editor;
using TeamStor.TBS.Menu;

namespace TeamStor.TBS
{
	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			using(Game game = Game.Run(new MainMenuState(), "data", false))
				game.Run();
		}
	}
}