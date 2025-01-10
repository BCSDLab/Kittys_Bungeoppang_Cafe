using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject guidePanel;
    [SerializeField] Text textBGM;
    [SerializeField] GameObject cookingSystem;
    [SerializeField] GameObject cookingTool;
    [SerializeField] GameObject cat;
    [SerializeField] GameObject shadow;
    [SerializeField] GameObject trashcan;

    [SerializeField] TimeController timeController;

    static bool isFirst = true;
    bool isBGMPlaying = true;

    void Awake()
    {
    #if UNITY_STANDALONE
        Screen.SetResolution(540, 960, false);
        Screen.fullScreen = false;
    #endif
    }

    void Start()
    {
        // 게임 처음으로 하는 경우
        if (isFirst)
        {
            if (guidePanel != null)
            {
                guidePanel.SetActive(true);
                isFirst = false;
            }

            if(cookingSystem != null && cookingTool != null && cat != null && shadow != null)
            {
                cookingSystem.SetActive(false);
                cookingTool.SetActive(false);
                cat.SetActive(false);
                shadow.SetActive(false);
                trashcan.SetActive(false);
            }
        }
        // 게임 다시 하는 경우
        else
        {
            if (cookingSystem != null && cookingTool != null && cat != null)
            {
                timeController.isGameStart = true;
                cookingSystem.SetActive(true);
                cookingTool.SetActive(true);
                cat.SetActive(true);
                shadow.SetActive(true);
                trashcan.SetActive(true);
            }
        }
    }

    public void BGMOnOff()
    {
        if (isBGMPlaying)
        {
            AudioManager.audioManager.StopBGM();
            isBGMPlaying = false;
            textBGM.text = "BGM On";
        }
        else
        {
            AudioManager.audioManager.PlayBGM("PlayBGM");
            isBGMPlaying = true;
            textBGM.text = "BGM Off";
        }
    }

    public void ButtonSFX()
    {
        AudioManager.audioManager.PlaySFX("ButtonSFX");
    }
}
