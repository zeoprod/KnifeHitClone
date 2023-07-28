using ScriptableObjects.Classes;

namespace Gameplay.Factories
{
	public class LevelConfigsFactory
	{
		private readonly AllLevelsConfig _allConfigs;

		public LevelConfigsFactory(AllLevelsConfig allConfigs)
		{
			_allConfigs = allConfigs;
		}

		public int Count => _allConfigs.Count;
		
		public LevelConfig Get(int index)
		{
			var config = _allConfigs.GetConfig(index);
			
			return config;
		}
	}
}