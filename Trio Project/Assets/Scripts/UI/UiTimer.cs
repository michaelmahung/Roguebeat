using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTimer : MonoBehaviour
{
    [SerializeField] bool runTimer;
    [SerializeField] Text timerText;
    int totalTime;

    float incrementTimer;

    void Update()
    {
        if (runTimer)
        {
            if (incrementTimer < 1)
            {
                incrementTimer += Time.deltaTime;
            }
            else
            {
                totalTime++;
                incrementTimer = 0;
                SetText();
            }
        }
    }

    void SetText()
    {
        timerText.text = totalTime.ToString();
    }
}
