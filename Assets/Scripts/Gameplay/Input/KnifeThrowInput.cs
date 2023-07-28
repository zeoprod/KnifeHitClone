using System;
using Zenject;

namespace Gameplay.Input
{
	public class KnifeThrowInput : ITickable
	{
		public event Action OnThrowButtonDown;

		private readonly KnifeThrowInputState _inputState;

		public KnifeThrowInput(KnifeThrowInputState inputState)
		{
			_inputState = inputState;
		}

		public void Tick()
		{
			_inputState.IsThrowing = UnityEngine.Input.GetMouseButtonDown(0);

			if (_inputState.IsThrowing) OnThrowButtonDown?.Invoke();
		}
	}
}