﻿using UnityEngine;

//For now, literally just tracks the players current room, important for everything.
//Can later add things to save.

public class PlayerStats : MonoBehaviour, ITrackRooms
{
    public string CurrentRoom { get; set; }
}
