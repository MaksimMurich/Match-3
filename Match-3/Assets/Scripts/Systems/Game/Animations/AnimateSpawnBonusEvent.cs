using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.Animations
{
    public class AnimateSpawnBonusEvent : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, SpawnBonusEvent> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                CellView view = _filter.Get1(index).View;
                view.transform.localScale = Vector3.zero;
                view.transform.DOScale(Vector3.one, _configuration.Animation.ExplosionSeconds);
                Vector3 to = view.transform.position;
                view.transform.position += Vector3.up;
                view.transform.DOMove(to, _configuration.Animation.ExplosionSeconds);
            }
        }
    }
}