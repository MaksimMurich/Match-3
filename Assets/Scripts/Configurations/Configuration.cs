using System.Collections.Generic;
using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public int LevelWidth => _levelWidth;
        public int LevelHeight => _levelHeight;
        public float Combo4Configuration => _combo4RewardMultiplayer;
        public float Combo5Configuration => _combo5RewardMultiplayer;
        public List<CellConfiguration> CellConfigurations => _cellConfigurations;

        [SerializeField] private int _levelWidth = 3;
        [SerializeField] private int _levelHeight = 3;
        [SerializeField] private float _combo4RewardMultiplayer = 2f;
        [SerializeField] private float _combo5RewardMultiplayer = 4f;
        [SerializeField] private List<CellConfiguration> _cellConfigurations = null;
    }
}
