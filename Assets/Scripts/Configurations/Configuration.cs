using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        [SerializeField] private int _levelWidth = 3;
        [SerializeField] private int _levelHeight = 3;
        [SerializeField] private int _cellHeight = 1;
        [SerializeField] private int _cellWidth = 1;
        [SerializeField] private float _combo4RewardMultiplayer = 2f;
        [SerializeField] private float _combo5RewardMultiplayer = 4f;
        [SerializeField] private CellConfiguration[] _cellConfigurations = null;

        public int LevelWidth => _levelWidth;
        public int LevelHeight => _levelHeight;
        public int CellHeight => _cellHeight;
        public int CellWidth => _cellWidth;
        public float Combo4Configuration => _combo4RewardMultiplayer;
        public float Combo5Configuration => _combo5RewardMultiplayer;
        public CellConfiguration[] CellConfigurations => _cellConfigurations;
    }
}
