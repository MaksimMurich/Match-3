using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game
{
    public sealed class ScaleSelectedCellSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, SelectEvent> _filter = null;

        public void Run()
        {
            AnimationsConfiguration configuration = _configuration.AnimationsConfiguration;

            foreach (int index in _filter)
            {
                Transform view = _filter.Get1(index).View.transform;
                view.transform.position += configuration.UpCellOnAnimate;
                view.DOScale(configuration.SetectedCellScale, configuration.SetectedCellScaleSeconds);
            }
        }
    }
}
