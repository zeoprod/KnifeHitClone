using System;
using UnityEngine;

namespace Utils
{
	public static class Extensions
	{
		/// <summary>
		/// Call action on object.
		/// Return self object for next call.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="action"></param>
		/// <typeparam name="T">Any type</typeparam>
		/// <returns></returns>
		public static T With<T>(this T self, Action<T> action)
		{
			action?.Invoke(self);
			return self;
		}

		/// <summary>
		/// Call action on object if condition func returns true.
		/// Return self object for next call.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="action"></param>
		/// <param name="condition"></param>
		/// <typeparam name="T">Any type</typeparam>
		/// <returns></returns>
		public static T With<T>(this T self, Action<T> action, Func<bool> condition)
		{
			if (condition()) action?.Invoke(self);
			return self;
		}

		/// <summary>
		/// Call action on object if condition is true.
		/// Return self object for next call.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="action"></param>
		/// <param name="condition"></param>
		/// <typeparam name="T">Any type</typeparam>
		/// <returns></returns>
		public static T With<T>(this T self, Action<T> action, bool condition)
		{
			if (condition) action?.Invoke(self);
			return self;
		}
		
		/// <summary>
		/// StopCoroutine
		/// </summary>
		/// <param name="coroutine"></param>
		/// <param name="owner">"Usually (this)"</param>
		public static void Stop(this Coroutine coroutine, MonoBehaviour owner)
		{
			if (coroutine != default) owner.StopCoroutine(coroutine);
		}
		
		public static bool IsInLayer(this Component component, LayerMask layer)
			=> layer == (layer | 1 << component.gameObject.layer);
	}
}