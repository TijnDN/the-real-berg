using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Settings")]
    public bool countUp = true;
    public float startTime = 0f;

    private float currentTime;
    private bool isRunning = true;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (!isRunning) return;

        if (countUp)
            currentTime += Time.deltaTime;
        else
            currentTime -= Time.deltaTime;

        if (currentTime < 0f)
            currentTime = 0f;

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer() => isRunning = true;
    public void StopTimer() => isRunning = false;

    public void ResetTimer()
    {
        currentTime = startTime;
        UpdateTimerDisplay();
    }
}