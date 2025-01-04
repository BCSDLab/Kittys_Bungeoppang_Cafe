using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private DialogSystem dialogSystem01;

    [SerializeField] private MainCat mainCat; // MainCat 클래스 참조 추가

    private bool isTouchActive = true; // 터치 입력 활성화 플래그

    public void ReceiveValueFromMainCat(int branch)
    {
        isTouchActive = true;
        // MainCat에서 전달받은 branch 값을 DialogSystem으로 전달
        if (dialogSystem01 != null)
        {
            dialogSystem01.ReceiveBranchValue(branch);
            Debug.Log($"DialogManager forwarded branch value: {branch}");
        }
        else
        {
            Debug.LogError("DialogSystem reference is null in DialogManager.");
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        // MainCat의 상태 확인
        if (mainCat != null && mainCat.IsAtIntermediatePosition() && isTouchActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTouchActive = false;
                // 랜덤 숫자 생성 (1~150)
                int randomValue = UnityEngine.Random.Range(1, 151);

                // 랜덤 숫자를 DialogSystem에 전달
                if (dialogSystem01 != null)
                {
                    dialogSystem01.ReceiveBranchValue(randomValue);
                    Debug.Log($"DialogManager passed random value {randomValue} to DialogSystem.");
                }
                else
                {
                    Debug.LogError("DialogSystem reference is null.");
                }

                // 랜덤 숫자를 30으로 나눈 값을 MainCat에 전달
                if (mainCat != null)
                {
                    int dividedValue = randomValue / 30;
                    mainCat.ReceiveDividedValue(dividedValue);
                    Debug.Log($"DialogManager passed divided value {dividedValue} to MainCat.");
                }
                else
                {
                    Debug.LogError("MainCat reference is null.");
                }

                // 대화 진행, 종료 시 루프 탈출
                if (dialogSystem01 != null)
                {
                    bool isDialogComplete = dialogSystem01.UpdateDialog();
                }
            }

        }
    }
}