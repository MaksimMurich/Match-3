using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using System.Linq;
using UnityEngine;

namespace Match3.Assets.Scripts.Systems.Game.CellsExplosion
{
    public sealed class CreateRandomCellsToEmptySpacesSystem : IEcsRunSystem
    {
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<EmptySpace> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                float random = Random.Range(0f, 100f);
                CellConfiguration configuration = _configuration.CellConfigurations.Where(c => c.CheckInSpawnRabge(random)).First();

                EcsEntity entity = _filter.GetEntity(index);
                entity.Unset<EmptySpace>();
                entity.Set<Cell>().Configuration = configuration;
                entity.Set<CreateCellViewRequest>();
            }
        }
    }
}
