using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private CatText dialogDB;
    [SerializeField]
    private Speaker[] speakers;
    [SerializeField]
    private GameObject chatUI;

    private List<DialogData> dialogs = new List<DialogData>();
    private int branch;
    private int currentSpeakerIndex = 0;
    private float typingSpeed = 0.1f;
    private bool isTypingEffect = false;

    private bool isActive = false;

    public float Endcount = 1f;
    public float Startcount = 1f;

    public void ReceiveBranchValue(int value)
    {
        branch = value;
        Debug.Log($"받은 branch 값: {branch}");

        if (branch == 151 || branch == 152)
        {
            Debug.Log("즉시 대화를 시작합니다.");
            ProcessDialogs();
            Debug.Log("1초 후 채팅 UI를 비활성화할 예정...");
            StartCoroutine(DeactivateChatWithDelay(Endcount));
        }
        else
        {
            Debug.Log("3초 후 대화를 시작합니다.");
            StartCoroutine(StartDialogAfterDelay(Startcount));
        }
    }

    private IEnumerator StartDialogAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProcessDialogs();
    }


    private void ProcessDialogs()
    {
        Debug.Log($"branch 값 {branch}에 대한 대사를 처리 중...");
        dialogs = dialogDB.Sheet1.FindAll(d => d.branch == branch)
                                 .ConvertAll(d => new DialogData
                                 {
                                     name = d.name,
                                     dialogue = d.dialog,
                                     speakerIndex = d.speakerIndex
                                 });

        if (dialogs.Count > 0)
        {
            ActivateChatUI();
            ShowDialog();
        }
        else
        {
            Debug.LogWarning($"branch 값 {branch}에 해당하는 대사가 없습니다.");
        }
    }

    private void ActivateChatUI()
    {
        if (chatUI == null)
        {
            Debug.LogError("Chat UI GameObject가 할당되지 않았습니다.");
            return;
        }

        chatUI.SetActive(true);
        isActive = true;
    }

    private void DeactivateChatUI()
    {
        if (chatUI != null)
        {
            chatUI.SetActive(false);
            isActive = false;
            Debug.Log("채팅 UI가 성공적으로 비활성화되었습니다.");
        }
        else
        {
            Debug.LogError("chatUI가 할당되지 않았습니다.");
        }
    }

    public bool UpdateDialog()
    {
        if (!isActive || isTypingEffect) return false;

        if (dialogs.Count > 0)
        {
            ShowDialog();
        }
        else
        {
            Debug.Log("모든 대사를 표시했습니다.");
            return true;
        }

        return false;
    }

    private void ShowDialog()
    {
        if (dialogs.Count == 0) return;

        var dialog = dialogs[0];
        dialogs.RemoveAt(0);

        if (dialog.speakerIndex < 0 || dialog.speakerIndex >= speakers.Length)
        {
            Debug.LogError($"Speaker index 범위 초과: {dialog.speakerIndex}");
            return;
        }

        currentSpeakerIndex = dialog.speakerIndex;
        SetActiveObjects(speakers[currentSpeakerIndex], true);
        speakers[currentSpeakerIndex].textName.text = dialog.name;

        StartCoroutine(OnTypingText(dialog.dialogue));
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);
    }

    private IEnumerator OnTypingText(string dialogue)
    {
        int index = 0;
        isTypingEffect = true;
        speakers[currentSpeakerIndex].textDialogue.text = string.Empty;

        while (index < dialogue.Length)
        {
            speakers[currentSpeakerIndex].textDialogue.text = dialogue.Substring(0, ++index);
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;
    }

    private IEnumerator DeactivateChatWithDelay(float delay)
    {
        Debug.Log($"{delay}초 후 채팅 UI를 비활성화합니다...");
        yield return new WaitForSeconds(delay);
        DeactivateChatUI();
    }
}

[System.Serializable]
public struct Speaker
{
    public Image imageDialog;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
}

[System.Serializable]
public struct DialogData
{
    public int speakerIndex; // 화자를 지정하는 필드
    public string name;      // 화자 이름
    [TextArea(3, 5)]
    public string dialogue;  // 대사 텍스트
}
