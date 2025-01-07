using System.Collections;
using UnityEngine;

public class OvenController : MonoBehaviour, ICookingTools
{
    [SerializeField] public float cookingTime = 7f;
    [SerializeField] OvenTimeBar ovenTimeBar;
    [SerializeField] BungeoppangController bungeoppangController;

    public ComponentsManager componentsManager;
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
        bungeoppangController.CreateBungeoppang();
        currentDough = null;
    }

    
}
