using System;
using Core.Enums;
using UnityEngine;

namespace Gameplay
{
	[Serializable]
	public class ObstacleSettings
	{
		[SerializeField]              private ObstacleType _type;
		[SerializeField, Range(0, 1)] private float        _spawnPointOnCircle;

		public ObstacleType Type => _type;
		public float SpawnPointOnCircle => _spawnPointOnCircle;
	}
}