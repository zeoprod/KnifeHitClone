using System;
using System.Collections.Generic;
using UnityEngine;
using LogType = Core.Enums.LogType;

namespace ScriptableObjects.Classes.Logs
{
	[CreateAssetMenu(
		menuName = "Configs/Logs/Create AllLogsConfigs", 
		fileName = "AllLogsConfigs", 
		order = 0)
	]
	public class AllLogsConfigs : ScriptableObject
	{
		[SerializeField] private List<LogConfig> _configs;
		
		private Dictionary<LogType, LogConfig> _cachedConfigs;

		public LogConfig GetConfig(LogType type)
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