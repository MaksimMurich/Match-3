﻿using DG.Tweening;
using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class AnimateInitializedCellsMovingSystem : IEcsInitSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;

        public void Init()
        {
            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    ref Cell cell = ref _gameField.Cells[new Vector2Int(row, column)].Ref<Cell>().Unref();
                    _gameField.Cells[new Vector2Int(row, column)].Set<ChangeFieldAnimating>();

                    Vector3 targetPosition = new Vector3(row, column);
                    cell.View.transform.DOMove(targetPosition, _configuration.Animation.CellMovingSeconds);

                }
            }
        }
    }
}