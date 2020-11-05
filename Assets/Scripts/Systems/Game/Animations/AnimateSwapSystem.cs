using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;
using System;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Animations
{
    public sealed class AnimateSwapSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, SwapEvent> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                Vector2Int target = _filter.Get3(index).TargetPosition;
                Vector3 targetPosition = new Vector3(target.x, target.y, 0);
                CellView cell = _filter.Get1(index).View;

                EcsEntity entity = _filter.GetEntity(index);
                entity.Set<FieldInputLocker>();
                cell.transform.DOMove(targetPosition, _configuration.Animation.SwapDuration)
                    .OnComplete(() => OnSwapCompleate(ref entity));
                entity.Unset<SwapEvent>();
            }
        }

        private void OnSwapCompleate(ref EcsEntity entity)
        {
            entity.Unset<FieldInputLocker>();
            entity.Set<SwapCompleateEvent>();
        }
    }
}
