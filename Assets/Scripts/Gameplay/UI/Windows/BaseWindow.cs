using System.Collections;
using UnityEngine;
using Utils;

namespace Gameplay.UI.Windows
{
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class BaseWindow : MonoBehaviour
	{
		[SerializeField, Range(0f, 10f)] private float _showDelay;
		[SerializeField, Range(0f, 5f)]  private float _fadeDuration;

		private CanvasGroup _canvasGroup;
		private Coroutine   _animationRoutine;

		protected virtual void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();

			Hide(true);
		}

		public virtual void Show(bool force = false)
		{
			if (force)
			{
				_canvasGroup.alpha = 1;
				_canvasGroup.interactable = true;
				_canvasGroup.blocksRaycasts = true;

				return;
			}
			
			_animationRoutine.Stop(this);
			_animationRoutine = StartCoroutine(RunToggleViewRoutine(true));
		}

		public void Hide(bool force = false)
		{
			_animationRoutine.Stop(this);

			if (force)
			{
				_canvasGroup.alpha = 0;
				_canvasGroup.interactable = false;
				_canvasGroup.blocksRaycasts = false;

				return;
			}

			_animationRoutine = StartCoroutine(RunToggleViewRoutine(false));
		}

		private IEnumerator RunToggleViewRoutine(bool state)
		{
			if (_showDelay > 0f)
			{
				yield return new WaitForSeconds(_showDelay);
			}

			var time       = 0f;
			var startAlpha = _canvasGroup.alpha;
			var endAlpha   = state ? 1f : 0f;

			while (enabled && time <= _fadeDuration)
			{
				time += Time.deltaTime;

				_canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / _fadeDuration);

				yield return null;
			}

			_canvasGroup.interactable = state;
			_canvasGroup.blocksRaycasts = state;
		}
	}
}