﻿using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.Initialization
{
    public sealed class CreateCellsViewSystem : IEcsRunSystem
    {
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<Cell, EmptyViewEvent, Vector2Int> _filter = null;

        public void Run()
        {
            foreach (int index in _filter)
            {
                ref Cell cell = ref _filter.Get1(index);
                CellView view = Object.Instantiate(cell.Configuration.ViewExample);
                view.Entity = _filter.GetEntity(index);
                cell.View = view;
                view.transform.position = new Vector2(_filter.Get3(index).x, _configuration.LevelHeight);
                view.Entity.Set<UpdateViewPositionEvent>();
            }
        }
    }
}