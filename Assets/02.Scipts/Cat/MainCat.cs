using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventData
{
    public Vector3 intermediatePosition; // 중간 위치
    public Vector3 finalPosition;       // 최종 위치
    public string message;              // 이벤트와 함께 전달할 메시지
}

[System.Serializable]
public class ResponseEvent : UnityEvent<EventData> { }

public class MainCat : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private float slideSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    [SerializeField] private float bobSpeed = 3f;

    // 입력 이벤트: 외부에서 데이터를 받아옴
    public UnityEvent<EventData> OnEventReceived;

    // 출력 이벤트: 처리가 완료된 정보를 외부로 전달
    public ResponseEvent OnEventProcessed;
    
    private void Start()
    {
        // 오브젝트를 시작 위치로 이동
        transform.position = startPoint.position;

        // 이벤트 리스너 등록
        if (OnEventReceived == null)
            OnEventReceived = new UnityEvent<EventData>();

        if (OnEventProcessed == null)
            OnEventProcessed = new ResponseEvent();

        OnEventReceived.AddListener(HandleIncomingEvent);
        
        // 1부터 30까지의 숫자 중 랜덤한 숫자를 생성
        int randomNumber = Random.Range(1, 31);
    }

    protected virtual void HandleIncomingEvent(EventData eventData)
    {
        // 이벤트로 전달받은 데이터를 처리
        Debug.Log($"Received Event: {eventData.message}");
        StartCoroutine(SlideSequence(eventData));
    }

    private IEnumerator SlideSequence(EventData eventData)
    {
        // 중간 위치로 이동
        yield return StartCoroutine(SlideWithBobbing(eventData.intermediatePosition));
        Debug.Log("Reached Intermediate Position");

        // 최종 위치로 이동
        yield return StartCoroutine(SlideWithBobbing(eventData.finalPosition));
        Debug.Log("Reached Final Position");

        // 데이터 처리 완료 후 결과 반환
        EventData responseData = new EventData
        {
            intermediatePosition = eventData.intermediatePosition,
            finalPosition = eventData.finalPosition,
            message = $"Arrived at {eventData.finalPosition}"
        };

        OnEventProcessed.Invoke(responseData);
    }

    private IEnumerator SlideWithBobbing(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float elapsedTime = 0;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // 슬라이드 이동
            float step = elapsedTime * slideSpeed / distance;
            transform.position = Vector3.Lerp(startPosition, targetPosition, step);

            // 상하 움직임 추가
            float bobOffset = Mathf.Sin(elapsedTime * bobSpeed) * bobHeight;
            transform.position += new Vector3(0, bobOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치 보정
        transform.position = targetPosition;
    }
}
