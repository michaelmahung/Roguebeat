using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//New Garbage-Reduced screen shake script - showing much better performance and control than the previous one. 

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float maxShakeDuration = 0.75f;
    [SerializeField] private float maxShakeMagnitude = 500;

    float _duration = 0;
    float _magnitude = 0;
    bool shaking = false;

    float duration
    {
        get
        {
            return _duration;
        }
        set
        {
            if (value > maxShakeDuration)
            {
                value = maxShakeDuration;
            }

            _duration = value;
        }
    }

    float magnitude
    {
        get
        {
            return _magnitude;
        }
        set
        {
            if (value > maxShakeMagnitude)
            {
                value = maxShakeMagnitude;
            }

            _magnitude = value;
        }
    }

    Vector3 startingPos;
    float resetTime = 1;
    float shakeTimer = 0;
    float magnitudeTimer = 0;
    float resetTimer = 0;

    void Start()
    {
        startingPos = transform.position;
    }

    public void CustomShake(float shakeDur, float shakeMag)
    {
        shaking = true;
        duration = shakeDur;
        magnitude = shakeMag;
        SetRandomPosition();
    }

    public void HeavyShake()
    {
        shaking = true;
        duration = 0.4f;
        magnitude = 75f;
        SetRandomPosition();
    }

    public void Shake()
    {
        shaking = true;
        duration = 0.3f;
        magnitude = 40f;
        SetRandomPosition();
    }

    public void LightShake()
    {
        shaking = true;
        duration = 0.25f;
        magnitude = 15f;
        SetRandomPosition();
    }

    void Update()
    {
        if (shaking)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
                magnitude -= (magnitude / duration) * Time.deltaTime;

                if (magnitude < 0)
                {
                    magnitude = 0;
                }

                SetRandomPosition();
            } else
            {
                shaking = false;
            }
        }

        if (transform.position != startingPos && !shaking)
        {
            if (resetTimer < resetTime)
            {
                resetTimer += Time.deltaTime;
            }

            transform.position = Vector3.Slerp(transform.position, startingPos, resetTimer);
        }
    }

    void SetRandomPosition()
    {
        Vector3 newPos = startingPos + (Random.insideUnitSphere * (Time.deltaTime * magnitude));
        newPos.y = startingPos.y;

        transform.position = newPos;
    }
}
