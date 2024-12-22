using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Image gauge;

    private float totalTime = 10;
    private float currentTime = 0;

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        currentTime += Time.deltaTime;
        gauge.fillAmount = 1 - currentTime / totalTime;

        if (currentTime >= totalTime)
        {
            currentTime = 0;
            ChangetoEndingScene();
        }
    }

    void ChangetoEndingScene()
    {
        SceneManager.LoadScene("EndingPage");
    }
}
