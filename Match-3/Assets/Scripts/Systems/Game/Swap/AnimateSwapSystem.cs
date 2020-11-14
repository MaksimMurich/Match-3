using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class AnimateSwapSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, AnimateSwapRequest>.Exclude<AnimateSwapBackRequest> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                entity.Set<ChangeFieldAnimating>();
                float zPosition = _filter.Get3(index).MainCell ? -1 : 0;
                Vector2Int cellPosition = _filter.Get2(index);
                Vector3 target = new Vector3(cellPosition.x, cellPosition.y, zPosition);

                Transform view = _filter.Get1(index).View.transform;
                view.position += new Vector3(0, 0, zPosition - view.position.z);
                view.DOMove(target, _configuration.Animation.SwapDuration)
                    .OnComplete(() => OnSwapCompleate(entity, view));
            }
        }

        private void OnSwapCompleate(EcsEntity entity, Transform view)
        {
            entity.Unset<ChangeFieldAnimating>();
            entity.Set<ActivateBonusRequest>();
            view.transform.position -= new Vector3(0, 0, view.transform.position.z);
        }
    }
}