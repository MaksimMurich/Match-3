using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class SetCellViewPositionSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Cell, Vector2Int> _cellsFilter = null;

        public void Init()
        {
            foreach (int index in _cellsFilter)
            {
                Cell cell = _cellsFilter.Get1(index);
                Vector2Int position = _cellsFilter.Get2(index);
                cell.View.transform.position = new Vector2(position.x, position.y);
            }
        }
    }
}