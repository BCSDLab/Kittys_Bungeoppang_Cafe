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

    private List<DialogData> dialogs; // 대화 데이터를 리스트로 변경
    [SerializeField]
    private int branch; // 전달받은 branch 값

    private int currentSpeakerIndex = 0;
    private float typingSpeed = 0.1f;
    private bool isTypingEffect = false;

    public void ReceiveRandomValue(int value)
    {
        branch = value;
        Debug.Log($"DialogSystem received random value: {branch}");
        LoadDialogs(); // 랜덤 값을 기반으로 대화 데이터 로드
    }

    private void LoadDialogs()
    {
        // branch에 따른 대화 데이터 필터링
        var filteredDialogs = dialogDB.Sheet1.FindAll(d => d.branch == branch);
        dialogs = new List<DialogData>();

        foreach (var dialog in filteredDialogs)
        {
            dialogs.Add(new DialogData
            {
                name = dialog.name,
                dialogue = dialog.dialog,
                speakerIndex = dialog.speakerIndex
            });
        }

        if (dialogs.Count == 0)
        {
            Debug.LogWarning("No dialogs found for the given branch value.");
        }
    }

    public bool UpdateDialog()
    {
        if (dialogs == null || dialogs.Count == 0)
        {
            Debug.LogError("Dialog data is not loaded or empty. Call ReceiveRandomValue first.");
            return false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isTypingEffect)
            {
                // 타이핑 중일 때는 입력을 무시
                return false;
            }

            // 타이핑이 끝난 후 새로운 랜덤 대화를 표시
            ShowRandomDialog();
            return false;
        }

        // 대화가 모두 끝났다면 true 반환
        if (dialogs.Count == 0)
        {
            Debug.Log("All dialogs have been displayed.");
            foreach (var speaker in speakers)
            {
                SetActiveObjects(speaker, false);
            }
            return true;
        }

        return false;
    }

    private void ShowRandomDialog()
    {
        if (dialogs.Count == 0)
        {
            Debug.LogWarning("No dialogs to display.");
            return;
        }

        // 랜덤으로 대화 선택
        int randomIndex = UnityEngine.Random.Range(0, dialogs.Count);
        var randomDialog = dialogs[randomIndex];

        // 화자 인덱스 설정 및 검증
        currentSpeakerIndex = randomDialog.speakerIndex;
        if (currentSpeakerIndex < 0 || currentSpeakerIndex >= speakers.Length)
        {
            Debug.LogError($"Speaker index out of bounds. Current: {currentSpeakerIndex}, Max: {speakers.Length - 1}");
            return;
        }

        SetActiveObjects(speakers[currentSpeakerIndex], true);
        speakers[currentSpeakerIndex].textName.text = randomDialog.name;

        // 텍스트 타이핑 효과 시작
        StartCoroutine(OnTypingText(randomDialog.dialogue));
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialogue.gameObject.SetActive(visible);
        speaker.objectArrow.SetActive(false);
    }

    private IEnumerator OnTypingText(string dialogue)
    {
        int index = 0;
        isTypingEffect = true;

        speakers[currentSpeakerIndex].textDialogue.text = string.Empty;

        while (index < dialogue.Length)
        {
            speakers[currentSpeakerIndex].textDialogue.text = dialogue.Substring(0, index + 1);
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTypingEffect = false;
        speakers[currentSpeakerIndex].objectArrow.SetActive(true);
    }
}

[System.Serializable]
public struct Speaker
{
    public Image imageDialog;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
    public GameObject objectArrow;
}

[System.Serializable]
public struct DialogData
{
    public int speakerIndex; // 화자를 지정하는 필드
    public string name;      // 화자 이름
    [TextArea(3, 5)]
    public string dialogue;  // 대사 텍스트
}
