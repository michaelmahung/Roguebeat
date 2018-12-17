using System.Collections;
using UnityEngine;

//Will create an orbit using an orbitpath from the Ellipse script and cause the object this is attached to to orbit whatever is selected as the orbiting object.

public class OrbitMotion : MonoBehaviour {

    [Range(0f, 1f)]
    public float OrbitProgress = 0f;
    public float OrbitSpeed = 3f;
    public bool OrbitActive = true;
    public Transform ObjectToOrbit;
    public Ellipse EllipsePath;

    Quaternion fixedRotation;

    private void Start()
    {
        fixedRotation = transform.rotation;

        if (ObjectToOrbit == null)
        {
            OrbitActive = false;
            return;
        }

        SetOrbitingObjectPosition();
        StartCoroutine(AnimateOrbit());
    }

    private void LateUpdate()
    {
        ObjectToOrbit.localRotation = fixedRotation;
    }

    void SetOrbitingObjectPosition ()
    {
        Vector2 orbitPos = EllipsePath.Evaluate(OrbitProgress);
        ObjectToOrbit.rotation = fixedRotation;
        ObjectToOrbit.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    IEnumerator AnimateOrbit()
    {
        if (OrbitSpeed < 0.1f)
        {
            OrbitSpeed = 0.1f;
        }

        float orbitSpeed = 1f / OrbitSpeed;
        while (OrbitActive)
        {
            OrbitProgress += Time.deltaTime * orbitSpeed;
            OrbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }
}
