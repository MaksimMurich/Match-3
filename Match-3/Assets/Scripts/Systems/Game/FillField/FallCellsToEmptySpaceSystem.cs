using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.FillField
{
    public sealed class FallCellsToEmptySpaceSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;

        public void Run()
        {
            FallToEmptySpace();
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
    }
}