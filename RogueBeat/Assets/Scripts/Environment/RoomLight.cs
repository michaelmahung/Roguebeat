using UnityEngine;

public class RoomLight : MonoBehaviour {

    [SerializeField] private Color ceilingColorFull = Color.green;
    [SerializeField] private Color ceilingColorClear = new Color(0,0,0,0);
    private MeshRenderer lightMesh;

    void Start () {
        lightMesh = GetComponent<MeshRenderer>();
	}

    public void ToggleLight(bool state)
    {
        if (state)
        {
            lightMesh.material.color = ceilingColorClear;
        } else
        {
            lightMesh.material.color = ceilingColorFull;
        }
    }
}
