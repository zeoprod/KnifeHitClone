using UnityEngine;
using UnityEngine.UI;
using Utils.ObjectPool;

namespace Gameplay.UI
{
	public class KnifeThrowingAttemptView : PoolItem
	{
		[SerializeField] private Image _knifeImage;
		[SerializeField] private Color _activatedColor;
		[SerializeField] private Color _deactivatedColor;

		public bool Used { get; private set; }

		public void SetUsed()
		{
			_knifeImage.color = _deactivatedColor;

			Used = true;
		}

		public void SetUnused()
		{
			_knifeImage.color = _activatedColor;

			Used = false;
		}
	}
}