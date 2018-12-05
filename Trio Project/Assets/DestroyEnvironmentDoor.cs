using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnvironmentDoor : BaseDoor
{
    private int itemsDestroyed;
    public int itemsRequired;

    public void ItemDestroyed()
    {
        itemsDestroyed++;

        if (itemsDestroyed >= itemsRequired && !doorMoved)
        {
            doorMoved = true;
            OpenDoor();
        }


        Debug.LogFormat("{0} items left", (itemsRequired - itemsDestroyed));
    }
}
