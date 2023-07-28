using NaughtyAttributes;
using UnityEngine;
using Utils.ObjectPool;
using Random = UnityEngine.Random;

namespace Gameplay.LevelElements
{
	public class Log : PoolItem
	{
		[SerializeField, BoxGroup("Components")]
		private Transform _rotatingTransform;

		[SerializeField, BoxGroup("Components")]
		private CircleCollider2D _circleCollider;

		[SerializeField, BoxGroup("Components")]
		private Rigidbody2D _rigidbody;

		[SerializeField, BoxGroup("Settings")] private float _throwForce;

		private StageSettings  _stageSettings;
		private AnimationCurve _speedCurve;

		private bool _allowRotating;
		private bool _forwardPlaying;

		private float _curveLength;
		private float _playProgress;

		private Coroutine _rotationRoutine;

		public float Radius => _circleCollider.radius;

		public void Initialize(StageSettings stageSettings)
		{
			_stageSettings = stageSettings;

			_speedCurve = _stageSettings.LogRotationSpeedCurve;
			_curveLength = _curveLength = _speedCurve.keys[_speedCurve.length - 1].time - _speedCurve.keys[0].time;
			;
		}

		public void StartRotation() => _allowRotating = true;
		public void StopRotation() => _allowRotating = false;

		public void Throw()
		{
			_rigidbody.isKinematic = false;

			var randomDirection = Random.insideUnitCircle;

			_rigidbody.AddForce(randomDirection.normalized * _throwForce, ForceMode2D.Impulse);
		}

		public override void Restart()
		{
			base.Restart();
			
			_rigidbody.isKinematic = true;
		}

		private void Update()
		{
			Rotation();
		}

		private void Rotation()
		{
			if (!_allowRotating) return;

			var progressDelta = Time.deltaTime / _curveLength;

			if (_forwardPlaying)
			{
				_playProgress += progressDelta;
			}
			else
			{
				_playProgress -= progressDelta;
			}

			_rotatingTransform.Rotate(Vector3.forward, _speedCurve.Evaluate(_playProgress));

			if (_forwardPlaying && _playProgress >= 1)
			{
				_forwardPlaying = Random.value > 0.5f;
			}
			else if (!_forwardPlaying && _playProgress <= 0)
			{
				_forwardPlaying = Random.value > 0.5f;
			}
		}
	}
}