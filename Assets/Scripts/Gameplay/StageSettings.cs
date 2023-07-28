using System;
using UnityEngine;
using LogType = Core.Enums.LogType;

namespace Gameplay
{
	[Serializable]
	public class StageSettings
	{
		[SerializeField] private int                _neededHitsAmount;
		[SerializeField] private AnimationCurve     _logRotationSpeedCurve;
		[SerializeField] private LogType            _logType;
		[SerializeField] private ObstacleSettings[] _obstacles;

		public int NeededHitsAmount => _neededHitsAmount;
		public AnimationCurve LogRotationSpeedCurve => _logRotationSpeedCurve;
		public LogType LogType => _logType;
		public ObstacleSettings[] Obstacles => _obstacles;
	}
}