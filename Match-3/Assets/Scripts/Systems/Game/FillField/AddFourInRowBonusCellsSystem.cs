using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.CellTypes;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using UnityEngine;

namespace Match3.Systems.Game.FillField
{
    public sealed class AddFourInRowBonusCellsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _ecsWorld = null;
        private readonly GameField _gameField = null;
        private readonly EcsFilter<Chain, Destroyed>.Exclude<FilledChain> _chainFilter = null;

        public void Run()
        {
            foreach (int index in _chainFilter)
            {
                Chain chain = _chainFilter.Get1(index);

                if (chain.Size == 4)
                {
                    AddFourBonusCell(chain);
                }
            }
        }

        private void AddFourBonusCell(Chain chain)
        {
            EcsEntity cellEntity = _ecsWorld.NewEntity();
            Vector2Int position = chain.Position + chain.Direction * 2;
            cellEntity.Set<Vector2Int>() = position;

            if (chain.Direction.x == 1)
            {
                cellEntity.Set<BonusCellFourInRowHorisontal>();
            }
            else
            {
                cellEntity.Set<BonusCellFourInRowVertical>();
            }

            cellEntity.Set<Cell>().Configuration = chain.CellsConfiguration;
            cellEntity.Set<EmptyViewEvent>();

            _gameField.Cells[position] = cellEntity;
        }
    }
}