using Gameplay;
using UnityEngine;

namespace ScriptableObjects.Classes
{
	[CreateAssetMenu(
		menuName = "Configs/Levels/Create LevelConfig",
		fileName = "LevelConfig",
		order = 0)
	]
	public class LevelConfig : ScriptableObject
	{
		[SerializeField] private StageSettings[] _stages;

		public int StagesCount => _stages.Length;

		public StageSettings GetStageConfig(int stageIndex)
		{
			if (stageIndex >= 0 && stageIndex < _stages.Length)
			{
				return _stages[stageIndex];
			}

			return null;
		}
	}
}