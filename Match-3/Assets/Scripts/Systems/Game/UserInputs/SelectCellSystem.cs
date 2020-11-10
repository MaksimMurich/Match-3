using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.UnityComponents;
using UnityEngine;

namespace Match3.Systems.Game.UserInputs
{
    public sealed class SelectCellSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Cell, Moving> _ecsFilter = null;
        private readonly EcsFilter<ChainEvent>.Exclude<FilledChain> _chainFilter = null;

        public void Run()
        {
            bool moving = _ecsFilter.GetEntitiesCount() > 0;
            bool hasChais = _chainFilter.GetEntitiesCount() > 0;

            if (moving || hasChais || !Input.GetMouseButtonDown(0))
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

            cellView.Entity.Set<SelectEvent>();
            cellView.Entity.Set<Selected>();
        }
    }
}