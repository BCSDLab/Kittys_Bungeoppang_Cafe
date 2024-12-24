using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThiefEvent : MonoBehaviour
{
    public Image blackoutImage;  // 화면 어두운 효과를 위한 이미지
    public float fadeDuration = 2f;  // 페이드 시간

    // 정전 이벤트 발생 시 화면 어두워지기
    public void TriggerBlackout()
    {
        StartCoroutine(FadeBlackout());
    }

    private IEnumerator FadeBlackout()
    {
        float elapsedTime = 0f;

        // 화면 어두워지기 시작
        blackoutImage.gameObject.SetActive(true);
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);  // 0에서 1까지 서서히 증가
            blackoutImage.color = new Color(0f, 0f, 0f, alpha);  // Alpha 값 조정하여 페이드 효과
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackoutImage.color = new Color(0f, 0f, 0f, 1f); // 완전히 어두운 상태

        // 잠시 후 밝아지도록 설정
        yield return new WaitForSeconds(1f);

        // 화면을 밝게 되도록
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // 1에서 0까지 서서히 감소
            blackoutImage.color = new Color(0f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackoutImage.color = new Color(0f, 0f, 0f, 0f);
        blackoutImage.gameObject.SetActive(false);  // 화면 복원
    }
}
