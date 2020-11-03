using System.Collections.Generic;
using UnityEngine;

namespace Match3.Configurations
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public int LevelWidth = 3;
        public int LevelHeight = 3;

        public List<CellConfiguration> CellConfigurations = null;
    }
}
