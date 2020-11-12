using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Configurations;
using System.Linq;
using UnityEditor;
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

                    bool hasChain = true;
                    int tryCount = 0;

                    _gameField.Cells.Add(position, cellEntity);

                    while (hasChain && tryCount < 100)
                    {
                        tryCount++;

                        float random = Random.Range(0f, 100f);
                        CellConfiguration configuration = _configuration.CellConfigurations.Where(c => c.CheckInSpawnRabge(random)).First();

                        cellEntity.Set<Cell>().Configuration = configuration;

                        hasChain = GameFieldAnalyst.CheckCellInChain(_gameField.Cells, _configuration, position);
                    }
                }
            }
        }
    }
}