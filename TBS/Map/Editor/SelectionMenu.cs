using System.Collections.Generic;
using TeamStor.Engine;

namespace TeamStor.TBS.Map.Editor
{
	public class SelectionMenu
	{
		public string Title;
		public List<string> Entries;

		public int Selected = 0;
		
		public delegate void OnSelectionChanged(SelectionMenu menu, int newSelected);
		public OnSelectionChanged SelectionChanged;
		
		public void Update(Game game)
		{
		}

		public void Draw(Game game)
		{
		}
	}
}