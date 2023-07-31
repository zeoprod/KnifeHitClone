using UnityEngine;

namespace ScriptableObjects.Classes.Gameplay
{
	[CreateAssetMenu(menuName = "Configs/Create GameConfig", fileName = "GameConfig", order = 0)]
	public class GameConfig : ScriptableObject
	{
		[SerializeField, Min(0)] private int _millisecondsDelayBetweenStages;

		public int MillisecondsDelayBetweenStages => _millisecondsDelayBetweenStages;
	}
}