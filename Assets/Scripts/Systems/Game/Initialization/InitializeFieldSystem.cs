using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System.Linq;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class InitializeFieldSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly Configuration _configuration = null;
        private readonly GameField _gameField = null;

        public void Init()
        {
            for (int row = 0; row < _configuration.LevelHeight; row++)
            {
                for (int column = 0; column < _configuration.LevelWidth; column++)
                {
                    EcsEntity cellEntity = _world.NewEntity();
                    Vector2Int position = new Vector2Int(column, row);
                    cellEntity.Set<Vector2Int>() = position;
                    cellEntity.Set<EmptyViewEvent>();

                    float random = Random.Range(0f, 100f);
                    CellConfiguration cellConfiguration = _configuration.CellConfigurations.Where(c => c.CheckInSpawnRabge(random)).First();
                    cellEntity.Set<Cell>().Configuration = cellConfiguration;

                    _gameField.Cells.Add(position, cellEntity);
                }
            }
        }
    }
}