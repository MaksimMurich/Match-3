using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class GameField
    {
        public readonly Dictionary<Vector2Int, EcsEntity> Cells = new Dictionary<Vector2Int, EcsEntity>();
    }
}
