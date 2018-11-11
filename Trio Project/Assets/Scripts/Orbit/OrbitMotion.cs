using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will create an orbit using an orbitpath from the Ellipse script and cause the object this is attached to to orbit whatever is selected as the orbiting object.

public class OrbitMotion : MonoBehaviour {

    Quaternion fixedRotation;
    public Transform orbitingObject;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    private void Start()
    {
        fixedRotation = transform.rotation;

        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }

        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
    }

    private void LateUpdate()
    {
        orbitingObject.localRotation = fixedRotation;
    }

    void SetOrbitingObjectPosition ()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.rotation = fixedRotation;
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    IEnumerator AnimateOrbit()
    {
        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }

        float orbitSpeed = 1f / orbitPeriod;
        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }
}
