using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class ChainRewardSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameField _gameField = null;
        private readonly PlayerState _playerState = null;
        private readonly EcsFilter<ChainEvent> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                int reward = 0;

                ChainEvent chain = _filter.Get1(index);

                for (int i = 0; i < chain.Size; i++)
                {
                    Vector2Int position = chain.Position + i * chain.Direction;
                    reward += GetCellReward(position);
                }

                _playerState.Score += reward;
                _world.NewEntity().Set<RewardRequest>();
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