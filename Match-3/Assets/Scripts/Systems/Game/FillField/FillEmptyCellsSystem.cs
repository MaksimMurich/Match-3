using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System.Linq;
using UnityEngine;

namespace Match3.Systems.Game.FillField
{
    public sealed class FillEmptyCellsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _ecsWorld = null;
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;

        public void Run()
        {
            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    bool cellIsEmpty = CheckCellIsEmpty(column, row);

                    if (cellIsEmpty)
                    {
                        GenerateCell(column, row);
                    }
                }
            }
        }

        private bool CheckCellIsEmpty(int column, int row)
        {
            bool empty = _gameField.Cells[new Vector2Int(column, row)].Equals(EcsEntity.Null); ;
            return empty;
        }

        private void GenerateCell(int column, int row)
        {
            EcsEntity cellEntity = _ecsWorld.NewEntity();
            Vector2Int position = new Vector2Int(column, row);
            cellEntity.Set<Vector2Int>() = position;

            float random = UnityEngine.Random.Range(0f, 100f);
            CellConfiguration cellConfiguration = _configuration.CellConfigurations.Where(c => c.CheckInSpawnRabge(random)).First();
            cellEntity.Set<Cell>().Configuration = cellConfiguration;
            cellEntity.Set<EmptyViewEvent>();

            _gameField.Cells[position] = cellEntity;
        }
    }
}