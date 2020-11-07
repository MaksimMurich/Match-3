using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class ChainRewardSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly PlayerState _playerState = null;
        private readonly EcsFilter<Chain>.Exclude<Rewarded> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                _filter.GetEntity(index).Set<Rewarded>();
                _filter.GetEntity(index).Set<RewardedEvent>();

                int reward = 0;

                Chain chain = _filter.Get1(index);

                for (int i = 0; i < chain.Size; i++)
                {
                    Vector2Int position = chain.Position + i * chain.Direction;
                    reward += GetCellReward(position);
                }

                _playerState.Score += reward;
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