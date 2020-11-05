using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using System;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class DetectSwapChainsSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, SwapCompleateEvent> _filter = null;

        public void Run()
        {
            foreach(int index in _filter)
            {
                UpdateField(_filter.Get1(index).Configuration.Type, _filter.Get2(index));
            }
        }

        private void UpdateField(CellType type, Vector2Int position)
        {
            int verticalChain = 1;
            verticalChain += GetChainSize(type, position, new Vector2Int(0, 1));
            int bottomSize = GetChainSize(type, position, new Vector2Int(0, -1));
            verticalChain += bottomSize;
            Vector2Int startPosition = position - bottomSize * new Vector2Int(0, 1);

            if (_configuration.MinRewardableChane <= verticalChain)
            {
                MarkChane(startPosition, new Vector2Int(0, 1), verticalChain);
            }

            int horisontalChain = 1;
            int leftSize = GetChainSize(type, position, new Vector2Int(-1, 0));
            horisontalChain += leftSize;
            horisontalChain += GetChainSize(type, position, new Vector2Int(1, 0));
            startPosition = position - leftSize * new Vector2Int(1, 0);

            if (_configuration.MinRewardableChane <= horisontalChain)
            {
                MarkChane(startPosition, new Vector2Int(1, 0), horisontalChain);
            }
        }
        private int GetChainSize(CellType type, Vector2Int position, Vector2Int direction)
        {
            int result = 0;

            Vector2Int currentPosition = position + direction;

            while (_gameField.Cells.ContainsKey(currentPosition))
            {
                if (_gameField.Cells[currentPosition].Ref<Cell>().Unref().Configuration.Type != type)
                {
                    break;
                }

                currentPosition += direction;
                result++;
            }

            return result;
        }

        private void MarkChane(Vector2Int startPosition, Vector2Int direction, int chainSize)
        {
            for (int i = 0; i < chainSize; i++)
            {
                Vector2Int cellPosition = startPosition + direction * i;
                _gameField.Cells[cellPosition].Set<ChainLink>().ChainSize = chainSize;
                _gameField.Cells[cellPosition].Set<ExplosionEvent>();
                _gameField.Cells[cellPosition].Set<FieldInputLocker>();
            }
        }
    }
}