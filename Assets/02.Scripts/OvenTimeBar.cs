using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OvenTimeBar : MonoBehaviour
{
    [SerializeField] private GameObject timeBar;
    [SerializeField] private Image currentGauge;
    [SerializeField] private OvenController ovenController;

    private float totalTime;
    private float currentTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        totalTime = ovenController.cookingTime;
    }

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
    }
}