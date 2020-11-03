using Match3.UnityComponents;
using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class CellConfiguration
    {
        /// <summary>
        /// weight when choosing a random cell for spawn
        /// </summary>
        public float Weight => _weight;
        public CellView ViewExample => _view;

        [SerializeField] private float _weight = 0;
        [SerializeField] private CellView _view = null;
        [SerializeField] private CellView _combo4VerticalView = null;
        [SerializeField] private CellView _combo4HorizontalView = null;
        [SerializeField] private CellView _combo5View = null;
    }
}