using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [SerializeField] private Image gauge;

    [SerializeField] private float gamePlayTime = 300;
    
    [SerializeField] private ResourceManager resourceManager;
    
    private float currentTime = 0;

    public bool isGameStart = false;

    void Update()
    {
        if (isGameStart)
        {
            Timer();
        }
    }

    public void GameStart()
    {
        isGameStart = true;
    }

    void Timer()
    {
        currentTime += Time.deltaTime;
        gauge.fillAmount = 1 - currentTime / gamePlayTime;

        if (currentTime >= gamePlayTime)
        {
            currentTime = 0;
            if (resourceManager.coinValue > 10000 && resourceManager.fameValue > 100)
            {
                SceneManager.LoadScene("Ending_Best_Scene");
            }else if (resourceManager.coinValue > 10000 && resourceManager.fameValue > 30)
            {
                SceneManager.LoadScene("Ending_Coin_Scene");
            }else if (resourceManager.coinValue > 3000 && resourceManager.fameValue > 100)
            {
                SceneManager.LoadScene("Ending_Fame_Scene");
            }else 
            {
                SceneManager.LoadScene("Ending_Bad_Scene");
            }
        }
    }

    void ChangetoEndingScene()
    {
        SceneManager.LoadScene("Ending_Best_Scene");
    }
}
