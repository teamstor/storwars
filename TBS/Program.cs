using System;
using TeamStor.Engine;
using TeamStor.TBS.Gameplay.States;
using TeamStor.TBS.Map.Editor;
using TeamStor.TBS.Menu;
using TeamStor.TBS.Online.States;

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
