using Leopotam.Ecs;
using Match3.Components.Game;
using UnityEngine;

namespace Match3.Systems.Game
{
    public class DestroyCellsSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly EcsFilter<Cell, Vector2Int, ChargedToDestroy> _filter = null;

        public void Run()
        {
            DestroyCells();
        }

        private void DestroyCells()
        {
            foreach (int index in _filter)
            {
                Vector2Int position = _filter.Get2(index);
                _gameField.Cells[position].Destroy();
                _gameField.Cells[position] = EcsEntity.Null;
            }
        }
    }
}