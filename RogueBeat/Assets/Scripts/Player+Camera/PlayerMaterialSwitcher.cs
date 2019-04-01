using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSwitcher : MonoBehaviour {

    Renderer playerRenderer;
    private Material[] playerMaterials;
    int matValue;

    void Start ()
    {
        playerRenderer = GetComponent<Renderer>();
        playerMaterials = Resources.LoadAll<Material>("Materials");
        matValue = 0;
    }
	

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (matValue < playerMaterials.Length - 1)
            {
                matValue++;
            }
            else
            {
                matValue = 0;
            }
            ChangeMaterial(playerRenderer, playerMaterials[matValue]);
        }
    }

    public void ChangeMaterial(Renderer renderer, Material mat)
    {
        renderer.material = mat;
    }
}
