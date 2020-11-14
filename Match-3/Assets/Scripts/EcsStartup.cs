using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Assets.Scripts.Systems.Game.Animations;
using Match3.Assets.Scripts.Systems.Game.CellsExplosion;
using Match3.Assets.Scripts.Systems.Game.Initialization;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using Match3.Systems.Game;
using Match3.Systems.Game.Initialization;
using Match3.Systems.Game.Swap;
using Match3.Systems.Game.UserInputs;
using Match3.Systems.Game.UserInputs.UI;
using UnityEngine;

namespace Match3
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Configuration _configuration = null;
        [SerializeField] private SceneData _sceneData = null;

        private EcsWorld _world;
        private EcsSystems _systems;
        private ObjectPool _objectPool;

        private readonly GameField _gameField = new GameField();
        private readonly PlayerState _playerState = new PlayerState();

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _objectPool = new ObjectPool();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif

            _systems

                // initialization
                .Add(new SetCellConfigSpawnRangesSystem())
                .Add(new InitializeFieldSystem())
                .Add(new InitializeFieldViewSystem())
                .Add(new ConfigurateCameraSystem())
                .Add(new AnimateInitializedCellsMovingSystem())

                // user input event handlers
                .Add(new OpenSettingsSystem())
                .Add(new ChangeSettingsSystem())
                .Add(new RestartSystem())
                .Add(new CancelSettingsSystem())
                .Add(new CloseAppSystem())

                //select cell
                .OneFrame<SelectCellAnimationRequest>()
                .OneFrame<DeselectCellAnimationRequest>()
                .Add(new SelectCellSystem())
                .Add(new ScaleSelectedCellSystem())

                // swap
                .OneFrame<SwapRequest>()
                .Add(new UserSwapInputSystem())
                .OneFrame<AnimateSwapRequest>()
                .OneFrame<AnimateSwapBackRequest>()
                .Add(new SwapSystem())
                .Add(new AnimateSwapSystem())
                .Add(new AnimateSwapBackSystem())

                // deselect cell
                .Add(new DeselectCellSystem())
                .Add(new UnscaleDeselectedCellSystem())

                // create chains
                .OneFrame<ChainEvent>()
                .Add(new CreateChainsSystem()) // on SwapRequest or field was unlocked

                // explode cells
                .OneFrame<AnimateExplosionRequest>()
                .Add(new ChargeCellsToExplosionSystem())
                .Add(new AnimateCellsExplosionSystem()) // mark as AnimateExplosion while animating
                .Add(new DestroyExplodedCellsViewSystem())
                .Add(new DestroyExplodedCellsSystem())

                // fill field
                .OneFrame<AnimateFallDownRequest>()
                .Add(new FallCellsToEmptySpacesSystem())
                .Add(new AnimateFallDownSystem())

                .OneFrame<CreateCellViewRequest>()
                .Add(new CreateRandomCellsToEmptySpacesSystem())
                .OneFrame<AnimateCreatedViewRequest>()
                .Add(new CreateCellsViewSystem())
                .Add(new AnimateCreatedViewSystem())

                // reward
                .OneFrame<RewardRequest>()
                .Add(new ChainRewardSystem())
                .Add(new AnimateRewardSystem())

                // inject service instances here (order doesn't important), for example:
                .Inject(_gameField)
                .Inject(_configuration)
                .Inject(_sceneData)
                .Inject(_playerState)
                .Inject(_objectPool)
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