using UnityEngine;

public class BossTiles : MonoBehaviour
{
    public TileStates State;
    public TilePosition Position;

    public void Activate(float time)
    {

    }

    public void DeactivateInstant(float time)
    {

    }

    public TilePosition GetRightTile()
    {
        return new TilePosition(Position.xPos + 1, Position.yPos);
    }
}
