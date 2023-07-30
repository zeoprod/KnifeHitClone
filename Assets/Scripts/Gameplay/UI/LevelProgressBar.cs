using System;
using System.Collections.Generic;
using ScriptableObjects.Classes;
using UnityEngine;
using Utils;

namespace Gameplay.UI
{
	public class LevelProgressBar : MonoBehaviour
	{
		[SerializeField] private Transform                  _partsContainer;
		[SerializeField] private LevelProgressBarPart       _leftPartPrefab;
		[SerializeField] private LevelProgressBarPart       _midPartPrefab;
		[SerializeField] private LevelProgressBarPart       _rightPartPrefab;

		private readonly List<LevelProgressBarPart> _parts = new();
	
		public void Initialize(LevelConfig levelConfig)
		{
			DestroyOldParts();
			CreateParts(levelConfig);
		}

		private void CreateParts(LevelConfig levelConfig)
		{
			var stagesCount = levelConfig.StagesCount;

			for (int i = 0; i < stagesCount; i++)
			{
				LevelProgressBarPart currentPartPrefab;

				if (i == 0)
					currentPartPrefab = _leftPartPrefab;
				else if (i == stagesCount - 1)
					currentPartPrefab = _rightPartPrefab;
				else
					currentPartPrefab = _midPartPrefab;

				if (currentPartPrefab == null)
				{
					throw new Exception("PartPrefab not found!");
				}
				
				Instantiate(currentPartPrefab, _partsContainer)
					.With(p => _parts.Add(p));
			}
		}

		private void DestroyOldParts()
		{
			if (_parts.Count > 0)
			{
				for (var i = 0; i < _parts.Count; i++)
				{
					Destroy(_parts[i].gameObject);
				}

				_parts.Clear();
			}
		}

		public void SetCurrent(int current)
		{
			for (int i = 0; i < _parts.Count; i++)
			{
				if (i < current)
				{
					_parts[i].SetAsCompleted();
				}
				else if (i == current)
				{
					_parts[i].SetAsCurrent();
				}
				else
				{
					_parts[i].SetAsNotCompleted();
				}
			}
		}
	}
}