using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] float minScale;
    [SerializeField] float addedScale;
    
    [SerializeField] float speed;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.time / speed;

        rect.transform.localScale = 
        new Vector3(Mathf.PingPong(step, addedScale) + minScale, Mathf.PingPong(step, addedScale) + minScale, 1);
    }
}
