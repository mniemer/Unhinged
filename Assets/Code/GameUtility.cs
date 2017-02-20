using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class GameUtility
{
    public static int gameToGridCoord(float coord)
    {
        return (int) (coord + 9.5);
    }

    public static float gridToGameCoord(int coord)
    {
        return (coord - 9.5f);
    }
}

