using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Systems.Game
{
    public class DestroyEntities : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly EcsFilter<ExplodedEvent> _explodedEventFilter = null;
        private readonly EcsFilter<Chain>.Exclude<FilledChain> _chainFilter = null;

        public void Run()
        {
            if (_explodedEventFilter.GetEntitiesCount() == 0)
            {
                return;
            }

            DestroyEntitiesFromField();
        }

        private void DestroyEntitiesFromField()
        {
            foreach (int index in _chainFilter)
            {
                Chain chain = _chainFilter.Get1(index);
                _chainFilter.GetEntity(index).Set<Destroyed>();

                for (int i = 0; i < chain.Size; i++)
                {
                    Vector2Int cellPosition = chain.Position + chain.Direction * i;

                    if (_gameField.Cells[cellPosition].Equals(EcsEntity.Null))
                    {
                        continue;
                    }

                    _gameField.Cells[cellPosition].Destroy();
                    _gameField.Cells[cellPosition] = EcsEntity.Null;
                }
            }
        }
    }
}