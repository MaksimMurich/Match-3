using Leopotam.Ecs;
using Match3.Configurations;
using System;

namespace Match3.Systems.Game
{
    public sealed class ChangeSettingsSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;

        public void Init()
        {
            _sceneData.SettingsView.ChangeFieldHeight += ChangeFieldHeightEventHandler;
            _sceneData.SettingsView.ChangeFieldWidth += ChangeFieldHeightEventWidth;
        }

        private void ChangeFieldHeightEventWidth(int value)
        {
            _configuration.LevelWidth = value;
        }

        private void ChangeFieldHeightEventHandler(int value)
        {
            _configuration.LevelHeight = value;
        }
    }
}