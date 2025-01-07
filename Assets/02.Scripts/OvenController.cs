using System.Collections;
using UnityEngine;

public class OvenController : MonoBehaviour, ICookingTools
{
    [SerializeField] public float cookingTime = 7f;
    [SerializeField] OvenTimeBar ovenTimeBar;
    [SerializeField] BungeoppangController bungeoppangController;

    public ComponentsManager componentsManager;
    private ComponentsSO currentDough;


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
            ovenTimeBar.StartTimer();
            StartCoroutine(ProcessCooking());
        }
    }

    // 5�� ���� ����
    public IEnumerator ProcessCooking()
    {
        Debug.Log("������ ���� ���Դϴ�...");
        yield return new WaitForSeconds(cookingTime);
        bungeoppangController.CreateBungeoppang();
        currentDough = null;
    }

    
}
