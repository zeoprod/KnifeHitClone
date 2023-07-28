using Core.Enums;
using Gameplay.LevelElements;
using UnityEngine;

namespace ScriptableObjects.Classes
{
	[CreateAssetMenu(
		menuName = "Configs/Knives/Create KnifeConfig", 
		fileName = "KnifeConfig", 
		order = 0)
	]
	public class KnifeConfig : ScriptableObject
	{
		[SerializeField] private KnifeType _type;
		[SerializeField] private Knife     _knifePrefab;

		[SerializeField] private float _throwForce;

		public KnifeType Type => _type;
		public Knife KnifePrefab => _knifePrefab;
		public float ThrowForce => _throwForce;
	}
}