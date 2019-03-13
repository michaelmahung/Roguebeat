using UnityEngine;

//For now, literally just tracks the players current room, important for everything.
//Can later add things to save.

public class PlayerStats : MonoBehaviour, ITrackRooms
{
    public RoomSetter MyRoom { get; set; }

    public const float HIGHHPMIN = .7f;
    public const float MEDHPMIN = 0.4f;
    public const float LOWHPMIN = 0.1f;
}
