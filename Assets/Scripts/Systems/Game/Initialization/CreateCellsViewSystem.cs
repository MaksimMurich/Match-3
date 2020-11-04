using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class CreateCellsViewSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Cell> _cellsFilter = null;

        public void Init()
        {
            foreach (int index in _cellsFilter)
            {
                ref Cell cell = ref _cellsFilter.Get1(index);
                CellView view = Object.Instantiate(cell.Configuration.ViewExample);
                cell.View = view;
            }
        }
    }
}