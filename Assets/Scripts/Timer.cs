using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    private float countdownDuration = 5f;
    [SerializeField] private AnimationCurve scaleCurve;

    private float currentTime;
    private Car_Controller carController;

    void Start()
    {
        StartCoroutine(StartCountdown());
        carController = GameObject.FindWithTag("Player").GetComponent<Car_Controller>();
    }

    IEnumerator StartCountdown()
    {
        int countdownValue = Mathf.CeilToInt(countdownDuration);
        countdownText.text = countdownValue.ToString();

        while (countdownValue >= 0)
        {
            countdownText.text = countdownValue.ToString();

            if (countdownValue == 0)
                countdownText.text = "GO!";
            countdownValue--;
            yield return ScaleDownText();
        }
        countdownText.text = "";
        carController.SetStart(true);
    }

    IEnumerator ScaleDownText()
    {
        Vector3 initialScale = countdownText.transform.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {

            elapsedTime += Time.fixedDeltaTime;
            float scaleValue = scaleCurve.Evaluate(elapsedTime);
            countdownText.transform.localScale = initialScale * scaleValue;
            yield return new WaitForFixedUpdate();
        }

        countdownText.transform.localScale = initialScale;
    }
}
