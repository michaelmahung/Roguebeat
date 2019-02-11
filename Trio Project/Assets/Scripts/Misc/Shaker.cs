using System.Collections;
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
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        if (!shaking)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(ShakeTime);
        shaking = false;
        transform.position = originalPos;
    }

}
