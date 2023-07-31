using System.Collections.Generic;
using Gameplay.Factories;
using UnityEngine;
using Utils;
using Zenject;

namespace Gameplay.LevelElements
{
	public class StageGenerator : MonoBehaviour
	{
		[SerializeField] private Transform _logSpawnPoint;

		private          Log            _log;
		private readonly List<Obstacle> _obstacles = new();

		private StageSettings    _stageSettings;
		private ObstaclesFactory _obstaclesFactory;
		private LogsFactory      _logsFactory;

		[Inject]
		private void Construct(LogsFactory logsFactory, ObstaclesFactory obstaclesFactory)
		{
			_logsFactory = logsFactory;
			_obstaclesFactory = obstaclesFactory;
		}

		public void Generate(StageSettings stageSettings)
		{
			_stageSettings = stageSettings;

			ClearOldStage();
			GenerateObjects(_stageSettings);

			_log.Initialize(stageSettings);
			_log.StartRotation();
		}

		public void StageComplete()
		{
			_log.StopRotation();
			_log.Throw();
		}

		private void ClearOldStage()
		{
			for (int i = 0; i < _obstacles.Count; i++)
			{
				_obstacles[i].Release();
			}

			_obstacles.Clear();

			if (_log != null)
			{
				_log.StopRotation();
				_log.Release();
			}
		}

		private void GenerateObjects(StageSettings stageSettings)
		{
			_log = _logsFactory.Create(stageSettings.LogType, _logSpawnPoint.position);

			var logRadius     = _log.Radius;
			var obstacleCount = stageSettings.Obstacles.Length;

			for (int i = 0; i < obstacleCount; i++)
			{
				var spawnAngleRadians = (360f * stageSettings.Obstacles[i].SpawnPointOnCircle - 90f) * Mathf.Deg2Rad;
				var spawnPosition = new Vector2(_logSpawnPoint.position.x + Mathf.Cos(spawnAngleRadians) * logRadius,
																				_logSpawnPoint.position.y + Mathf.Sin(spawnAngleRadians) * logRadius);

				_obstaclesFactory.Create(stageSettings.Obstacles[i].Type, spawnPosition, Quaternion.identity)
												 .With(o => _obstacles.Add(o))
												 .With(o => o.transform.SetParent(_log.Transform))
												 .With(o => o.transform.Rotate(Vector3.forward * spawnAngleRadians * Mathf.Rad2Deg));
			}
		}
	}
}