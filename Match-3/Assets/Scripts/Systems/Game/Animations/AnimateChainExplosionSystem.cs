using DG.Tweening;
using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Animations
{
    public sealed class AnimateChainExplosionSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly ObjectPool _objectPool = null;
        private readonly EcsFilter<Chain, ExplosionEvent>.Exclude<ExplosionAnimatedEvent> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                Chain chain = _filter.Get1(index);
                int lastChainId = chain.Size - 1;
                bool lastChain = index == _filter.GetEntitiesCount() - 1;

                for (int i = 0; i < chain.Size; i++)
                {
                    Vector2Int position = chain.Position + i * chain.Direction;
                    CellView view = _gameField.Cells[position].Ref<Cell>().Unref().View;
                    view.transform.position += _configuration.Animation.UpCellOnAnimate;

                    var tween = view.transform.DOScale(_configuration.Animation.ExplosionScale, _configuration.Animation.ExplosionSeconds).OnComplete(() =>
                    {
                        Hide(view, entity);
                    });
                }
            }
        }

        private void Hide(CellView cell, EcsEntity chain)
        {
            _objectPool.Stash(cell);
            chain.Set<ExplosionAnimatedEvent>();
        }
    }

    public class AnimateCellExplosionSystem : IEcsRunSystem
    {
        public readonly GameField _gameField = null;

        public void Run()
        {
            
        }
    }
}
