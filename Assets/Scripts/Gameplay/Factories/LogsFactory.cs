using Gameplay.LevelElements;
using ScriptableObjects.Classes.Logs;
using UnityEngine;
using Utils.ObjectPool;

namespace Gameplay.Factories
{
	public class LogsFactory
	{
		private readonly AllLogsConfigs _allConfigs;

		public LogsFactory(AllLogsConfigs allConfigs)
		{
			_allConfigs = allConfigs;
		}
		
		public Log Create(Core.Enums.LogType type, Vector3 position, Quaternion rotation = default)
		{
			var config = _allConfigs.GetConfig(type);
			var log  = Pool.Get(config.LogPrefab, position, rotation);

			return log;
		}
	}
}