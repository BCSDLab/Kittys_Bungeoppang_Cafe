using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] 
    private DialogSystem dialogSystem01;
    [SerializeField] 
    private TextMeshProUGUI textCountdown;

    [SerializeField] 
    private MainCat mainCat; // MainCat 클래스 참조 추가

    private bool isTouchActive = false; // 터치 입력 활성화 플래그

    private void Start()
    {
        textCountdown.gameObject.SetActive(false); // 카운트다운 UI 비활성화
        StartCoroutine(WaitForDialogOnTouch(dialogSystem01));
    }

    private void Update()
    {
        // 화면 터치 또는 클릭 입력 확인
        if (Input.GetMouseButtonDown(0)) // 모바일과 에디터에서 모두 동작 (0: 좌클릭 또는 터치)
        {
            isTouchActive = true;
        }
    }

    private IEnumerator WaitForDialogOnTouch(DialogSystem dialogSystem)
    {
        while (true)
        {
            try
            {
                if (isTouchActive)
                {
                    isTouchActive = false;

                    // 랜덤 숫자 생성 (1~150)
                    int randomValue = UnityEngine.Random.Range(1, 151);

                    // 랜덤 숫자를 DialogSystem에 전달
                    dialogSystem.ReceiveRandomValue(randomValue);

                    // 랜덤 숫자를 30으로 나눈 값을 MainCat에 전달
                    int dividedValue = randomValue / 30;
                    mainCat.ReceiveDividedValue(dividedValue);

                    // 대화 진행, 종료 시 루프 탈출
                    bool isDialogComplete = dialogSystem.UpdateDialog(); 
                    if (isDialogComplete)
                    {
                        yield break;
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.LogError($"DialogSystem encountered an error: {e.Message}");
                yield break; // 문제 발생 시 루프 탈출
            }

            yield return null;
        }
    }
}