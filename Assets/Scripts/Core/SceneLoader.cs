using System;
using System.Threading.Tasks;
using Core.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services
{
	public class SceneLoader
	{
		public void Load(SceneType sceneType, Action onLoaded = null)
		{
			LoadScene(sceneType, onLoaded);
		}
		
		private async void LoadScene(SceneType sceneType, Action onLoaded = null)
		{
			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneType.ToString());

			while (!waitNextScene.isDone)
			{
				await Task.Yield();
			}

			onLoaded?.Invoke();
		}
	}
}