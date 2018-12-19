using UnityEngine;

public class PlayerSFX : MonoBehaviour {

    public AudioClip HurtClip;

	void Start () {
        PlayerHealth.PlayerDamaged += PlaySFX;
	}
	
    private void PlaySFX()
    {
        GameManager.Instance.PlaySFX(GameManager.Instance.PlayerAudio, HurtClip);
    }
}
