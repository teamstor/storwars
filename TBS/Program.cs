using TeamStor.Engine;
using TeamStor.TBS.Map.Editor;

namespace TeamStor.TBS
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using(Game game = Game.Run(new MapEditorState()))
				game.Run();
		}
	}
}