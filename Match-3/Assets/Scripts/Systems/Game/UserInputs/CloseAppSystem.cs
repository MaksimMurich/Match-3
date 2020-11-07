using Leopotam.Ecs;

namespace Match3.Systems.Game.UserInputs
{
    public sealed class CloseAppSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = null;

        public void Init()
        {
            _sceneData.SettingsView.CloseAppClickEvent += CloseAppClickEventHandler;
        }

        private void CloseAppClickEventHandler()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
