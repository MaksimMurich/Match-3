using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class SwapSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, Vector2Int, SwapRequest> _filter = null;
        private readonly EcsFilter<ChangeFieldAnimating> _fieldChangers = null;
        private readonly EcsFilter<AnimateExplosion> _explosionAnimations = null;
        private readonly EcsFilter<ChainEvent> _chains = null;

        public void Run()
        {
            if (_fieldChangers.GetEntitiesCount() > 0 || _chains.GetEntitiesCount() > 0 || _explosionAnimations.GetEntitiesCount() > 0)
            {
                return;
            }

            foreach (int index in _filter)
            {
                Vector2Int cellPosition = _filter.Get2(index);
                Vector2Int targetPosition = _filter.Get3(index).To;

                EcsEntity swapCell = _filter.GetEntity(index);
                EcsEntity secondCell = _gameField.Cells[targetPosition];

                swapCell.Set<Vector2Int>() = targetPosition;
                swapCell.Set<AnimateSwapRequest>().MainCell = true;

                secondCell.Set<Vector2Int>() = cellPosition;
                secondCell.Set<AnimateSwapRequest>().MainCell = false;

                _gameField.Cells[cellPosition] = secondCell;
                _gameField.Cells[targetPosition] = swapCell;

                List<ChainEvent> chains = GameFieldAnalyst.GetChains(_gameField.Cells, _configuration);

                if (chains.Count == 0)
                {
                    _gameField.Cells[cellPosition] = swapCell;
                    _gameField.Cells[targetPosition] = secondCell;

                    swapCell.Set<Vector2Int>() = cellPosition;

                    AnimateSwapBackRequest request = new AnimateSwapBackRequest();
                    request.TargetPosition = targetPosition;
                    swapCell.Set<AnimateSwapBackRequest>() = request;

                    secondCell.Set<Vector2Int>() = targetPosition;

                    AnimateSwapBackRequest secondRequest = new AnimateSwapBackRequest();
                    secondRequest.TargetPosition = cellPosition;
                    secondCell.Set<AnimateSwapBackRequest>() = secondRequest;
                }
            }
        }
    }
}