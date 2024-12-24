using UnityEngine;
using System.Collections;

public class CheeseCat : MainCat
{
    protected override void HandleIncomingEvent(EventData eventData)
    {
        base.HandleIncomingEvent(eventData); // GrayCat의 기본 동작 수행

        // 치즈냥이 특성 구현
        StartCoroutine(ShareTMI());
    }

    private IEnumerator ShareTMI()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }
}