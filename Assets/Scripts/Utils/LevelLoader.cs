using UnityEngine.SceneManagement;
using static Core.Enums.SceneType;

namespace Utils
{
    public class LevelLoader
    {
        public void Load(Names sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName.ToString(), mode);
        }
    }
}