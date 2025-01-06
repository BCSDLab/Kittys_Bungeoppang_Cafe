using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject guidePanel;
    [SerializeField] Text textBGM;
    static bool isFirst = true;
    bool isBGMPlaying = true;

    void Start()
    {
        if (isFirst)
        {
            if (guidePanel != null)
            {
                guidePanel.SetActive(true);
                isFirst = false;
            }
        }
    }

    public void BGMOnOff()
    {
        if (isBGMPlaying)
        {
            AudioManager.audioManager.StopBGM();
            isBGMPlaying = false;
            textBGM.text = "BGM ÄÑ±â";
        }
        else
        {
            AudioManager.audioManager.PlayBGM("PlayBGM");
            isBGMPlaying = true;
            textBGM.text = "BGM ²ô±â";
        }
    }

    public void ButtonSFX()
    {
        AudioManager.audioManager.PlaySFX("ButtonSFX");
    }
}
