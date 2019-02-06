/*
 * This struct will be used to contain general information about the level being created.
 * What I need to keep track of is the minimum and maximum size of the level, as well as the 
 * distance between each room spawned. 
 */
using UnityEngine;

public struct LevelInfo
{
    public int GridFactor; //The amount of cells per row/column
    public int CellOffset; //The distance between each room cell

    public Vector3 LevelStartPoint { get { return Vector3.zero; } }
    public Vector3 CurrentLocation;

    public int MinX { get { return 0; } }
    public int MinY { get { return 0; } }

    public int MaxX { get { return GridFactor - 1; } }
    public int MaxY { get { return GridFactor - 1; } }


    /*When spawning there are a few key things to keep track of.
     * I need to make sure that each level has an open connection to the final room
     * I need to make sure that rooms do not spawn on top of each other
     * I need to make sure that rooms do not spawn outside of the grid
     * I need to make sure that there is a room the player can spawn in
     * I need to make sure that rooms are varied
     * I need to make sure that room spawning is efficient
     * I need to be able to track what the previously spawned room was
     */    
}
