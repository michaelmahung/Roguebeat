using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileColor_Alt : MonoBehaviour
{
    [Range(0, 8)] [SerializeField] int band = 4;
    [Range(0, 1)] [SerializeField] float colorChangeThreshold = 0.4f;
    [Range(0, 1)] [SerializeField] float minChangeTime = 0.5f;
    [Range(0, 3)] [SerializeField] float emissionIntensity = 1.25f;
    [SerializeField] Color[] myColors;
    [SerializeField] Color currentColor;
    [SerializeField] Color nextColor;
    [SerializeField] int colorIndex;
    [SerializeField] int nextColorIndex;
    Renderer thisRenderer;
    Material thisMaterial;
    float lerpTimer;
    bool canChange;

    void Start()
    {
        colorIndex = Random.Range(0, myColors.Length -1);
        nextColorIndex = colorIndex + 1;
        thisRenderer = GetComponent<Renderer>();
        thisMaterial = thisRenderer.material;

        thisMaterial.SetColor("_EmissionColor", myColors[colorIndex] * emissionIntensity);
    }

    void Update()
    {
        if (lerpTimer < minChangeTime)
        {
            lerpTimer += Time.deltaTime;
        } else
        {
            canChange = true;
        }

        if (AudioPeer._audioBandBuffer[band] > colorChangeThreshold && canChange)
        {
            lerpTimer = 0;
            ChangeColor();
        }

        if (colorIndex < myColors.Length && nextColorIndex < myColors.Length)
        {
            thisMaterial.color = Color.Lerp(myColors[colorIndex], myColors[nextColorIndex], lerpTimer);
        }
    }

    void ChangeColor()
    {
        canChange = false;
        colorIndex += 1;
        nextColorIndex = colorIndex + 1;

        if (colorIndex >= myColors.Length)
        {
            colorIndex = 0;
            nextColorIndex = colorIndex + 1;
        }

        if (nextColorIndex >= myColors.Length)
        {
            nextColorIndex = 0;
        }

        thisMaterial.SetColor("_EmissionColor", myColors[colorIndex] * emissionIntensity);
    }
}
