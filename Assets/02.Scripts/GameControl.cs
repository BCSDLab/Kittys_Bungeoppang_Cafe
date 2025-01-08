using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject guidePanel;
    [SerializeField] Text textBGM;
    [SerializeField] GameObject cookingSystem;
    [SerializeField] GameObject cookingTool;
    [SerializeField] GameObject cat;

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
        if (isFirst)
        {
            if (guidePanel != null)
            {
                guidePanel.SetActive(true);
                isFirst = false;
            }

            if(cookingSystem!=null && cookingTool != null && cat != null)
            {
                cookingSystem.SetActive(false);
                cookingTool.SetActive(false);
                cat.SetActive(false);
            }
        }
        else
        {
            if (cookingSystem != null && cookingTool != null && cat != null)
            {
                cookingSystem.SetActive(true);
                cookingTool.SetActive(true);
                cat.SetActive(true);
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
