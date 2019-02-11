using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour {

    [SerializeField] private Image FlashImage;
    [SerializeField] private Color32 FlashColor;

    [Range(0, 255f)]
    [SerializeField] private byte FlashIntensity;
    [Range(1, 20)]
    [SerializeField] private byte FlashReductionTime;
    private byte currentIntensity;

    void Start ()
    {
        PlayerHealth.PlayerDamaged += SetFlashIntensity;
	}
	
	void Update () {

        if (currentIntensity > 20)
        {
            ReduceAlpha();
        }
	}

    private void ReduceAlpha()
    {
        currentIntensity -= FlashReductionTime;
        FlashImage.color = FlashColor;
        FlashColor.a = currentIntensity;
        if (currentIntensity <= 20)
        {
            currentIntensity = 0;
            FlashColor.a = 0;
            FlashImage.color = FlashColor;
        }
    }

    private void SetFlashIntensity()
    {
        currentIntensity = FlashIntensity;
    }
}
