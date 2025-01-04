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

    [Header("Dialog Manager")]
    [SerializeField] private DialogManager dialogManager; // DialogManager 참조 추가

    public ResponseEvent OnEventProcessed;

    private EventData currentEventData;
    private State currentState = State.Start;
    private bool give = false;
    private bool text = true;


    private enum State
    {
        Start,
        Intermediate,
        Final
    }

    private void Start()
    {
        transform.position = startPoint.position;

        if (OnEventProcessed == null)
            OnEventProcessed = new ResponseEvent();

        HandleBranchLogic();
    }
    
    public void ReceiveDividedValue(int value)
    {
        branch = value;
        Debug.Log($"MainCat received divided value: {branch}");
    }
    
    public bool IsAtIntermediatePosition()
    {
        if (text == true)
        {
            return currentState == State.Intermediate && 
                   Vector3.Distance(transform.position, intermediatePoint.position) < 0.01f;
        }
        text = false; 
        return false;
    }

    public void ReceiveBungeobbangData(ComponentsSO bungeobbangComponent)
    {
        Debug.Log($"붕어빵 정보 수신: {bungeobbangComponent.componentName}");

        int receivedValue = MapIngredientToValue(bungeobbangComponent.componentName);

        if (receivedValue != -1)
        {
            if (dialogManager != null)
            {
                int dialogValue = (branch % 5) + 1; // DialogManager에서 기대값을 가져옴

                if (receivedValue == dialogValue)
                {
                    Debug.Log("Received value matches DialogManager's expected value: True");
                    give = true;
                }
                else
                {
                    Debug.Log("Received value does not match DialogManager's expected value: False");
                }
            }
        }
        else
        {
            Debug.LogWarning("Received an invalid ingredient name.");
        }
    }

    private int MapIngredientToValue(string ingredientName)
    {
        switch (ingredientName)
        {
            case "Avocado":
                return 1;
            case "Chocolate":
                return 2;
            case "Milk":
                return 3;
            case "Peach":
                return 4;
            case "Red_Pepper":
                return 5;
            default:
                return -1; // Invalid ingredient
        }
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

        // 중간 위치에서 DialogManager에 값 전달
        if (currentState == State.Intermediate && give == true)
        {
            if (dialogManager != null)
            {
                Debug.Log("Cat reached intermediate position. Notifying DialogManager.");
                
            }

            give = false;
            currentState = State.Final; // 다음 상태로 전환
        }

        if (Input.GetMouseButtonDown(0) && currentState == State.Final)
        {
            text = true;
            dialogManager.ReceiveValueFromMainCat(branch); // DialogManager로 값 전달
            StartCoroutine(SlideSequence(currentEventData.finalPosition.position));
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