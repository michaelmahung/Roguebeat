using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRoomManager : MonoBehaviour {
	public GameObject[] Rooms;
	public Mesh[] RoomMeshes;
	/*private Vector3[] Room1Verts;
	private Vector3[] Room2Verts;
	private Vector3[] Room3Verts;
	private Vector3[] Room4Verts;
	private Vector3[] Room5Verts;
	private Vector3[] Room6Verts;
	*/

	/*private Color[] Room1Colors;
	private Color[] Room2Colors;
	private Color[] Room3Colors;
	private Color[] Room4Colors;
	private Color[] Room5Colors;
	private Color[] Room6Colors;
	*/

	public Color[][] RoomColors;
	public Vector3[][] RoomVectors;



	// Use this for initialization
	void Start () {
		Initialize();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha7)){
			for (int i=0; i< RoomVectors[6].Length; i++){
RoomColors[6][i].a = 0.1f;
		}
		}
		if(Input.GetKeyDown(KeyCode.T)){
				print("getting to this point");
			Rooms[5].GetComponent<MeshRenderer>().material.color = Color.clear;
			print("we got here");
		}
	}

	void Initialize(){

/* for(int i = 0; i < Rooms.Length; i++){
RoomMeshes[i] = Rooms[i].GetComponent<MeshFilter>().mesh;
}

for(int i = 0; i < RoomMeshes.Length; i++){
RoomVectors[i] = RoomMeshes[i].vertices;
}


for(int i = 0; i < RoomColors.Length; i++){
RoomColors[i] = new Color[RoomVectors[i].Length];
}
*/
UpdateColors();
	}

	 void UpdateColors(){
/* for(int i = 0; i < RoomMeshes.Length; i++){
	RoomMeshes[i].colors = RoomColors[i];
}
*/


	}
}
