using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {

        [SerializeField] private int _levelWidth = 3;
        [SerializeField] private int _levelHeight = 3;
        [SerializeField] private float _combo4RewardMultiplayer = 2f;
        [SerializeField] private float _combo5RewardMultiplayer = 4f;
        [SerializeField] private CellConfiguration[] _cellConfigurations = null;

        private bool _cellRangesGenerated = false;

        public int LevelWidth => _levelWidth;
        public int LevelHeight => _levelHeight;
        public float Combo4Configuration => _combo4RewardMultiplayer;
        public float Combo5Configuration => _combo5RewardMultiplayer;

        public CellConfiguration[] CellConfigurations
        {
            get
            {
                if (!_cellRangesGenerated)
                {
                    GenerateCellRanges();
                }

                return _cellConfigurations;
            }
        }

        private void GenerateCellRanges()
        {
            float currentMax = 0;

            for (int i = 0; i < _cellConfigurations.Length; i++)
            {
                float min = currentMax;
                currentMax += _cellConfigurations[i].Weight;
                _cellConfigurations[i].SetSpawnRange(currentMax, currentMax);
            }

            _cellRangesGenerated = true;
        }
    }
}
