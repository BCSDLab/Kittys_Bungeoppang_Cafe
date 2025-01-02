using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Image gauge;

    [SerializeField] private float gamePlayTime = 300;
    private float currentTime = 0;

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        currentTime += Time.deltaTime;
        gauge.fillAmount = 1 - currentTime / gamePlayTime;

        if (currentTime >= gamePlayTime)
        {
            currentTime = 0;
            ChangetoEndingScene();
        }
    }

    void ChangetoEndingScene()
    {
        SceneManager.LoadScene("Ending_Best_Scene");
    }
}
