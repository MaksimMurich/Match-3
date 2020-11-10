using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Animations
{
    public sealed class AnimateCellViewPositionSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, UpdateViewPositionEvent>.Exclude<MoveBack> _filter = null;

        public void Run()
        {
            MoveCells();
        }

        private void MoveCells()
        {
            foreach (int index in _filter)
            {
                Cell cell = _filter.Get1(index);
                EcsEntity entity = _filter.GetEntity(index);
                entity.Set<Animating>();
                Vector2Int position = _filter.Get2(index);

                cell.View.transform
                    .DOMove(new Vector3(position.x, position.y), _configuration.Animation.UpdateCellPositionSeconds)
                    .OnComplete(() => entity.Unset<Animating>());
            }
        }
    }
}