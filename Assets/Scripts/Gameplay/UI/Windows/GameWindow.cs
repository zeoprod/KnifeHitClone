using UnityEngine;

namespace Gameplay.UI.Windows
{
	public class GameWindow : BaseWindow
	{
		[SerializeField] private LevelProgressBar _levelProgressBar;
		
		public LevelProgressBar LevelProgressBar => _levelProgressBar;
	}
}
