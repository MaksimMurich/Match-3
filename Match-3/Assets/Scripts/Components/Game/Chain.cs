using Match3.Configurations;
using UnityEngine;

namespace Match3.Components.Game
{
    public struct Chain
    {
        public int Size;
        public Vector2Int Position;
        public Vector2Int Direction;
        public CellConfiguration CellsConfiguration;
    }
}