using Core.Enums;
using Core.Services;
using UnityEngine;
using Zenject;

namespace Core
{
	public class Bootstrap : MonoBehaviour
	{
		private SceneLoader _sceneLoader;

		[Inject]
		public void Construct(SceneLoader sceneLoader)
		{
			_sceneLoader = sceneLoader;
		}

		private void Awake()
		{
			Application.targetFrameRate = 60;
			
			_sceneLoader.Load(SceneType.Menu);
		}
	}
}