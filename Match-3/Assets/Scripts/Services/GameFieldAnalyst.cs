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
            List<ChainEvent> chains = GetChains(cells, _configuration);

            return chains.Count > 0;
        }

        public static List<ChainEvent> GetChains(Dictionary<Vector2Int, EcsEntity> cells, Configuration _configuration)
        {
            List<ChainEvent> result = new List<ChainEvent>();

            for (int column = 0; column < _configuration.LevelWidth; column++)
            {
                for (int row = 0; row < _configuration.LevelHeight; row++)
                {
                    Vector2Int position = new Vector2Int(column, row);

                    if (!cells.ContainsKey(position))
                    {
                        continue;
                    }

                    result.AddRange(GetCellChains(cells, _configuration, position));
                }
            }

            return result;
        }

        public static bool CheckCellInChain(Dictionary<Vector2Int, EcsEntity> cells, Configuration _configuration, Vector2Int position)
        {
            Vector2Int direction = new Vector2Int(0, 1);
            ChainEvent horisontalRight = GetChain(position, direction, cells);

            if (horisontalRight.Size >= _configuration.MinRewardableChain)
            {
                return true;
            }

            direction = new Vector2Int(0, -1);
            ChainEvent horisontalLeft = GetChain(position, direction, cells);

            if (horisontalLeft.Size >= _configuration.MinRewardableChain)
            {
                return true;
            }

            direction = new Vector2Int(1, 0);
            ChainEvent verticalUp = GetChain(position, direction, cells);

            if (verticalUp.Size >= _configuration.MinRewardableChain)
            {
                return true;
            }

            direction = new Vector2Int(-1, 0);
            ChainEvent verticalDown = GetChain(position, direction, cells);

            if (verticalDown.Size >= _configuration.MinRewardableChain)
            {
                return true;
            }

            return false;
        }

        private static List<ChainEvent> GetCellChains(Dictionary<Vector2Int, EcsEntity> cells, Configuration _configuration, Vector2Int position)
        {
            List<ChainEvent> result = new List<ChainEvent>();

            Vector2Int direction = new Vector2Int(0, 1);
            ChainEvent horisontal = GetChain(position, direction, cells);

            if (horisontal.Size >= _configuration.MinRewardableChain)
            {
                result.Add(horisontal);
            }

            direction = new Vector2Int(1, 0);
            ChainEvent vertical = GetChain(position, direction, cells);

            if (vertical.Size >= _configuration.MinRewardableChain)
            {
                result.Add(vertical);
            }

            return result;
        }

        private static ChainEvent GetChain(Vector2Int startPosition, Vector2Int direction, Dictionary<Vector2Int, EcsEntity> cells)
        {
            Vector2Int position = startPosition;

            if (!cells.ContainsKey(position))
            {
                return new ChainEvent();
            }

            CellType cellType = GetCellType(position, cells);
            bool chained = CheckCellChainedBefore(direction, position, cellType, cells);

            if (chained)
            {
                return new ChainEvent();
            }

            int chainSize = 0;

            while (cells.ContainsKey(position) && GetCellType(position, cells) == cellType)
            {
                chainSize++;
                position += direction;
            }

            return new ChainEvent() { Position = startPosition, Direction = direction, Size = chainSize };
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
