using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarSam : MonoBehaviour {

	public GameObject m_fogOfWarPlane; // Gameobject that serves as the fog of war
	public Transform m_player; // the transform that will serve as a raycast point to read from camera to player.
	public Transform m_fogofwarTransform;
	public LayerMask m_fogLayer; // only layer that will be affected by the alpha change.
	public float m_radius = 20f; // radius of raycasthit around player, acts as "LOS"
	private float m_radiusSqr {get { return m_radius * m_radius ;}}

	private Mesh m_mesh;
	private Vector3[] m_vertices;
	public Color[] m_colors;

	// Use this for initialization
	void Start () {
		Initialize();
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		Ray r = new Ray(transform.position, m_player.position - transform.position); // cast a ray from the camera to the player
		RaycastHit hit; //calling a ray variable of hit type to detail what our raycast will hit
		if(Physics.Raycast(r, out hit, 2000, m_fogLayer, QueryTriggerInteraction.Collide))
		{
for (int i=0; i< m_vertices.Length; i++){

	Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
	float dist = Vector3.SqrMagnitude(v - hit.point);
	if(dist < m_radiusSqr){

		float alpha = Mathf.Min(m_colors[i].a, dist/m_radiusSqr);

		m_colors[i].a = alpha;
	}

}
/* FogRefreshTime += Time.deltaTime;
if(FogRefreshTime > 0.2f){
for(int i = 0; i< m_colors.Length; i++){
	Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
	float dist = Vector3.SqrMagnitude(v - hit.point);
	if(dist > m_radiusSqr){
	foreach(Color c in m_colors)
					{
						if(m_colors[i].a != 1.0f)
						{
							m_colors[i].a = 0.4f;
						}
						FogRefreshTime = 0.0f;
						
					}
					}
}
}

			/* if(Input.GetKey(KeyCode.K)){
				for(int i = 0; i< m_colors.Length; i++)
				{
					foreach(Color c in m_colors)
					{
						if(m_colors[i].a != 1.0f)
						{
							m_colors[i].a = 0.4f;
						}
					}
				}
			}
			*/
UpdateColor();
		}
	}

	void Initialize(){

m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh; // mesh filter on fog of war object
m_vertices = m_mesh.vertices; //the vertices that make up the mesh filter of the fog of war object
m_colors = new Color[m_vertices.Length]; // the color of the vertices that make up the mesh filter of the fog of war object
for (int i=0; i < m_colors.Length; i++){

	m_colors[i] = Color.black;
}
UpdateColor();
	}
void UpdateColor(){
	m_mesh.colors = m_colors;
}
	}

