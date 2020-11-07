using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Systems.Game.UserInputs
{
    public sealed class DeselectCellSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, Selected> _filter = null;

        public void Run()
        {
            if (_filter.GetEntitiesCount() == 0 || !Input.GetMouseButtonUp(0))
            {
                return;
            }

            foreach (int index in _filter)
            {
                EcsEntity cell = _filter.GetEntity(index);
                cell.Unset<Selected>();
                cell.Set<DeselectEvent>();
            }
        }
    }
}