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
        
        // �ʱ� ���� ����
        if (originRenderer != null)
        {
            originalColor = originRenderer.color; 
        }
    }

    // �ͼ��⿡ ��� �ֱ�
    public void CheckComponent(GameObject ingredient)
    {
        Components component = ingredient.GetComponent<Components>();

        if (currentIngredient != null)
        {
            Debug.Log("�̹� �ٸ� ��ᰡ �ȿ� ��� �ֽ��ϴ�.");
            return;
        }

        if (component != null && component.componentData != null)
        {
            currentIngredient = component.componentData;
            StartCoroutine(ProcessCooking());
        }
    }

    // 5�� ���� ����
    public IEnumerator ProcessCooking()
    {
        Debug.Log("��Ḧ ���� ���Դϴ�...");
        ChangeColor();
        yield return new WaitForSeconds(cookingTime);
        ChangeColor();
        CreateOutput();
        currentIngredient = null;
    }
    
    // ���� ���� �ͼ��⸦ �ش� ����� ����� �ٲٱ�
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
    
    // ��ٸ� �Ŀ� Output Prefab�� ������ �÷�����
    public void CreateOutput()
    {
        Debug.Log("������ �Ϸ�Ǿ����ϴ�.");
    }
}
