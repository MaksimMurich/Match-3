using Leopotam.Ecs;

namespace Match3.Systems.Game.UserInputs.UI
{
    public sealed class OpenSettingsSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = null;

        public void Init()
        {
            _sceneData.NavigationView.OpenSettingsClickEvent += OpenSettingsClickEventHandler;
        }

        private void OpenSettingsClickEventHandler()
        {
            _sceneData.SettingsView.gameObject.SetActive(true);
        }
    }
}