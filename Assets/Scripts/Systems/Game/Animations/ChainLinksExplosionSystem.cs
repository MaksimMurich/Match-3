using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using Match3.UnityComponents;

namespace Match3.Assets.Scripts.Systems.Game.Animations
{
    public sealed class ChainLinksExplosionSystem: IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, ExplosionEvent> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                Cell cell = _filter.Get1(index);
                CellView view = cell.View;
                view.transform.position += _configuration.Animation.UpCellOnAnimate;
                view.transform.DOScale(_configuration.Animation.ExplosionScale, _configuration.Animation.ExplosionSeconds).OnComplete(() => Hide(entity));
            }
        }

        private void Hide(EcsEntity cell)
        {
            cell.Set<ExplodedEvent>();
        }
    }
}
