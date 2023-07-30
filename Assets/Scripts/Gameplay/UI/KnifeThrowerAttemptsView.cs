using System.Collections.Generic;
using Gameplay.LevelElements;
using UnityEngine;
using Utils;
using Zenject;

namespace Gameplay.UI
{
	public class KnifeThrowerAttemptsView : MonoBehaviour
	{
		[SerializeField] private KnifeThrowingAttemptView _attemptViewPrefab;
		[SerializeField] private Transform                _viewsContainer;

		private List<KnifeThrowingAttemptView> _attemptsViews = new();

		private KnifeThrower _knifeThrower;

		private int _totalHitAttemptsAmount;

		[Inject]
		public void Construct(KnifeThrower knifeThrower)
		{
			_knifeThrower = knifeThrower;
			
			_knifeThrower.OnAttemptsAmountInitialized += HandleNewInitialize;
			_knifeThrower.OnAttemptsAmountChanged += UpdateAttemptsViews;			
		}

		private void HandleNewInitialize()
		{
			ClearOldViews();
			CreateAttemptsViews();
		}

		private void OnDestroy()
		{
			_knifeThrower.OnAttemptsAmountInitialized -= HandleNewInitialize;
			_knifeThrower.OnAttemptsAmountChanged -= UpdateAttemptsViews;
		}

		private void ClearOldViews()
		{
			if (_attemptsViews.Count > 0)
			{
				for (int i = 0; i < _attemptsViews.Count; i++)
				{
					_attemptsViews[i].Release();
				}
				
				_attemptsViews.Clear();
			}
		}

		private void CreateAttemptsViews()
		{
			_totalHitAttemptsAmount = _knifeThrower.HitAttemptsAmount;
			
			for (int i = 0; i < _totalHitAttemptsAmount; i++)
			{
				Instantiate(_attemptViewPrefab, _viewsContainer)
					.With(v => v.SetUnused())
					.With(v => _attemptsViews.Add(v));
			}
		}

		private void UpdateAttemptsViews()
		{
			var usedAttemptsAmount = _knifeThrower.UsedAttemptsAmount;

			for (int i = _totalHitAttemptsAmount - 1; i >= _totalHitAttemptsAmount - usedAttemptsAmount; i--)
			{
				_attemptsViews[i].SetUsed();
			}
		}
	}
}