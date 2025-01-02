using System.Collections;
using UnityEngine;

public class MixerController : MonoBehaviour, ICookingTools
{
    [SerializeField] public float cookingTime = 5f;
    [SerializeField] MixerTimeBar mixerTimeBar;

    private ComponentsSO currentIngredient;

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
            mixerTimeBar.StartTimer();
            StartCoroutine(ProcessCooking());
        }
    }

    // 5초 동안 섞기
    public IEnumerator ProcessCooking()
    {
        Debug.Log("재료를 섞는 중입니다...");
        yield return new WaitForSeconds(cookingTime);
        CreateOutput();
        currentIngredient = null;
    }

    // 기다린 후에 Output Prefab을 주변에 놓기
    public void CreateOutput()
    {
        Debug.Log("제조가 완료되었습니다.");
        GameObject output = Instantiate(currentIngredient.outputPrefab);
        float random_x = Random.Range(0.8f, -0.8f);
        output.transform.position = this.transform.position + new Vector3(random_x, -0.8f, 0);
    }
}
