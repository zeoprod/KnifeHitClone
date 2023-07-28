using Core.Enums;
using Gameplay.LevelElements;
using UnityEngine;

namespace ScriptableObjects.Classes
{
	[CreateAssetMenu(
		menuName = "Configs/Obstacles/Create ObstacleConfig", 
		fileName = "ObstacleConfig", 
		order = 0)
	]
	public class ObstacleConfig : ScriptableObject
	{
		[SerializeField] private ObstacleType _type;
		[SerializeField] private Obstacle     _obstaclePrefab;

		public ObstacleType Type => _type;
		public Obstacle ObstaclePrefab => _obstaclePrefab;
	}
}