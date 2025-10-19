using System.Collections.Generic;

public static class BoardLayouts
{
    public static IEnumerable<int> BasicFleet => new[]
    {
        // Small sloop (length 2)
        TileScript.Width * 2 + 3,
        TileScript.Width * 2 + 4,

        // Brigantine (length 3)
        TileScript.Width * 5 + 10,
        TileScript.Width * 6 + 10,
        TileScript.Width * 7 + 10,

        // Galleon (length 4)
        TileScript.Width * 9 + 2,
        TileScript.Width * 9 + 3,
        TileScript.Width * 9 + 4,
        TileScript.Width * 9 + 5
    };
}
