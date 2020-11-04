using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class CreateCellsViewSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Cell> _filter = null;

        public void Init()
        {
            foreach (int index in _filter)
            {
                ref Cell cell = ref _filter.Get1(index);
                CellView view = Object.Instantiate(cell.Configuration.ViewExample);
                view.Entity = _filter.GetEntity(index);
                cell.View = view;
            }
        }
    }
}