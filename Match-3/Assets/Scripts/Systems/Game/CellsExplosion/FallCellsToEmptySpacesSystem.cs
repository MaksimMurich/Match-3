using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class FallCellsToEmptySpacesSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<EmptySpace> _filter = null;

        public void Run()
        {
            bool fullField = _filter.GetEntitiesCount() == 0;

            if (fullField)
            {
                return;
            }

            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    Vector2Int position = new Vector2Int(column, row);
                    EcsEntity cell = _gameField.Cells[position];

                    if (!cell.Has<EmptySpace>())
                    {
                        continue;
                    }

                    bool emptySpace = true;

                    Vector2Int extenderPosition = position;

                    while (emptySpace)
                    {
                        extenderPosition = extenderPosition + Vector2Int.up;
                        bool hasCellID = _gameField.Cells.ContainsKey(extenderPosition);

                        if (!hasCellID)
                        {
                            break;
                        }

                        emptySpace = _gameField.Cells[extenderPosition].Has<EmptySpace>();
                    }

                    SwapCells(position, extenderPosition);
                }
            }
        }

        private void SwapCells(Vector2Int position, Vector2Int extenderPosition)
        {
            bool hasCellID = _gameField.Cells.ContainsKey(extenderPosition);

            if (!hasCellID)
            {
                return;
            }

            EcsEntity emptyEntity = _gameField.Cells[position];
            EcsEntity extenderEntity = _gameField.Cells[extenderPosition];
            _gameField.Cells.Remove(position);
            _gameField.Cells.Remove(extenderPosition);
            _gameField.Cells.Add(position, extenderEntity);
            _gameField.Cells.Add(extenderPosition, emptyEntity);
            emptyEntity.Set<Vector2Int>() = extenderPosition;
            extenderEntity.Set<Vector2Int>() = position;

            extenderEntity.Set<AnimateFallDownRequest>();
        }
    }
}
