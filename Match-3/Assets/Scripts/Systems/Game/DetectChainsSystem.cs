using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using Match3.Components.Game;
using Match3.Components.Game.Events;
using Match3.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Systems.Game
{
    public sealed class DetectChainsSystem : IEcsRunSystem
    {
        private bool _fieldChanged;

        private readonly EcsWorld _ecsWorld = null;
        private readonly GameField _gameField = null;
        private readonly Configuration _configuration = null;
        private readonly EcsFilter<ChangeFieldAnimating> _changeField = null;

        public void Run()
        {
            if (_changeField.GetEntitiesCount() > 0)
            {
                _fieldChanged = true;
            }
            else if (_fieldChanged)
            {
                _fieldChanged = false;
                List<Chain> chains = GameFieldAnalyst.GetChains(_gameField.Cells, _configuration);

                for (int i = 0; i < chains.Count; i++)
                {
                    AddChain(chains[i].Position, chains[i].Direction, chains[i].Size);
                }
            }
        }

        private void AddChain(Vector2Int startPosition, Vector2Int direction, int chainSize)
        {
            var chainEntity = _ecsWorld.NewEntity();
            chainEntity.Set<ExplosionRequest>();

            ref Chain chain = ref chainEntity.Set<Chain>();
            chain.Size = chainSize;
            chain.Direction = direction;
            chain.Position = startPosition;
        }
    }
}