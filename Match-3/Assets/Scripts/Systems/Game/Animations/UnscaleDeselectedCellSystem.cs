using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.Animations
{
    public sealed class UnscaleDeselectedCellSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, DeselectRequest> _filter = null;

        public void Run()
        {
            AnimationsConfiguration configuration = _configuration.Animation;

            foreach (int index in _filter)
            {
                Transform view = _filter.Get1(index).View.transform;
                view.DOScale(1, configuration.SetectedCellScaleSeconds)
                    .OnComplete(() => { OffsetCellBack(configuration, view); });
            }
        }

        private void OffsetCellBack(AnimationsConfiguration configuration, Transform view)
        {
            view.transform.position -= configuration.UpCellOnAnimate;
        }
    }
}
