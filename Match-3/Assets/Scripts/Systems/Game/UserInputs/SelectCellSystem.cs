﻿using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.UserInputs
{
    public sealed class SelectCellSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChangeFieldAnimating> _changeFieldFilter = null;
        private readonly EcsFilter<ChainEvent>.Exclude<FilledChain> _chainFilter = null;

        public void Run()
        {
            bool isFieldChanging = _changeFieldFilter.GetEntitiesCount() > 0;
            //bool hasChais = _chainFilter.GetEntitiesCount() > 0;

            if (/*hasChais || */ isFieldChanging || !Input.GetMouseButtonDown(0))
            {
                return;
            }

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null)
            {
                return;
            }

            CellView cellView = hit.collider.GetComponent<CellView>();

            if (cellView == null)
            {
                return;
            }

            cellView.Entity.Set<SelectCellAnimationRequest>();
            cellView.Entity.Set<Selected>();
        }
    }
}