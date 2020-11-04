using Leopotam.Ecs;
using Match3.Components.GameField;
using Match3.Configurations;
using System.Linq;
using UnityEngine;

namespace Match3.Systems
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
                    cellEntity.Set<Vector2Int>() = new Vector2Int(row, column);
                    Cell cell = cellEntity.Set<Cell>();
                    float random = Random.Range(0f, 100f);
                    CellConfiguration cellConfiguration = _configuration.CellConfigurations.Where(c => c.CheckInSpawnRabge(random)).First();
                    cell.Configuration = cellConfiguration;
                }
            }
        }
    }
}