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

    public ResponseEvent OnEventProcessed;

    private EventData currentEventData;
    [SerializeField] private State currentState = State.Start;
    private bool give = false;
    private bool text = true;
    private ComponentsSO currentbungeobbang;
    private int addon = 0;

    [SerializeField] private DialogSystem dialogSystem01;
    [SerializeField] private ResourceManager resourceManager;

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
        
        //HandleBranchLogic();
    }
    

    public int CheckComponent(GameObject bungeobbang)
    {
        Components component = bungeobbang.GetComponent<Components>();

        if (component != null && component.componentData != null)
        {
            give = true;
            int result = -1;

            switch (component.componentData.componentName)
            {
                case "Peach":
                    result = 0;
                    break;
                case "Chocolate":
                    result = 3;
                    break;
                case "Avocado":
                    result = 1;
                    break;
                case "Red_Pepper":
                    result = 2;
                    break;
                case "Milk":
                    result = 4;
                    break;
                default:
                    Debug.LogWarning($"알 수 없는 componentName: {component.componentData.componentName}");
                    result = -1;
                    break;
            }

            Debug.Log($"componentName: {component.componentData.componentName}, 반환 값: {result}");

            // branch와 반환값을 비교하여 결과 반환
            dialogSystem01.ReceiveBranchValue((result == branch) ? 151 : 152);
            if (result == branch)
            {
                resourceManager.AddCoin(1000 + addon * 50);
                resourceManager.AddFame(5 + addon);
            }
            else
            {
                resourceManager.AddFame(-5 - addon);
            }
            
            return (result == branch) ? 151 : 152;
        }

        Debug.LogWarning("유효하지 않은 붕어빵 데이터입니다.");
        return -1; // 유효하지 않은 데이터일 경우 -1 반환
    }


    private void HandleBranchLogic()
    {
        int prefabIndex = Random.Range(1, 6);
        addon = prefabIndex;
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

        // 기존에 생성된 자식 프리팹이 있으면 삭제
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            Debug.Log($"Child {child.name} destroyed.");
        }

        // 새로운 프리팹 생성
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
        if (Input.GetMouseButtonDown(0) && currentState == State.Start)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
                Debug.Log($"Child {child.name} destroyed in Final State.");
            }
            HandleBranchLogic();
            StartCoroutine(SlideSequence(currentEventData.intermediatePosition.position));
            currentState = State.Intermediate;
        }
        
        if (currentState == State.Intermediate && give == true)
        {
            give = false;
            currentState = State.Final; // 다음 상태로 전환
        }

        if (currentState == State.Final)
        {
            text = true;
            StartCoroutine(SlideSequence(currentEventData.finalPosition.position));
        }
        
        if (Input.GetMouseButtonDown(0) && currentState == State.Intermediate && text == true)
        {
            int randomValue = UnityEngine.Random.Range(1, 151);
            
            if (dialogSystem01 != null)
            {
                dialogSystem01.ReceiveBranchValue(randomValue);
            }
            
            branch = randomValue / 30;
            
            if (dialogSystem01 != null)
            {
                bool isDialogComplete = dialogSystem01.UpdateDialog();
            }

            text = false;
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
        
        if (currentState == State.Final)
        {
            currentState = State.Start;
        }
    }
}