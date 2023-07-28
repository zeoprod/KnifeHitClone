using System;
using UnityEngine;

namespace Utils.ObjectPool
{
	[SelectionBase]
	public class PoolItem : MonoBehaviour, IPoolable
	{
		public event Action OnRestart;
		public event Action OnRelease;

		private Transform _transform;
		private GameObject _gameObject;

		public int ID { get; private set; }
		public string ContainerName { get; private set; }
		public Transform Transform
		{
			get
			{
				if (_transform == null) _transform = transform;
				return _transform;
			}
		}
		
		public GameObject GameObject
		{
			get
			{
				if (_gameObject == null) _gameObject = gameObject;
				return _gameObject;
			}
		}

		public virtual void Restart() => OnRestart?.Invoke();

		public virtual void Retain(int id, string containerName)
		{
			ID = id;
			ContainerName = containerName;
		}

		public virtual void Release(bool disableObject = true)
		{
			OnRelease?.Invoke();
			Pool.Release(ID, this, disableObject);
		}

		public void SetParent(Transform parent) => transform.SetParent(parent);
		public void SetActive(bool active) => gameObject.SetActive(active);
	}
}