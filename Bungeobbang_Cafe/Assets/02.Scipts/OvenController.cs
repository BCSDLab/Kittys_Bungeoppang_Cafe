using System.Collections;
using UnityEngine;

public class OvenController : MonoBehaviour, ICookingTools
{
    private SpriteRenderer originRenderer;
    private Color originalColor;
    private bool isCooking = false;
    [SerializeField] private Transform cuttingBoardPosition;
    [SerializeField] private float cookingTime = 10f;

    public ComponentsManager componentsManager;
    private ComponentsSO currentDough;

    void Start()
    {
        originRenderer = GetComponent<SpriteRenderer>();

        // 초기 색상 저장
        if (originRenderer != null)
        {
            originalColor = originRenderer.color;
        }
    }

    // 믹서기에 재료 넣기
    public void CheckComponent(GameObject dough)
    {
        Components component = dough.GetComponent<Components>();

        if (currentDough != null)
        {
            Debug.Log("이미 다른 반죽이 안에 들어 있습니다.");
            return;
        }

        if (component != null && component.componentData != null)
        {
            currentDough = component.componentData;
            StartCoroutine(ProcessCooking());
        }
    }

    // 5초 동안 섞기
    public IEnumerator ProcessCooking()
    {
        Debug.Log("반죽을 굽는 중입니다...");
        ChangeColor();
        yield return new WaitForSeconds(cookingTime);
        ChangeColor();
        CreateOutput();
        currentDough = null;
    }

    // 굽는 동안 오븐을 해당 반죽의 색깔로 바꾸기
    public void ChangeColor()
    {
        isCooking = !isCooking;

        SpriteRenderer ovenRenderer = GetComponent<SpriteRenderer>();
        if (ovenRenderer == null || currentDough == null) return;

        SpriteRenderer doughRenderer = currentDough.ComponentPrefab.GetComponent<SpriteRenderer>();
        if (doughRenderer == null) return;

        if (isCooking)
        {
            ovenRenderer.color = doughRenderer.color;
        }
        else
        {
            ovenRenderer.color = originalColor;
        }
    }

    // 기다린 후에 Output Prefab을 도마에 올려놓기
    public void CreateOutput()
    {
        Debug.Log("제조가 완료되었습니다.");
    }
}
