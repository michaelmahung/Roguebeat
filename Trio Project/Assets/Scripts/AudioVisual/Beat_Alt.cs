using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat_Alt : MonoBehaviour
{
    [SerializeField] int band = 4;
    [SerializeField] float minScale = 0.5f;
    [SerializeField] float maxScale = 2.5f;
    Vector3 newScale;

    void Update()
    {
        if (gameObject.activeInHierarchy)

        newScale = new Vector3(AudioPeer._audioBandBuffer[band] * (maxScale - minScale) + minScale, AudioPeer._audioBandBuffer[band] * 
            (maxScale - minScale) + minScale, AudioPeer._audioBandBuffer[band] * (maxScale - minScale) + minScale);

        transform.localScale = newScale;
    }
}
