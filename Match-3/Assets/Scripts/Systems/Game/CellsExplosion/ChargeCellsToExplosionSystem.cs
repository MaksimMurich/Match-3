using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class ChargeCellsToExplosionSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly EcsFilter<ChainEvent> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ChainEvent chain = _filter.Get1(index);
                Vector2Int currentPosition = chain.Position;

                for (int i = 0; i < chain.Size; i++)
                {
                    EcsEntity cellEntity = _gameField.Cells[currentPosition];
                    currentPosition += chain.Direction;

                    MarkCells(cellEntity);
                }
            }
        }

        private void MarkCells(EcsEntity cellEntity)
        {
            cellEntity.Set<AnimateExplosionRequest>();
            cellEntity.Set<ChargedToExplosion>();
        }
    }
}
