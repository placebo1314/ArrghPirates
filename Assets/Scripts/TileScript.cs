using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Material OriginalWater;
    public Material TargetWater;
    public Material BlackWater;

    public const int Width = 20;
    public const int Height = 12;

    public enum TileShotResult
    {
        Unknown,
        Miss,
        Hit
    }

    [System.Serializable]
    public class BoardTile
    {
        public bool HasShip;
        public TileShotResult ShotResult = TileShotResult.Unknown;

        public bool WasShot => ShotResult != TileShotResult.Unknown;
    }

    public Dictionary<int, BoardTile> Board { get; } = new Dictionary<int, BoardTile>();

    public void SetOriginalWater(Renderer target)
    {
        target.sharedMaterial = OriginalWater;
    }

    public void SetTargetWater(Renderer target)
    {
        target.sharedMaterial = TargetWater;
    }

    public void SetBlackWater(Renderer target)
    {
        target.sharedMaterial = BlackWater;
    }

    public void SetupStartBoard(IEnumerable<int> shipPositions)
    {
        Board.Clear();

        int totalTiles = Width * Height;
        for (int i = 0; i < totalTiles; i++)
        {
            Board[i] = new BoardTile();
        }

        foreach (int position in shipPositions)
        {
            if (position < 0 || position >= totalTiles)
            {
                Debug.LogWarning($"Ship position {position} is outside of the board and will be ignored.");
                continue;
            }

            Board[position].HasShip = true;
        }
    }

    public bool TryGetTile(int position, out BoardTile tile)
    {
        return Board.TryGetValue(position, out tile);
    }

    public void RegisterHit(int position)
    {
        if (!Board.TryGetValue(position, out BoardTile tile))
        {
            Debug.LogWarning($"Tried to register a hit on invalid position {position}.");
            return;
        }

        tile.ShotResult = TileShotResult.Hit;
    }

    public void RegisterMiss(int position)
    {
        if (!Board.TryGetValue(position, out BoardTile tile))
        {
            Debug.LogWarning($"Tried to register a miss on invalid position {position}.");
            return;
        }

        tile.ShotResult = TileShotResult.Miss;
    }

    public int CountRemainingShips()
    {
        int remaining = 0;
        foreach (BoardTile tile in Board.Values)
        {
            if (tile.HasShip && tile.ShotResult != TileShotResult.Hit)
            {
                remaining++;
            }
        }

        return remaining;
    }
}
