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

    public void ReceiveBranchValue(int value)
    {
        branch = value;
        Debug.Log($"Received branch value: {branch}");
        ProcessDialogs();
    }

    private void ProcessDialogs()
    {
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
            Debug.LogWarning("No dialogs found for the given branch value.");
        }
    }

    private void ActivateChatUI()
    {
        if (chatUI == null)
        {
            Debug.LogError("Chat UI GameObject is not assigned.");
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
            DeactivateChatUI();
            Debug.Log("All dialogs have been displayed.");
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
            Debug.LogError($"Speaker index out of bounds: {dialog.speakerIndex}");
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
