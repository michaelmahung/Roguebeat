using UnityEngine;

public class PlayerSFX : MonoBehaviour {

	void Start () {
        PlayerHealth.PlayerDamaged += PlaySFX;
	}
	
    private void PlaySFX()
    {
        AudioManager.Instance.PlaySound("PlayerHurt");
    }
}
