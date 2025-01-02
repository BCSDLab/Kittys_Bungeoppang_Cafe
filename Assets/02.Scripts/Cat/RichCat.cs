using UnityEngine;

public class RichCat : MainCat
{
    protected override void HandleIncomingEvent(EventData eventData)
    {
        base.HandleIncomingEvent(eventData); // GrayCat의 기본 동작 수행

        // 부자냥이 특성 구현
        Debug.Log("부자냥이가 2배 많은 돈을 지급했습니다!");
        Debug.Log("평점이 크게 올랐습니다!");
    }
}