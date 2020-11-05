using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using System;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class ChainRewardSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly GameField _gameField = null;
        private readonly PlayerState _playerState = null;
        private readonly EcsFilter<Chain>.Exclude<Rewarded> _filter = null;

        public void Run()
        {
            foreach(int index in _filter)
            {
                _filter.GetEntity(index).Set<Rewarded>();

                int reward = 0;

                Chain chain = _filter.Get1(index);

                for (int i = 0; i < chain.Size; i++)
                {
                    Vector2Int position = chain.Position + i * chain.Direction;
                    reward += GetCellReward(position);
                    Debug.Log($"reward = {reward}");
                }

                _playerState.Score += reward;
                Debug.Log($"score = {_playerState.Score}");

            }
        }

        private int GetCellReward(Vector2Int position)
        {
            if (!_gameField.Cells.ContainsKey(position))
            {
                return 0;
            }

            int result = _gameField.Cells[position].Ref<Cell>().Unref().Configuration.Reward;

            return result;
        }
    }
}