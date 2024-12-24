using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] 
    private DialogSystem dialogSystem01;
    [SerializeField] 
    private DialogSystem dialogSystem02;
    [SerializeField] 
    private TextMeshProUGUI textCountdown;

    private IEnumerator Start()
    {
        textCountdown.gameObject.SetActive(false);

        yield return WaitForDialog(dialogSystem01);
        
        textCountdown.gameObject.SetActive(true);
        int count = 5;
        while (count > 0)
        {
            textCountdown.text = count.ToString();
            count--;

            yield return new WaitForSeconds(1);
        }
        textCountdown.gameObject.SetActive(false);

        yield return WaitForDialog(dialogSystem02);
        
        textCountdown.gameObject.SetActive(true);
        textCountdown.text = "The End";

        yield return new WaitForSeconds(2);

        UnityEditor.EditorApplication.ExitPlaymode();
    }

    private IEnumerator WaitForDialog(DialogSystem dialogSystem)
    {
        while (true)
        {
            try
            {
                // dialogSystem의 UpdateDialog를 안전하게 호출
                if (dialogSystem.UpdateDialog())
                {
                    yield break; // 대화가 종료되면 루프 탈출
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.LogError($"DialogSystem encountered an error: {e.Message}");
                yield break; // 문제 발생 시 루프 탈출
            }

            yield return null; // 다음 프레임까지 대기
        }
    }
}