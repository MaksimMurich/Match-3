using Match3.Components.Game;
using Match3.UnityComponents;
using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class CellConfiguration
    {
        [SerializeField] private CellType _type;
        [Tooltip("weight of current cell, when choosing a random cell for spawn")]
        [SerializeField] private float _spawnWeight = 10;
        [SerializeField] private int _reward = 50;
        [SerializeField] private CellView _view = null;
        [SerializeField] private CellView _combo4VerticalView = null;
        [SerializeField] private CellView _combo4HorizontalView = null;
        [SerializeField] private CellView _combo5View = null;

        private float _spawnRangeMin = -1;
        private float _spawnRangeMax = -1;

        public CellType Type => _type;
        public float Weight => _spawnWeight;
        public int Reward => _reward;
        public CellView ViewExample => _view;
        public CellView Combo4VerticalView => _combo4VerticalView;
        public CellView Combo4HorizontalView => _combo4HorizontalView;
        public CellView Combo5View => _combo5View;

        public void SetSpawnRange(float minValue, float maxValue)
        {
            _spawnRangeMin = minValue;
            _spawnRangeMax = maxValue;
        }

        public bool CheckInSpawnRabge(float value)
        {
            bool inRange = value >= _spawnRangeMin && value < _spawnRangeMax;
            return inRange;
        }
    }
}