using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.Animations
{
    public sealed class AnimateEmptySwapSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, UpdateViewPositionEvent, MoveBack> _filter = null;

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
                Vector2Int position = _filter.Get2(index);
                Vector2Int moveBackPosition = _filter.Get4(index).Position;

                entity.Unset<MoveBack>();
                entity.Set<Moving>();

                cell.View.transform
                    .DOMove(new Vector3(position.x, position.y), _configuration.Animation.UpdateCellPositionSeconds)
                    .OnComplete(() => MoveCellBack(entity, cell.View, moveBackPosition));
            }
        }

        private void MoveCellBack(EcsEntity entity, CellView view, Vector2Int position)
        {
            entity.Set<Vector2Int>() = position;

            view.transform
                .DOMove(new Vector3(position.x, position.y), _configuration.Animation.UpdateCellPositionSeconds)
                .OnComplete(() => { entity.Unset<Moving>(); });
        }
    }
}