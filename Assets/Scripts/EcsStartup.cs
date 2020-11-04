using Leopotam.Ecs;
using Match3.Configurations;
using Match3.Systems.Game.Initialization;
using UnityEngine;

namespace Match3
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Configuration _configuration = null;

        private EcsWorld _world;
        private EcsSystems _systems;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems
                // register systems
                .Add(new SetCellConfigSpawnRangesSystem())
                .Add(new InitializeFieldSystem())
                .Add(new CreateCellsViewSystem())
                .Add(new SetCellViewPositionSystem())

                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()
                // .OneFrame<TestComponent2> ()

                // inject service instances here (order doesn't important), for example:
                .Inject(_configuration)
                .Inject(new GameField())
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}