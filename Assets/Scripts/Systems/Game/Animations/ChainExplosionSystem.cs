using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Animations
{
    public sealed class ChainExplosionSystem: IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Chain, ExplosionEvent> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                Chain chain = _filter.Get1(index);

                for (int i = 0; i < chain.Size; i++)
                {
                    bool isLast = i == chain.Size - 1;
                    Vector2Int position = chain.Position + i * chain.Direction;
                    CellView view = _gameField.Cells[position].Ref<Cell>().Unref().View;
                    view.transform.position += _configuration.Animation.UpCellOnAnimate;
                    var tween = view.transform.DOScale(_configuration.Animation.ExplosionScale, _configuration.Animation.ExplosionSeconds).OnComplete(() => Hide(view, entity, isLast));
                }
            }
        }

        private void Hide(CellView cell, EcsEntity chain, bool animationCompleate)
        {
            cell.gameObject.SetActive(false);

            if (animationCompleate)
            {
                chain.Set<ExplodedEvent>();
            }
        }
    }
}
