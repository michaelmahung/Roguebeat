using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldObjScript : MonoBehaviour {

    bool grow = true;

	// Use this for initialization
	void Start () {
       // Invoke("ChangeSize", .5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(grow == true)
        {
            transform.localScale = new Vector3(0, 1, 1.885f);
           grow = false;
        }
	}

    void ChangeSize()
    {
        //gameObject.transform.localScale = new Vector3(.0938f, 2, 3.77f);
    }
}
