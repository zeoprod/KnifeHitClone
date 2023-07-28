using Core.Enums;
using Gameplay.LevelElements;
using ScriptableObjects.Classes;
using UnityEngine;
using Utils.ObjectPool;

namespace Gameplay.Factories
{
	public class ObstaclesFactory
	{
		private readonly AllObstaclesConfig _allConfigs;

		public ObstaclesFactory(AllObstaclesConfig allConfigs)
		{
			_allConfigs = allConfigs;
		}
		
		public Obstacle Create(ObstacleType type, Vector3 position, Quaternion rotation)
		{
			var config = _allConfigs.GetConfig(type);
			var knife  = Pool.Get(config.ObstaclePrefab, position, rotation);

			return knife;
		}
	}
}