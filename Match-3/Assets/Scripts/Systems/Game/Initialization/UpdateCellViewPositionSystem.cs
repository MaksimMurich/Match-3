using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class UpdateCellViewPositionSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, UpdateViewPositionEvent> _cellsFilter = null;

        public void Run()
        {
            foreach (int index in _cellsFilter)
            {
                Cell cell = _cellsFilter.Get1(index);
                EcsEntity entity = _cellsFilter.GetEntity(index);
                entity.Set<Moving>();
                Vector2Int position = _cellsFilter.Get2(index);

                cell.View.transform
                    .DOMove(new Vector3(position.x, position.y), _configuration.Animation.UpdateCellPositionSeconds)
                    .OnComplete(() => entity.Unset<Moving>());
            }
        }
    }
}