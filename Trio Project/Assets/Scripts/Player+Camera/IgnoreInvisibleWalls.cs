using UnityEngine;

public class IgnoreInvisibleWalls : MonoBehaviour {

	void Start ()
    {
        Physics.IgnoreLayerCollision(8, 10, true);
	}

    /* Layer 0 = Default
     * Layer 1 = TransparentFX
     * Layer 2 = Ignore Raycast
     * Layer 3 = Empty
     * Layer 4 = Water
     * Layer 5 = UI
     * Layer 6 = Empty
     * Layer 6 = Empty
     * Layer 8 = Invisible Wall
     * Layer 9 = Floor
     * Layer 10 = Player
     * */
}
