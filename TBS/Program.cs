using System;
using TeamStor.Engine;
using TeamStor.TBS.Map.Editor;

namespace TeamStor.TBS
{
	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			using(Game game = Game.Run(new Menu.MainMenuState(), "data", false))
				game.Run();
		}
	}
}