﻿//Obsolete camera shaker, used coroutine and generated too much garbage
//Also didnt offer much control in how it could be used

/*using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour {

    [Range(1, 200)]
    [SerializeField] private float ShakeAmount;
    [Range(0.01f, 2)]
    [SerializeField] private float ShakeTime;

    private bool shaking;


    private void Update()
    {
        if (shaking)
        {
            Vector3 newPos = transform.position + (Random.insideUnitSphere * (Time.deltaTime * ShakeAmount));
            newPos.y = transform.position.y;

            transform.position = newPos;
        }
    }

    public void ShakeMe(float amount, float time)
    {
        ShakeAmount = amount;
        ShakeTime = time;
        StartCoroutine(Shake());
    }

    // -- Remove Coroutine to reduce GC
    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        if (!shaking)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(ShakeTime);
        shaking = false;
        transform.position = Vector3.Slerp(transform.position, originalPos, 1);
    }

}*/
