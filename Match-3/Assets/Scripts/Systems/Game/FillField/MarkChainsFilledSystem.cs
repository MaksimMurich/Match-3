using Leopotam.Ecs;
using Match3.Components.Game;

namespace Match3.Systems.Game.FillField
{
    public sealed class MarkChainsFilledSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Chain, Destroyed>.Exclude<FilledChain> _chainFilter = null;

        public void Run()
        {
            foreach (int index in _chainFilter)
            {
                _chainFilter.GetEntity(index).Set<FilledChain>();
            }
        }
    }
}