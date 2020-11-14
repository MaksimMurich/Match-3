using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.UnityComponents;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class DestroyExplodedCellsViewSystem : IEcsRunSystem
    {
        private readonly ObjectPool _pool = null;
        private readonly EcsFilter<Cell, ChargedToExplosion>.Exclude<AnimateExplosion> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                CellView view = _filter.Get1(index).View;
                _pool.Stash(view);
            }
        }
    }
}
