﻿using Match3.UnityComponents;
using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class CellConfiguration
    {
        [Tooltip("weight of current cell, when choosing a random cell for spawn")]
        [SerializeField] private float _spawnWeight = 0;
        [SerializeField] private CellView _view = null;
        [SerializeField] private CellView _combo4VerticalView = null;
        [SerializeField] private CellView _combo4HorizontalView = null;
        [SerializeField] private CellView _combo5View = null;

        private float _spawnRangeMin = -1;
        private float _spawnRangeMax = -1;

        public float Weight => _spawnWeight;
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