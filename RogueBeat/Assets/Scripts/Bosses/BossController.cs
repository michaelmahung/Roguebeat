using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : MonoBehaviour, IBossController
{
    public bool MainBoss;

    public abstract void StartBoss();

    public abstract void StopBoss();
}
