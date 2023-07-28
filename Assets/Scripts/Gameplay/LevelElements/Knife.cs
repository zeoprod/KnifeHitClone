using System;
using ScriptableObjects.Classes;
using UnityEngine;
using Utils;
using Utils.ObjectPool;

namespace Gameplay.LevelElements
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class Knife : PoolItem
	{
		public event Action<Knife> OnStuck;
		public event Action<Knife> OnFailed;

		[SerializeField] private LayerMask _stuckLayerMask;

		private KnifeConfig _config;
		private Rigidbody2D _rigidbody;
		private Collider2D  _collider;

		private bool _used;

		public void Init(KnifeConfig config)
		{
			_config = config;
		}

		public void Throw(Vector3 throwPoint)
		{
			_collider.enabled = true;
			_rigidbody.isKinematic = false;
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.AddForce(throwPoint * _config.ThrowForce, ForceMode2D.Impulse);
		}

		private void ThrowAway(Vector3 direction)
		{
			_collider.enabled = false;
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.AddForce(direction * _config.ThrowForce, ForceMode2D.Impulse);
		}

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
			_collider = GetComponent<Collider2D>();
		}

		public override void Retain(int id, string containerName)
		{
			base.Retain(id, containerName);
			
			ResetState();
		}

		public override void Restart()
		{
			base.Restart();

			ResetState();
		}

		private void ResetState()
		{
			_used = false;

			_collider.enabled = false;
			_rigidbody.isKinematic = true;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (_used) return;

			if (collision.collider.IsInLayer(_stuckLayerMask))
			{
				_used = true;

				Transform.SetParent(collision.transform);
				_rigidbody.isKinematic = true;

				OnStuck?.Invoke(this);
			}
			else
			{
				ThrowAway(collision.GetContact(0).normal);

				_used = true;

				OnFailed?.Invoke(this);
			}
		}
	}
}