using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerMineBlink : MonoBehaviour {

    [SerializeField] private Renderer myRenderer;
    [SerializeField] private Material myMaterial;
    [SerializeField] private Color mineColor;
    [SerializeField] private float pulseSpeed = 5;
    [SerializeField] private float minIntensity = 1.25f;
    [SerializeField] private float maxIntensity = 3.5f;
    [SerializeField] private float currentIntensity;

    void Start () {

        myRenderer = GetComponent<Renderer>();
        myMaterial = myRenderer.material;
	}
	
	void Update () {

        currentIntensity = minIntensity + Mathf.PingPong(Time.time * pulseSpeed, maxIntensity);

        myMaterial.SetColor("_EmissionColor", mineColor * currentIntensity);
	}
}
