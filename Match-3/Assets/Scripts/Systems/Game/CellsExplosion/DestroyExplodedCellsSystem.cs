using Leopotam.Ecs;
using Match3.Components.Game;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class DestroyExplodedCellsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, ChargedToExplosion>.Exclude<AnimateExplosion> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                EcsEntity cell = _filter.GetEntity(index);
                cell.Unset<ChargedToExplosion>();
                cell.Set<EmptySpace>();
            }
        }
    }
}
