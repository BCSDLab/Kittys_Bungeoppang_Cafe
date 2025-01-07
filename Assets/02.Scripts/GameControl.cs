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
            textBGM.text = "BGM �ѱ�";
        }
        else
        {
            AudioManager.audioManager.PlayBGM("PlayBGM");
            isBGMPlaying = true;
            textBGM.text = "BGM ����";
        }
    }

    public void ButtonSFX()
    {
        AudioManager.audioManager.PlaySFX("ButtonSFX");
    }
}
