using UnityEngine;
using System.Collections;

//A central hub for all the interfaces

public interface IKillable
{
    void Kill();
}

//By using <T>, we can define the damage type to be taken later instead of only using floats.
public interface IDamageable<T>
{
    void Damage(T damageTaken);
}

public interface IPooledObject
{
    void OnObjectSpawn();
}