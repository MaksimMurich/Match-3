using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.CellTypes;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Systems.Game
{
    public class ChargeCellsToDestroySystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly EcsFilter<ExplosionAnimatedEvent> _explodedEventFilter = null;
        private readonly EcsFilter<Chain>.Exclude<FilledChain> _chainFilter = null;

        public void Run()
        {
            if (_explodedEventFilter.GetEntitiesCount() == 0)
            {
                return;
            }

            ChargeCells();
        }

        private void ChargeCells()
        {
            foreach (int index in _chainFilter)
            {
                Chain chain = _chainFilter.Get1(index);
                _chainFilter.GetEntity(index).Set<Destroyed>();

                CargeChain(chain);
            }
        }

        private void CargeChain(Chain chain)
        {
            for (int i = 0; i < chain.Size; i++)
            {
                Vector2Int cellPosition = chain.Position + chain.Direction * i;
                EcsEntity cellEntity = _gameField.Cells[cellPosition];
                bool cellIsEmpty = cellEntity.Equals(EcsEntity.Null);

                if (cellIsEmpty)
                {
                    continue;
                }

                bool cellIsCharged = cellEntity.Has<ChargedToDestroy>();

                if (cellIsCharged)
                {
                    continue;
                }

                ChargeCell(_gameField.Cells[cellPosition], cellPosition);
            }
        }

        private void ChargeCell(EcsEntity ecsEntity, Vector2Int position)
        {
            if (ecsEntity.Has<ChargedToDestroy>())
            {
                return;
            }

            ecsEntity.Set<ChargedToDestroy>();

            if (ecsEntity.Has<BonusCellFourInRowHorisontal>())
            {
                Vector2Int cellID = new Vector2Int(0, position.y);

                while (_gameField.Cells.ContainsKey(cellID))
                {
                    ChargeCell(_gameField.Cells[cellID], cellID);

                    cellID += Vector2Int.right;
                }
            }

            if (ecsEntity.Has<BonusCellFourInRowVertical>())
            {
                Vector2Int cellID = new Vector2Int(position.x, 0);

                while (_gameField.Cells.ContainsKey(cellID))
                {
                    ChargeCell(_gameField.Cells[cellID], cellID);

                    cellID += Vector2Int.up;
                }
            }
        }
    }
}