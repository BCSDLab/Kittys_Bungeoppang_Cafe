using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MixerTimeBar : MonoBehaviour
{
    [SerializeField] private GameObject timeBar;
    [SerializeField] private Image currentGauge;

    private float totalTime = 5f;
    private float currentTime = 0f;
    private bool isRunning = false;

    void Update()
    {
        if (isRunning)
        {
            Timer();
        }    
    }

    void Timer()
    {
        if (currentGauge != null)
        {
            currentTime += Time.deltaTime;
            currentGauge.fillAmount = 1 - (currentTime / totalTime);

            if (currentTime >= totalTime)
            {
                OnTimerComplete();
            }
        }
    }

    public void StartTimer()
    {
        timeBar.SetActive(true);
        isRunning = true;
        currentGauge.fillAmount = 1f;
        currentTime = 0f;
    }

    private void OnTimerComplete()
    {
        timeBar.SetActive(false);
        isRunning = false;
        Debug.Log("타이머 완료!");
    }
}
