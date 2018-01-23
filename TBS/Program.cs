using TeamStor.Engine;

namespace TeamStor.TBS
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using(Game game = Game.Run(new TestState(), "data", false))
				game.Run();
		}
	}
}