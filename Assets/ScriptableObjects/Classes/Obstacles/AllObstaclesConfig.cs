using System;
using System.Collections.Generic;
using Core.Enums;
using UnityEngine;

namespace ScriptableObjects.Classes
{
	[CreateAssetMenu(
		menuName = "Configs/Obstacles/Create AllObstaclesConfig", 
		fileName = "AllObstaclesConfig", 
		order = 0)
	]
	public class AllObstaclesConfig : ScriptableObject
	{
		[SerializeField] private List<ObstacleConfig> _configs;
		
		private Dictionary<ObstacleType, ObstacleConfig> _cachedConfigs;

		public ObstacleConfig GetConfig(ObstacleType type)
		{
			if (_cachedConfigs == null)
			{
				Cache();
			}

			if (!_cachedConfigs.ContainsKey(type))
			{
				throw new Exception($"This {type} is not found!");
			}
			
			return _cachedConfigs[type];
		}

		private void Cache()
		{
			_cachedConfigs = new();

			foreach (var config in _configs)
			{
				if (_cachedConfigs.ContainsKey(config.Type))
				{
					throw new Exception($"This {config.Type} is contains in list!");
				}
				
				_cachedConfigs.Add(config.Type, config);
			}
		}
	}
}