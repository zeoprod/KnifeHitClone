using Gameplay.LevelElements;
using UnityEngine;
using LogType = Core.Enums.LogType;


namespace ScriptableObjects.Classes.Logs
{
	[CreateAssetMenu(
		menuName = "Configs/Logs/Create LogConfig", 
		fileName = "LogConfig", 
		order = 0)
	]
	public class LogConfig : ScriptableObject
	{
		[SerializeField] private LogType _type;
		[SerializeField] private Log     _logPrefab;

		public LogType Type => _type;
		public Log LogPrefab => _logPrefab;
	}
}