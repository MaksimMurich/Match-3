using Leopotam.Ecs;
using Match3.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Initialization
{
    public sealed class ConfigurateCameraSystem : IEcsInitSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;

        public void Init()
        {
            float cameraSize = _configuration.LevelHeight / 2f;
            float cameraViewWidth = cameraSize * Screen.width / (float)Screen.height;

            if (cameraViewWidth < _configuration.LevelWidth / 2f)
            {
                cameraSize *= _configuration.LevelWidth / 2f / cameraViewWidth;
            }

            _sceneData.Camera.orthographic = true;
            _sceneData.Camera.orthographicSize = cameraSize;
            _sceneData.Camera.transform.position = new Vector3((_configuration.LevelWidth - 1) / 2f, (_configuration.LevelHeight - 1f) / 2f, -10);
        }
    }
}
