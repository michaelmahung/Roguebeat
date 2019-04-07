using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDecal : MonoBehaviour
{
    [SerializeField] float fadeTime;
    Renderer rend;
    Material mat;
    Color startColor;
    Color endColor;
    float timer;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
        startColor = mat.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    private void OnEnable()
    {
        mat.color = startColor;
    }

    private void Update()
    {
        timer += Time.deltaTime / fadeTime;

        if (timer < 1)
        {
            mat.color = Color.Lerp(startColor, endColor, timer);
            return;
        }

        Disable();
    }

    void Disable()
    {
        timer = 0;
        this.gameObject.SetActive(false);
    }
}
