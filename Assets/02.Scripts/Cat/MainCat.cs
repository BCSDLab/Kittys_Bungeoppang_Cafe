using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventData
{
    public Transform intermediatePosition;
    public Transform finalPosition;
    public string message;
    public int prefabIndex;
}

[System.Serializable]
public class ResponseEvent : UnityEvent<EventData> { }

public class MainCat : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform intermediatePoint;
    [SerializeField] private Transform finalPoint;
    [SerializeField] private float slideSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    [SerializeField] private float bobSpeed = 3f;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] prefabs;

    [Header("Branch Configuration")]
    [SerializeField] private int branch;
    [SerializeField] private string triggerTag = "Bungeobbang";

    public ResponseEvent OnEventProcessed;

    private EventData currentEventData;
    private State currentState = State.Start;

    private bool isOverlapping = false; // 충돌 상태 확인 변수
    private GameObject overlappingObject; // 충돌 중인 오브젝트

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
        string message = $"Branch {branch} processed with prefab index {prefabIndex}.";

        currentEventData = new EventData
        {
            intermediatePosition = intermediatePoint,
            finalPosition = finalPoint,
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
                Instantiate(prefabs[index], transform.position, Quaternion.identity, transform);
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
        // 마우스 좌클릭으로 중간 위치로 이동
        if (Input.GetMouseButtonDown(0) && currentState == State.Start)
        {
            StartCoroutine(SlideSequence(currentEventData.intermediatePosition.position));
            currentState = State.Intermediate;
        }

        // 마우스 우클릭으로 최종 위치로 이동
        if (Input.GetMouseButtonDown(0) && currentState == State.Intermediate)
        {
            StartCoroutine(SlideSequence(currentEventData.finalPosition.position));
            currentState = State.Final;
            isOverlapping = false; // 상태 초기화
        }

        // 최종 위치에 도달한 경우 x 위치를 -5로 변경
        if (currentState == State.Final && Vector3.Distance(transform.position, finalPoint.position) < 0.01f)
        {
            transform.position = new Vector3(-5f, transform.position.y, transform.position.z); // x 위치를 -5로 변경
            currentState = State.Start; // 상태 초기화
            Debug.Log("X position set to -5 and state reset to Start.");
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

    public void SetIsOverlapping(bool value, GameObject otherObject = null)
    {
        isOverlapping = value;
        overlappingObject = value ? otherObject : null;

        if (value && otherObject != null)
        {
            Debug.Log($"Overlapping started with: {otherObject.name}");
        }
        else
        {
            Debug.Log("Overlapping stopped.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            SetIsOverlapping(true, other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            SetIsOverlapping(false);
        }
    }

    private void OnMouseDown()
    {
        // 마우스 클릭으로 isOverlapping 변경
        SetIsOverlapping(true, gameObject);
        Debug.Log($"Object {name} clicked. isOverlapping set to true.");
    }
}
