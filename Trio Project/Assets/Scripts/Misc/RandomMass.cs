using UnityEngine;

public class RandomMass : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        //Give this thing a random mass
        rb = GetComponent<Rigidbody>();
        rb.mass = Random.Range(0, 10);
    }

}
