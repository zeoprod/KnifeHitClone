using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Classes
{
	[CreateAssetMenu(
		menuName = "Configs/Levels/Create AllLevelsConfig", 
		fileName = "AllLevelsConfig", 
		order = 0)
	]
	public class AllLevelsConfig : ScriptableObject
	{
		[SerializeField] private List<LevelConfig> _configs;

		public int Count => _configs.Count;

		public LevelConfig GetConfig(int index)
		{
			return _configs[index];
		}
	}
}