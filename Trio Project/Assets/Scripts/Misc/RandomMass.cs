using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMass : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = Random.Range(0, 10);
    }

}
