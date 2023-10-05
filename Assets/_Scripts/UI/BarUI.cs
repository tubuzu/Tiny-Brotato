using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MyMonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] float elapsedTime = 0.1f;

    public void ChangeBarValue(float value)
    {
        StopAllCoroutines();
        StartCoroutine(BarValueChangeCoroutine(value));
    }

    IEnumerator BarValueChangeCoroutine(float value)
    {
        float hehe = 0f;
        while (hehe < elapsedTime)
        {
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, value, hehe / elapsedTime);
            hehe += Time.deltaTime;
            yield return null;
        }
        bar.fillAmount = value;
    }
}