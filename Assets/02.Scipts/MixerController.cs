using System.Collections;
using UnityEngine;

public class MixerController : MonoBehaviour, ICookingTools
{
    private SpriteRenderer originRenderer;
    private Color originalColor;
    private bool isCooking = false;
    [SerializeField] private Transform cuttingBoardPosition;
    [SerializeField] private float cookingTime = 5f;

    public ComponentsManager componentsManager;
    private ComponentsSO currentIngredient;


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
    public void CheckComponent(GameObject ingredient)
    {
        Components component = ingredient.GetComponent<Components>();

        if (currentIngredient != null)
        {
            Debug.Log("이미 다른 재료가 안에 들어 있습니다.");
            return;
        }

        if (component != null && component.componentData != null)
        {
            currentIngredient = component.componentData;
            StartCoroutine(ProcessCooking());
        }
    }

    // 5초 동안 섞기
    public IEnumerator ProcessCooking()
    {
        Debug.Log("재료를 섞는 중입니다...");
        ChangeColor();
        yield return new WaitForSeconds(cookingTime);
        ChangeColor();
        CreateOutput();
        currentIngredient = null;
    }
    
    // 섞는 동안 믹서기를 해당 재료의 색깔로 바꾸기
    public void ChangeColor()
    {
        isCooking = !isCooking;

        SpriteRenderer mixerRenderer = GetComponent<SpriteRenderer>();
        if (mixerRenderer == null || currentIngredient == null) return;

        SpriteRenderer ingredientRenderer = currentIngredient.ComponentPrefab.GetComponent<SpriteRenderer>();
        if (ingredientRenderer == null) return;

        if (isCooking)
        {
            mixerRenderer.color = ingredientRenderer.color;
        }
        else
        {
            mixerRenderer.color = originalColor;
        }
    }
    
    // 기다린 후에 Output Prefab을 도마에 올려놓기
    public void CreateOutput()
    {
        Debug.Log("제조가 완료되었습니다.");
    }
}
