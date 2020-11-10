using Leopotam.Ecs;
using Match3.Assets.Scripts.Components.Game.CellTypes;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;
using System;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class CreateCellsViewSystem : IEcsRunSystem
    {
        private readonly ObjectPool _objectPool = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, EmptyViewEvent, Vector2Int>.Exclude<BonusCellFourInRowVertical>.Exclude<BonusCellFourInRowHorisontal> _filter = null;
        private readonly EcsFilter<Cell, EmptyViewEvent, Vector2Int, BonusCellFourInRowVertical> _bonusVerticalFilter = null;
        private readonly EcsFilter<Cell, EmptyViewEvent, Vector2Int, BonusCellFourInRowHorisontal> _bonusHorisontalFilter = null;

        public void Run()
        {
            SpawnSimpleCells();
            SpawnBonusHorisontalCells();
            SpawnBonusVerticalCells();
        }

        private void SpawnBonusHorisontalCells()
        {
            foreach (int index in _bonusHorisontalFilter)
            {
                ref Cell cell = ref _bonusHorisontalFilter.Get1(index);
                CellView view = _objectPool.Get(cell.Configuration.Combo4HorizontalView);
                view.Entity = _bonusHorisontalFilter.GetEntity(index);

                cell.View = view;
                Vector2Int position = _bonusHorisontalFilter.Get3(index);
                view.transform.position = new Vector2(position.x, position.y);
                view.Entity.Set<SpawnBonusEvent>();
            }
        }

        private void SpawnBonusVerticalCells()
        {
            foreach (int index in _bonusVerticalFilter)
            {
                ref Cell cell = ref _bonusVerticalFilter.Get1(index);
                CellView view = _objectPool.Get(cell.Configuration.Combo4VerticalView);
                view.Entity = _bonusVerticalFilter.GetEntity(index);

                cell.View = view;
                Vector2Int position = _bonusVerticalFilter.Get3(index);
                view.transform.position = new Vector2(position.x, position.y);
                view.Entity.Set<SpawnBonusEvent>();
            }
        }

        private void SpawnSimpleCells()
        {
            foreach (int index in _filter)
            {
                ref Cell cell = ref _filter.Get1(index);
                CellView view = _objectPool.Get(cell.Configuration.ViewExample);
                view.Entity = _filter.GetEntity(index);

                cell.View = view;
                view.transform.position = new Vector2(_filter.Get3(index).x, _configuration.LevelHeight);
                view.Entity.Set<UpdateViewPositionEvent>();
            }
        }
    }
}