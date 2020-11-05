using Leopotam.Ecs;
using Match3.Components.Game;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game
{
    public sealed class SwapSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly EcsFilter<Cell, Vector2Int, SwapEvent> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                Debug.Log(_filter.Get1(index).View.name);
            }
        }
    }
}
