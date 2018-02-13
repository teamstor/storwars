using System;
using TeamStor.Engine;
using TeamStor.TBS.Online.States;

namespace TeamStor.TBS
{
	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			using(Game game = Game.Run(new TestCreateOrJoinServerState(), "data", false))
				game.Run();
		}
	}
}