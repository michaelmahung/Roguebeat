using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCode : MonoBehaviour {

	[SerializeField]
	private Image foregroundImage;
	[SerializeField]
	//private float updateSpeedSeconds = 0.2f;


/* private void Awake()
{
GetComponentInParent<AI>().OnHealthPctChanged += HandleHealthChanged;

}
*/

/*private void HandleHealthChanged(float pct)
{
StartCoroutine(ChangeToPct(pct));

}
*/

/*private IEnumerator ChangeToPct(float pct)
{
	float preChangePct = foregroundImage.fillAmount;
	float elapsed = 0f;

	while (elapsed < updateSpeedSeconds)
	{
		elapsed += Time.deltaTime;
		foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);	
		yield return null;
		}

		foregroundImage.fillAmount = pct;
}
*/

public void HealthChange(float percentage){
	foregroundImage.fillAmount = percentage;
}

private void LateUpdate()
{
//transform.LookAt(Camera.main.transform);
//transform.Rotate(0, 0, -90);
}
}
