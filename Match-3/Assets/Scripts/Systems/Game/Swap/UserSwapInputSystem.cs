using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using UnityEngine;

namespace Match3.Systems.Game.Swap
{
    public sealed class UserSwapInputSystem : IEcsRunSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly Configuration _configuration = null;
        private readonly GameField _gameField = null;
        private readonly EcsFilter<Cell, Vector2Int, Selected> _filter = null;

        public void Run()
        {
            Vector2 mousePosition = _sceneData.Camera.ScreenToWorldPoint(Input.mousePosition);

            foreach (int index in _filter)
            {
                Cell cell = _filter.Get1(index);
                Vector2 cellPosition = cell.View.transform.position;
                Vector2 mouseOffset = mousePosition - cellPosition;
                EcsEntity cellEntity = _filter.GetEntity(index);

                Vector2Int offset = Vector2Int.zero;

                if (Mathf.Abs(mouseOffset.x) > _configuration.SwapMinMouseOffset)
                {
                    int offsetX = mouseOffset.x > 0 ? 1 : -1;
                    offset = new Vector2Int(offsetX, 0);
                }
                else if (Mathf.Abs(mouseOffset.y) > _configuration.SwapMinMouseOffset)
                {
                    int offsetY = mouseOffset.y > 0 ? 1 : -1;
                    offset = new Vector2Int(0, offsetY);
                }

                Vector2Int fieldPosition = _filter.Get2(index);
                Vector2Int targetPosition = fieldPosition + offset;

                if (offset.Equals(Vector2Int.zero) || !_gameField.Cells.ContainsKey(targetPosition))
                {
                    continue;
                }

                SwapRequest swap = new SwapRequest()
                {
                    From = fieldPosition,
                    To = targetPosition
                };

                cellEntity.Set<SwapRequest>() = swap;
                cellEntity.Unset<Selected>();
                cellEntity.Set<DeselectCellAnimationRequest>();
            }
        }
    }
}