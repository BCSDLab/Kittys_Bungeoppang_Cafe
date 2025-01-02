using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventData
{
    public Vector3 intermediatePosition; // 중간 위치
    public Vector3 finalPosition;       // 최종 위치
    public string message;              // 이벤트와 함께 전달할 메시지
    public int prefabIndex;             // 불러올 프리팹의 인덱스 (1~5)
}

[System.Serializable]
public class ResponseEvent : UnityEvent<EventData> { }

public class MainCat : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private float slideSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    [SerializeField] private float bobSpeed = 3f;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] prefabs; // 1~5에 해당하는 프리팹

    [Header("Branch Configuration")]
    [SerializeField] private int branch; // DialogSystem에서 전달받은 branch 값

    // 출력 이벤트: 처리가 완료된 정보를 외부로 전달
    public ResponseEvent OnEventProcessed;

    private EventData currentEventData;
    private State currentState = State.Start;

    private enum State
    {
        Start,
        Intermediate,
        Final
    }

    public void ReceiveDividedValue(int value)
    {
        branch = value;
        Debug.Log($"MainCat received divided value: {branch}");
        // 나눈 값에 기반한 추가 처리 로직 작성
    }
    
    private void Start()
    {
        transform.position = startPoint.position;

        if (OnEventProcessed == null)
            OnEventProcessed = new ResponseEvent();

        HandleBranchLogic();
    }

    private void HandleBranchLogic()
    {
        int prefabIndex = (branch % 5) + 1;
        Vector3 intermediatePosition = new Vector3(branch, 0, branch % 10);
        Vector3 finalPosition = new Vector3(branch * 2, 0, branch % 15);
        string message = $"Branch {branch} processed with prefab index {prefabIndex}.";

        currentEventData = new EventData
        {
            intermediatePosition = intermediatePosition,
            finalPosition = finalPosition,
            message = message,
            prefabIndex = prefabIndex
        };

        HandleIncomingEvent(currentEventData);
    }

    protected virtual void HandleIncomingEvent(EventData eventData)
    {
        Debug.Log($"Received Event: {eventData.message}");

        if (eventData.prefabIndex >= 1 && eventData.prefabIndex <= 5)
        {
            int index = eventData.prefabIndex - 1;
            if (prefabs[index] != null)
            {
                GameObject instantiatedPrefab = Instantiate(prefabs[index], transform.position, Quaternion.identity, transform);
                Debug.Log($"Prefab {eventData.prefabIndex} instantiated as child of {gameObject.name}.");
            }
            else
            {
                Debug.LogWarning($"Prefab {eventData.prefabIndex} is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("Prefab index out of range (1~5).");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentState == State.Start)
            {
                StartCoroutine(SlideSequence(currentEventData.intermediatePosition));
                currentState = State.Intermediate;
            }
            else if (currentState == State.Intermediate)
            {
                StartCoroutine(SlideSequence(currentEventData.finalPosition));
                currentState = State.Final;
            }
        }
    }

    private IEnumerator SlideSequence(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float elapsedTime = 0;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            float step = elapsedTime * slideSpeed / distance;
            transform.position = Vector3.Lerp(startPosition, targetPosition, step);

            float bobOffset = Mathf.Sin(elapsedTime * bobSpeed) * bobHeight;
            transform.position += new Vector3(0, bobOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
