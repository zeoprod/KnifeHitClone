using System.Collections;
using UnityEngine;

namespace Utils.ObjectPool
{
	[RequireComponent(typeof(PoolItem))]
	public class ReleaseAfterDelay : MonoBehaviour
	{
		[SerializeField] private float _delay = 3f;

		private Coroutine _releaseRoutine;
		private IPoolable _poolItem;

		private void Start()
		{
			_poolItem = GetComponent<IPoolable>();
			if (_poolItem != default) _poolItem.OnRestart += RestartObject;
		}

		private void OnEnable()
		{
			RestartObject();
		}

		public void SetDelay(float delay = 0)
		{
			_delay = Mathf.Clamp(delay, 0, float.MaxValue);
		}

		private void RestartObject()
		{
			if (_releaseRoutine != null) StopCoroutine(_releaseRoutine);
			_releaseRoutine = StartCoroutine(ReleaseAfterTimeCor());
		}

		private IEnumerator ReleaseAfterTimeCor()
		{
			yield return new WaitForSeconds(_delay);

			_poolItem?.Release();
		}

		private void OnDisable()
		{
			if (_releaseRoutine != null) StopCoroutine(_releaseRoutine);
		}

		private void OnDestroy()
		{
			if (_poolItem != default) _poolItem.OnRestart -= RestartObject;
		}
	}
}