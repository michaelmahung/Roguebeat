using UnityEngine;
using System.Collections;

//A central hub for all the interfaces

//By using <T>, we can define the damage type to be taken later instead of only using floats.
public interface IDamageable<T>
{
    void Damage(T damageTaken);
}

public interface IKillable
{
    int KillPoints { get; set; }
    void Kill();
}

public interface IPooledObject
{
    void OnObjectSpawn();
}

public interface IWeaponSwap
{
    void WeaponSwapped();
}

public interface IChangeSong
{
    void SongChanged();
}

public interface ITrackRooms
{
    string MyRoomName { get; set; }
    RoomSetter MyRoom{get;set;}
}