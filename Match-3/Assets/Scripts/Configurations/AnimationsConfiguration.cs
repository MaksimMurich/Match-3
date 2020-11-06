using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class AnimationsConfiguration
    {
        [SerializeField] private float _setectedCellScaleSeconds = .3f;
        [SerializeField] private float _setectedCellScale = 1.2f;
        [SerializeField] private float _swapDuration = .4f;
        [SerializeField] private float _explosionScale = 0.05f;
        [SerializeField] private float _explosionSeconds = .5f;
        [SerializeField] private float _updateCellPositionSeconds = .5f;
        [SerializeField] private Vector3 _upCellOnAnimate = new Vector3(0, 0, -.1f);

        public float SetectedCellScaleSeconds => _setectedCellScaleSeconds;
        public float SetectedCellScale => _setectedCellScale;
        public float SwapDuration => _swapDuration;
        public float ExplosionScale => _explosionScale;
        public float ExplosionSeconds => _explosionSeconds;
        public float UpdateCellPositionSeconds => _updateCellPositionSeconds;
        public Vector3 UpCellOnAnimate => _upCellOnAnimate;

    }
}