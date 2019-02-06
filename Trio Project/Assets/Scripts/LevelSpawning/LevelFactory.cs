/*
 * This struct will be used to contain general information about the level being created.
 * What I need to keep track of is the minimum and maximum size of the level, as well as the 
 * distance between each room spawned. 
 */


public struct LevelInfo
{
    public int GridFactor; //The amount of cells per row/column

    public int CellOffset; //The distance between each room cell

    public int minX { get { return 0; } }
    public int minY { get { return 0; } }

    public int maxX { get { return GridFactor - 1; } }
    public int maxY { get { return GridFactor - 1; } }
}
