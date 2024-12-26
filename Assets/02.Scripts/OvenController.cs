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
            ovenTimeBar.StartTimer();
            StartCoroutine(ProcessCooking());
        }
    }

    // 5초 동안 섞기
    public IEnumerator ProcessCooking()
    {
        Debug.Log("반죽을 굽는 중입니다...");
        yield return new WaitForSeconds(cookingTime);
        CreateOutput();
        currentDough = null;
    }

    // 기다린 후에 도마에 붕어빵 놓기
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
            Debug.Log("붕어빵을 놓을 자리가 부족합니다.");
        }
    }
}
