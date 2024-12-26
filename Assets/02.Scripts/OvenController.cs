using System.Collections;
using UnityEngine;

public class OvenController : MonoBehaviour, ICookingTools
{
    [SerializeField] private float cookingTime = 10f;

    [SerializeField] OvenTimeBar ovenTimeBar;

    [SerializeField] private GameObject bungeobbang1;
    [SerializeField] private GameObject bungeobbang2;
    [SerializeField] private GameObject bungeobbang3;

    private bool bungeobbang1State = false;
    private bool bungeobbang2State = false;
    private bool bungeobbang3State = false;

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
        CreateOutput();
        currentDough = null;
    }

    // ��ٸ� �Ŀ� ������ �ؾ ����
    public void CreateOutput()
    {
        if(bungeobbang1State == false)
        {
            bungeobbang1.SetActive(true);
            bungeobbang1State = true;
        }
        else if(bungeobbang2State == false)
        {
            bungeobbang2.SetActive(true);
            bungeobbang2State = true;
        }
        else if(bungeobbang3State == false)
        {
            bungeobbang3.SetActive(true);
            bungeobbang3State = true;
        }
        else
        {
            Debug.Log("�ؾ�� ���� �ڸ��� �����մϴ�.");
        }
    }
}
