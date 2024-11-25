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

        // �ʱ� ���� ����
        if (originRenderer != null)
        {
            originalColor = originRenderer.color;
        }
    }

    // �ͼ��⿡ ��� �ֱ�
    public void CheckComponent(GameObject dough)
    {
        Components component = dough.GetComponent<Components>();

        if (currentDough != null)
        {
            Debug.Log("�̹� �ٸ� ������ �ȿ� ��� �ֽ��ϴ�.");
            return;
        }

        if (component != null && component.componentData != null)
        {
            currentDough = component.componentData;
            StartCoroutine(ProcessCooking());
        }
    }

    // 5�� ���� ����
    public IEnumerator ProcessCooking()
    {
        Debug.Log("������ ���� ���Դϴ�...");
        ChangeColor();
        yield return new WaitForSeconds(cookingTime);
        ChangeColor();
        CreateOutput();
        currentDough = null;
    }

    // ���� ���� ������ �ش� ������ ����� �ٲٱ�
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

    // ��ٸ� �Ŀ� Output Prefab�� ������ �÷�����
    public void CreateOutput()
    {
        Debug.Log("������ �Ϸ�Ǿ����ϴ�.");
    }
}
