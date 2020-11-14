using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class InitializeFieldViewSystem : IEcsInitSystem
    {
        private readonly Configuration _configuration = null;
        private readonly GameField _gameField = null;
        private readonly ObjectPool _objectPool = null;

        public void Init()
        {
            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    Vector2Int position = new Vector2Int(column, row);
                    EcsEntity entity = _gameField.Cells[position];
                    ref Cell cell = ref entity.Ref<Cell>().Unref();
                    cell.View = _objectPool.Get(cell.Configuration.ViewExample);
                    cell.View.transform.position = new Vector3(position.x, _configuration.LevelHeight + 1);
                    cell.View.Entity = entity;
                }
            }
        }
    }
}