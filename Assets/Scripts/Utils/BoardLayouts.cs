using System.Collections.Generic;

public static class BoardLayouts
{
    public static IEnumerable<int> BasicFleet => new[]
    {
        TileScript.Width * 2 + 3,
        TileScript.Width * 2 + 4,

        TileScript.Width * 5 + 10,
        TileScript.Width * 6 + 10,
        TileScript.Width * 7 + 10,

        TileScript.Width * 9 + 2,
        TileScript.Width * 9 + 3,
        TileScript.Width * 9 + 4,
        TileScript.Width * 9 + 5
    };

    public static IEnumerable<int> GenerateRandomFleet(int seed)
    {
        var random = new System.Random(seed);
        var occupied = new HashSet<int>();
        int[] ships = { 4, 3, 2 };

        foreach (int shipLength in ships)
        {
            PlaceShip(shipLength, random, occupied);
        }

        return occupied;
    }

    private static void PlaceShip(int length, System.Random random, HashSet<int> occupied)
    {
        const int maxAttempts = 200;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            bool horizontal = random.NextDouble() > 0.5;
            int maxX = horizontal ? TileScript.Width - length : TileScript.Width - 1;
            int maxY = horizontal ? TileScript.Height - 1 : TileScript.Height - length;

            int startX = random.Next(0, maxX + 1);
            int startY = random.Next(0, maxY + 1);

            List<int> candidate = new List<int>();
            bool canPlace = true;

            for (int i = 0; i < length; i++)
            {
                int x = horizontal ? startX + i : startX;
                int y = horizontal ? startY : startY + i;
                int pos = y * TileScript.Width + x;

                if (occupied.Contains(pos))
                {
                    canPlace = false;
                    break;
                }

                candidate.Add(pos);
            }

            if (!canPlace)
            {
                continue;
            }

            foreach (int pos in candidate)
            {
                occupied.Add(pos);
            }

            return;
        }

        foreach (int pos in BasicFleet)
        {
            occupied.Add(pos);
        }
    }
}
