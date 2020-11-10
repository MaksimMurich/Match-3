using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System.Linq;
using UnityEngine;

namespace Match3.Systems.Game.UpdateFieldData
{
    public sealed class FillFieldSystem : IEcsRunSystem
    {
        private readonly EcsWorld _ecsWorld = null;
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<ExplodedEvent> _explodedEventFilter = null;
        private readonly EcsFilter<ChainEvent>.Exclude<FilledChain> _chainFilter = null;

        public void Run()
        {
            if (_explodedEventFilter.GetEntitiesCount() == 0)
            {
                return;
            }

            bool destroyed = DestroyEntitiesFromField();

            if (destroyed)
            {
                FallToEmptySpace();
                FillEmptySpaces();
            }
        }

        private bool DestroyEntitiesFromField()
        {
            bool destroyed = false;

            foreach (int index in _chainFilter)
            {
                ChainEvent chain = _chainFilter.Get1(index);

                for (int i = 0; i < chain.Size; i++)
                {
                    Vector2Int cellPosition = chain.Position + chain.Direction * i;

                    if (_gameField.Cells[cellPosition].Equals(EcsEntity.Null))
                    {
                        continue;
                    }

                    _gameField.Cells[cellPosition].Destroy();
                    _gameField.Cells[cellPosition] = EcsEntity.Null;
                    destroyed = true;
                }

                _chainFilter.GetEntity(index).Set<FilledChain>();
            }

            return destroyed;
        }

        private bool FallToEmptySpace()
        {
            bool fall = false;

            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 1; row < _configuration.LevelHeight; row++)
                {
                    Vector2Int cellId = new Vector2Int(column, row);
                    EcsEntity cell = _gameField.Cells[cellId];

                    bool cellIsEmpty = cell.Equals(EcsEntity.Null);
                    bool bottomCellFilled = !_gameField.Cells[cellId - new Vector2Int(0, 1)].Equals(EcsEntity.Null);

                    if (cellIsEmpty || bottomCellFilled)
                    {
                        continue;
                    }

                    int currentTargetRow = row - 1;
                    bool bottomCellIsEmpty = CheckCellIsEmpty(column, currentTargetRow);

                    while (bottomCellIsEmpty && _gameField.Cells.ContainsKey(new Vector2Int(column, currentTargetRow - 1)))
                    {
                        bottomCellIsEmpty = CheckCellIsEmpty(column, currentTargetRow - 1);

                        if (!bottomCellIsEmpty)
                        {
                            break;
                        }

                        currentTargetRow--;
                    }

                    Vector2Int targetCellPosition = new Vector2Int(column, currentTargetRow);
                    _gameField.Cells[targetCellPosition] = cell;
                    _gameField.Cells[cellId] = EcsEntity.Null;

                    cell.Set<Vector2Int>() = targetCellPosition;
                    cell.Set<UpdateViewPositionEvent>().StartPosition = cellId;
                    fall = true;
                }
            }

            return fall;
        }

        private bool CheckCellIsEmpty(int column, int row)
        {
            bool empty = _gameField.Cells[new Vector2Int(column, row)].Equals(EcsEntity.Null); ;
            return empty;
        }

        private void FillEmptySpaces()
        {
            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    if (CheckCellIsEmpty(column, row))
                    {
                        GenerateCell(column, row);
                    }
                }
            }
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

            _gameField.Cells[position] = EcsEntity.Null;
            _gameField.Cells[position] = cellEntity;
        }
    }
}