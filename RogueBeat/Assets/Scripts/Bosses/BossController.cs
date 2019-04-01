using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : MonoBehaviour, IBossController
{
    public abstract void StartBoss();

    public abstract void StopBoss();
}
