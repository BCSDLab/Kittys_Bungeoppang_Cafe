using UnityEngine;

public class ThiefCat : MainCat
{
    protected override void HandleIncomingEvent(EventData eventData)
    {
        base.HandleIncomingEvent(eventData); // GrayCat의 기본 동작 수행

        // 도둑냥이 특성 구현
        int action = Random.Range(1, 101);
        if (action <= 40)
        {
            Debug.Log("도둑냥이가 고양이 순서를 바꿨습니다! 정전 발생!");
        }
        else if (action <= 80)
        {
            Debug.Log("도둑냥이가 필링을 훔쳤습니다! 정전 발생!");
        }
        else
        {
            Debug.Log("도둑냥이가 고추 붕어빵을 사 먹었습니다!");
        }
    }
}