using UnityEngine;

//Contains logic for creating a visual respresentation of the orbit of the Ellipse

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour {

    LineRenderer lr;
    [Range(3, 36)]
    [SerializeField] private int segments = 24;
    [SerializeField] private Ellipse ellipse = new Ellipse(4, 4);
    Quaternion fixedRotation;

    public void Awake()
    {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();
        fixedRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = fixedRotation;
    }

    void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i ++)
        {
            Vector2 position2D = ellipse.Evaluate((float)i / (float)segments);
            points[i] = new Vector3(position2D.x, position2D.y, 0f);
        }

        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    private void OnValidate()
    {
        if (Application.isPlaying && lr != null)
        {
            CalculateEllipse();
        }

    }
}
