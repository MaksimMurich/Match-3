using Leopotam.Ecs;
using Match3.Assets.Scripts.Systems.Game.Animations;
using Match3.Assets.Scripts.Systems.Game.Initialization;
using Match3.Components.Game;
using Match3.Configurations;
using Match3.Systems.Game;
using Match3.Systems.Game.Initialization;
using Match3.Systems.Game.UserInputs;
using UnityEngine;

namespace Match3
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Configuration _configuration = null;
        [SerializeField] private SceneData _sceneData = null;
        private readonly GameField _gameField = new GameField();

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

                // initialization
                .Add(new SetCellConfigSpawnRangesSystem())
                .Add(new InitializeFieldSystem())
                .Add(new CreateCellsViewSystem())
                .Add(new SetCellViewPositionSystem())
                .Add(new ConfigurateCameraSystem())

                // user input event handlers
                .Add(new OpenSettingsSystem())
                .Add(new ChangeSettingsSystem())
                .Add(new RestartSystem())
                .Add(new SelectCellSystem())
                .Add(new DeselectCellSystem())
                .Add(new UserSwapSystem())

                // view effects
                .Add(new ScaleSelectedCellSystem())
                .Add(new UnscaleDeselectedCellSystem())
                .Add(new AnimateSwapSystem())

                // update game field
                .Add(new DetectSwapChainsSystem())

                 // register one-frame components
                 .OneFrame<SwapEvent>()
                 .OneFrame<SelectEvent>()
                 .OneFrame<DeselectEvent>()
                 .OneFrame<SwapCompleateEvent>()

                // inject service instances here (order doesn't important), for example:
                .Inject(_gameField)
                .Inject(_configuration)
                .Inject(_sceneData)
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