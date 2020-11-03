using Match3.UnityComponents;
using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class CellConfiguration
    {
        public string Name => _name;
        /// <summary>
        /// weight when choosing a random cell for spawn
        /// </summary>
        public float Weight => _weight;
        public CellView ViewExample => _view;

        [SerializeField] private string _name;
        [SerializeField] private float _weight = 0;
        [SerializeField] private CellView _view;
        [SerializeField] private CellView _combo4VerticalView;
        [SerializeField] private CellView _combo4HorizontalView;
        [SerializeField] private CellView _combo5View;
    }
}