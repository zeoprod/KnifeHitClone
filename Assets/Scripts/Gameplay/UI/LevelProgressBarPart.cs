using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
	public class LevelProgressBarPart : MonoBehaviour
	{
		[SerializeField] private Image      _fill;
		[SerializeField] private GameObject _checkMark;
		[SerializeField] private GameObject _currentMark;

		public void SetAsCurrent()
		{
			_fill.fillAmount = 0f;
			_checkMark.SetActive(false);
			_currentMark.SetActive(true);
		}

		public void SetAsCompleted()
		{
			_fill.fillAmount = 1f;
			_checkMark.SetActive(true);
			_currentMark.SetActive(false);
		}

		public void SetAsNotCompleted()
		{
			_fill.fillAmount = 0f;
			_checkMark.SetActive(false);
			_currentMark.SetActive(false);
		}
	}
}