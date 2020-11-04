using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class AnimationsConfiguration
    {
        [SerializeField] private float _setectedCellScaleSeconds = .3f;
        [SerializeField] private float _setectedCellScale = 1.2f;
        [SerializeField] private Vector3 _upCellOnAnimate = new Vector3(0, 0, -.1f);

        public float SetectedCellScaleSeconds => _setectedCellScaleSeconds;
        public float SetectedCellScale => _setectedCellScale;
        public Vector3 UpCellOnAnimate => _upCellOnAnimate;
    }
}