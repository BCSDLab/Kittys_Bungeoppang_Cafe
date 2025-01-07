using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [TextArea(3, 10)]
    public string fullText;
    public float typingDelay = 0.1f;

    private string currentText = "";
    [SerializeField] private TMP_Text uiTMPText;
    [SerializeField] private Text uiText;

    void Start()
    {
        if (uiTMPText)
        {
            StartCoroutine(ShowTMPText());
        }
        else if (uiText)
        {
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowTMPText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiTMPText.text = currentText;
            yield return new WaitForSeconds(typingDelay);
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(typingDelay);
        }
    }
}
