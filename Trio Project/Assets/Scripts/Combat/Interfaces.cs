using UnityEngine;
using System.Collections;

public interface IKillable
{
    void Kill();
}

public interface IDamageable<T>
{
    void Damage(T damageTaken);
}

public interface IPooledObject
{
    void OnObjectSpawn();
}