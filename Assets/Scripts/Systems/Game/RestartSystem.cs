using Leopotam.Ecs;
using Match3.Configurations;
using UnityEngine.SceneManagement;

namespace Match3.Systems.Game
{
    public sealed class RestartSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configurations = null;

        public void Init()
        {
            _sceneData.SettingsView.gameObject.SetActive(_configurations.ShowSettingsOnStart);
            _sceneData.SettingsView.StartGame += StartGameEventHandler;
        }

        private void StartGameEventHandler()
        {
            _configurations.ShowSettingsOnStart = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}