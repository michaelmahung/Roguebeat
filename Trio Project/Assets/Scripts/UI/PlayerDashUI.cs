using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashUI : MonoBehaviour {

    [SerializeField] Color barColor;
    [SerializeField] Image dashBarImage;
    private Slider dashBarSlider;

    private void Start()
    {
        dashBarSlider = GetComponent<Slider>();
    }

    public void SetPercentage(float value)
    {
        if (dashBarSlider != null)
        {
            dashBarSlider.normalizedValue = value;
            SetBarAlpha();
            return;
        }

        Debug.Log("No Dash Bar Slider Assigned");
    }

    private void SetBarAlpha()
    {
        if (dashBarSlider.normalizedValue >= 1f)
        {
            dashBarImage.color = new Color(barColor.r, barColor.g, barColor.b, 1);
        }

        if (dashBarSlider.normalizedValue >= 0.7f && dashBarSlider.normalizedValue <= 0.99f)
        {
            dashBarImage.color = new Color(barColor.r, barColor.g, barColor.b, .5f);
        }

        if (dashBarSlider.normalizedValue < .7f && dashBarSlider.normalizedValue >= .4f)
        {
            dashBarImage.color = new Color(barColor.r, barColor.g, barColor.b, .25f);
        }

        if (dashBarSlider.normalizedValue < 0.4f)
        {
            dashBarImage.color = new Color(barColor.r, barColor.g, barColor.b, .1f);
        }
    }
}
