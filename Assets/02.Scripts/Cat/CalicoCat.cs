using UnityEngine;

public class CalicoCat : MainCat
{
    protected override void HandleIncomingEvent(EventData eventData)
    {
        base.HandleIncomingEvent(eventData); // GrayCat의 기본 동작 수행

        // 삼색냥이 특성 구현
        int quantity = Random.Range(1, 101) <= 70 ? 3 : 1;
        Debug.Log($"삼색냥이가 초코 붕어빵 {quantity}개를 구매했습니다!");
    }
}