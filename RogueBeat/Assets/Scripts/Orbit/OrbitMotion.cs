using System.Collections;
using UnityEngine;

//Will create an orbit using an orbitpath from the Ellipse script and cause the object this is attached to to orbit whatever is selected as the orbiting object.

public class OrbitMotion : MonoBehaviour {

    [Range(0f, 1f)]
    [SerializeField] private float OrbitProgress = 0f;
    [SerializeField] private float OrbitSpeed = 3f;
    [SerializeField] private bool OrbitActive = true;
    [SerializeField] private Transform ObjectToOrbit;
    [SerializeField] private Ellipse EllipsePath = new Ellipse(4, 4);

    Quaternion fixedRotation;

    private void Start()
    {
        if (ObjectToOrbit == null)
        {
            ObjectToOrbit = GetComponentInChildren<LockRotation>().gameObject.transform;
        }

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
