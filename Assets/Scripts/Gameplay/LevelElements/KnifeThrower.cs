using System;
using System.Collections.Generic;
using Core.Enums;
using Gameplay.Factories;
using Gameplay.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.LevelElements
{
	public class KnifeThrower : MonoBehaviour
	{
		public event Action OnAttemptsAmountInitialized;
		public event Action OnAttemptsAmountChanged;
		
		public event Action OnSuccessfulHit;
		public event Action OnFailedHit;
		
		[SerializeField] private Transform _throwPoint;

		private readonly List<Knife> _knives = new();
		
		private int _totalHitAttemptsAmount;
		private int _hitAttemptsAmount;

		private bool _knifeReady;

		private Knife           _currentKnife;
		private KnivesFactory   _knivesFactory;
		private KnifeThrowInput _input;

		public int HitAttemptsAmount => _hitAttemptsAmount;
		public int UsedAttemptsAmount => _totalHitAttemptsAmount - _hitAttemptsAmount;

		[Inject]
		public void Construct(KnivesFactory knivesFactory, KnifeThrowInput input)
		{
			_input = input;
			_knivesFactory = knivesFactory;

			_input.OnThrowButtonDown += Throw;
		}

		private void OnDestroy()
		{
			_input.OnThrowButtonDown -= Throw;
		}

		public void Initialize(StageSettings stageSettings)
		{
			ClearOldKnives();
			
			_totalHitAttemptsAmount = stageSettings.NeededHitsAmount;
			_hitAttemptsAmount = _totalHitAttemptsAmount;

			OnAttemptsAmountInitialized?.Invoke();

			CreateKnife();
		}

		private void ClearOldKnives()
		{
			if (_knives.Count > 0)
			{
				foreach (var knife in _knives) knife.Release(); 
			}
			
			if (_currentKnife != null) _currentKnife.Release();
			
			_knives.Clear();
		}

		private void Throw()
		{
			if (!_knifeReady) return;
			_knifeReady = false;

			_hitAttemptsAmount--;

			_currentKnife.Throw(_throwPoint.up);

			OnAttemptsAmountChanged?.Invoke();
		}

		private void CreateKnife()
		{
			if (_hitAttemptsAmount == 0) return;
			
			_currentKnife = _knivesFactory.CreateKnife(KnifeType.Default, _throwPoint.position, _throwPoint.rotation);

			_knives.Add(_currentKnife);
			
			_knifeReady = true;
			
			_currentKnife.OnStuck += HandleKnifeStuck;
			_currentKnife.OnFailed += HandleKnifeFailed;
		}

		private void HandleKnifeStuck(Knife knife)
		{
			_currentKnife.OnStuck -= HandleKnifeStuck;
			_currentKnife.OnFailed -= HandleKnifeFailed;
			
			CreateKnife();
			
			OnSuccessfulHit?.Invoke();
		}

		private void HandleKnifeFailed(Knife knife)
		{
			_currentKnife.OnStuck -= HandleKnifeStuck;
			_currentKnife.OnFailed -= HandleKnifeFailed;
			
			OnFailedHit?.Invoke();
		}
	}
}