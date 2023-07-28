using Core.Enums;
using Gameplay.LevelElements;
using ScriptableObjects.Classes;
using UnityEngine;
using Utils.ObjectPool;

namespace Gameplay.Factories
{
	public class KnivesFactory
	{
		private readonly AllKnivesConfig _allConfigs;

		public KnivesFactory(AllKnivesConfig allConfigs)
		{
			_allConfigs = allConfigs;
		}
		
		public Knife CreateKnife(KnifeType type, Vector3 position, Quaternion rotation)
		{
			var config = _allConfigs.GetConfig(type);
			var knife  = Pool.Get(config.KnifePrefab, position, rotation);

			knife.Init(config);
			
			return knife;
		}
	}
}