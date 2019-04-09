using UnityEngine;

//We probably want to switch most physics-based collision to work off of layers. 
//This will potentially significantly reduce the amount of collisions occuring at any given time
//and will reduce the strain on the CPU.

public class IgnoreLayers : MonoBehaviour {

	void Start ()
    {
        Physics.IgnoreLayerCollision(8, 10, true);
        Physics.IgnoreLayerCollision(0, 11, true);
        Physics.IgnoreLayerCollision(0, 13, true);
	}

    /* Layer 0 = Default
     * Layer 1 = TransparentFX
     * Layer 2 = Ignore Raycast
     * Layer 3 = Empty
     * Layer 4 = Water
     * Layer 5 = UI
     * Layer 6 = Empty
     * Layer 7 = Empty
     * Layer 8 = Invisible Wall
     * Layer 9 = Floor
     * Layer 10 = Player
     * Layer 11 = FogLayer
     * Layer 12 = Shield
     * Layer 13 = Spawn Point    
     * */
}
