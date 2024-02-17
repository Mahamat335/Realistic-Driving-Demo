using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text chronometer;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject highScoreImage;
    private float countdownDuration = 5f;
    [SerializeField] private AnimationCurve scaleCurve;

    private float currentTime;
    private Car_Controller carController;
    private bool timerRunning = false;
    private High_Score highScoreScript;
    void Start()
    {
        StartCoroutine(StartCountdown());
        carController = GameObject.FindWithTag("Player").GetComponent<Car_Controller>();
        highScoreScript = GameObject.Find("EventSystem").GetComponent<High_Score>();
    }

    private IEnumerator StartCountdown()
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
        timerRunning = true;
        StartCoroutine(ChronometerCoroutine());
    }

    private IEnumerator ScaleDownText()
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
    private IEnumerator ChronometerCoroutine()
    {
        while (timerRunning)
        {
            currentTime += Time.fixedDeltaTime;
            UpdateChronometerText();
            if (carController.transform.position.x < 1000)
                yield return new WaitForFixedUpdate();
            else
                timerRunning = false;
        }

        if (highScoreScript.GetHighScore() == 0f || highScoreScript.GetHighScore() > currentTime)
        {
            highScoreScript.SetHighScore(currentTime);
            highScoreImage.SetActive(true);
        }
        StartCoroutine(carController.EndGame());

        // End Game Animations
        RectTransform rectTransform = chronometer.GetComponent<RectTransform>();
        float elapsedTime = 0f, duration = 0.75f;
        Vector2 targetPosition = new Vector2(0f, 125f);
        Color panelColor = chronometer.transform.parent.GetComponent<Image>().color;
        while (panelColor.a != 0.4f)
        {
            elapsedTime += Time.fixedDeltaTime;
            float t = elapsedTime / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, t);
            panelColor.a = Mathf.Lerp(panelColor.a, 0.4f, t);
            chronometer.transform.parent.GetComponent<Image>().color = panelColor;
            yield return new WaitForFixedUpdate();
        }
        endGameUI.SetActive(true);
    }

    private void UpdateChronometerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((int)(currentTime * 1000f) % 1000f);

        string timerText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        chronometer.text = timerText;
    }
}
