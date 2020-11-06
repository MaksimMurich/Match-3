using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class DetectChainsSystem : IEcsRunSystem
    {
        private bool _movingDetected;

        private readonly EcsWorld _ecsWorld = null;
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Moving> _movingFilter = null;


        public void Run()
        {
            if(_movingFilter.GetEntitiesCount() > 0)
            {
                _movingDetected = true;
            }
            else if(_movingDetected)
            {
                DetectChains();
                _movingDetected = false;
            }
        }

        private void DetectChains()
        {
            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    DetectHorisontalChain(column, row);
                    DetectVerticalChain(column, row);
                }
            }
        }

        private void DetectVerticalChain(int column, int row)
        {
            DetectChain(new Vector2Int(0, 1), column, row);
        }

        private void DetectHorisontalChain(int column, int row)
        {
            DetectChain(new Vector2Int(1, 0), column, row);
        }

        private void DetectChain(Vector2Int direction, int column, int row)
        {
            Vector2Int startPosition = new Vector2Int(column, row);
            Vector2Int position = startPosition;

            if (!_gameField.Cells.ContainsKey(position))
            {
                return;
            }

            CellType cellType = GetCellType(position);
            bool chained = CheckCellChained(direction, position, cellType);

            if (chained)
            {
                return;
            }

            int chainSize = 0;

            while (_gameField.Cells.ContainsKey(position) && GetCellType(position) == cellType)
            {
                chainSize++;
                position += direction;
            }

            if (_configuration.MinRewardableChain <= chainSize)
            {
                MarkChain(startPosition, direction, chainSize);
            }
        }

        private bool CheckCellChained(Vector2Int direction, Vector2Int position, CellType cellType)
        {
            bool result = false;
            Vector2Int previousCell = position - direction;
            bool hasCellBefore = _gameField.Cells.ContainsKey(previousCell);

            if (hasCellBefore)
            {
                CellType typeBefore = GetCellType(previousCell);
                result = typeBefore == cellType;
            }

            return result;
        }

        private CellType GetCellType(Vector2Int position)
        {
            return _gameField.Cells[position].Ref<Cell>().Unref().Configuration.Type;
        }

        private void MarkChain(Vector2Int startPosition, Vector2Int direction, int chainSize)
        {
            var chainEntity = _ecsWorld.NewEntity();
            chainEntity.Set<ExplosionEvent>();

            ref Chain chain = ref chainEntity.Set<Chain>();
            chain.Size = chainSize;
            chain.Direction = direction;
            chain.Position = startPosition;
        }
    }
}