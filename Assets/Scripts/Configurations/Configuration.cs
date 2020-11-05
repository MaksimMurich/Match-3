using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public int LevelWidth = 9;
        public int LevelHeight = 5;

        [SerializeField] private float _swapMinOffset = .5f;
        [SerializeField] private float _combo4RewardMultiplayer = 2f;
        [SerializeField] private float _combo5RewardMultiplayer = 4f;
        [SerializeField] private AnimationsConfiguration _animationsConfiguration = null;
        [SerializeField] private CellConfiguration[] _cellConfigurations = null;

        public float SwapMinOffset => _swapMinOffset;
        public float Combo4Configuration => _combo4RewardMultiplayer;
        public float Combo5Configuration => _combo5RewardMultiplayer;
        public AnimationsConfiguration Animation => _animationsConfiguration;
        public CellConfiguration[] CellConfigurations => _cellConfigurations;
    }
}
