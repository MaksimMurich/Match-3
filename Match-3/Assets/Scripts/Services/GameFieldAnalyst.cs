using Leopotam.Ecs;
using Match3.Components.Game;
using Match3.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Assets.Scripts.Services
{
    public static class GameFieldAnalyst
    {
        internal static bool HasChain(Dictionary<Vector2Int, EcsEntity> cells, Configuration _configuration)
        {
            List<Chain> chains = GetChains(cells, _configuration);

            return chains.Count > 0;
        }

        public static List<Chain> GetChains(Dictionary<Vector2Int, EcsEntity> cells, Configuration _configuration)
        {
            List<Chain> result = new List<Chain>();

            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    Vector2Int position = new Vector2Int(column, row);

                    if (!cells.ContainsKey(position))
                    {
                        continue;
                    }

                    Vector2Int direction = new Vector2Int(0, 1);
                    Chain horisontal = GetChain(position, direction, cells, _configuration.MinRewardableChain);

                    if (horisontal.Size >= _configuration.MinRewardableChain)
                    {
                        result.Add(horisontal);
                    }

                    direction = new Vector2Int(1, 0);
                    Chain vertical = GetChain(position, direction, cells, _configuration.MinRewardableChain);

                    if (vertical.Size >= _configuration.MinRewardableChain)
                    {
                        result.Add(vertical);
                    }
                }
            }

            return result;
        }

        private static Chain GetChain(Vector2Int startPosition, Vector2Int direction, Dictionary<Vector2Int, EcsEntity> cells, int minRewardableCHain)
        {
            Vector2Int position = startPosition;

            if (!cells.ContainsKey(position))
            {
                return new Chain();
            }

            CellType cellType = GetCellType(position, cells);
            bool chained = CheckCellChainedBefore(direction, position, cellType, cells);

            if (chained)
            {
                return new Chain();
            }

            int chainSize = 0;

            while (cells.ContainsKey(position) && GetCellType(position, cells) == cellType)
            {
                chainSize++;
                position += direction;
            }

            return new Chain() { Position = startPosition, Direction = direction, Size = chainSize };
        }

        private static bool CheckCellChainedBefore(Vector2Int direction, Vector2Int position, CellType cellType, Dictionary<Vector2Int, EcsEntity> cells)
        {
            bool result = false;
            Vector2Int previousCell = position - direction;
            bool hasCellBefore = cells.ContainsKey(previousCell);

            if (hasCellBefore)
            {
                CellType typeBefore = GetCellType(previousCell, cells);
                result = typeBefore == cellType;
            }

            return result;
        }

        private static CellType GetCellType(Vector2Int position, Dictionary<Vector2Int, EcsEntity> cells)
        {
            return cells[position].Ref<Cell>().Unref().Configuration.Type;
        }
    }
}
