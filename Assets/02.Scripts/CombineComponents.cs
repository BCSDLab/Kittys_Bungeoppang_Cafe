using UnityEngine;

public class CombineComponents : MonoBehaviour
{
    // 필드 선언
    private ComponentsSO currentIngredient;
    [SerializeField] GameObject toolCuttingBoard;

    public void CheckFilling(GameObject empty_Bungeobbang)
    {
        Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(
          empty_Bungeobbang.transform.position,
          empty_Bungeobbang.GetComponent<Collider2D>().bounds.size,
          0f
        );

        foreach (Collider2D col in overlappingColliders)
        {
            if (col.gameObject == empty_Bungeobbang.gameObject) continue;

            if (col.CompareTag("Filling"))
            {
                Debug.Log("붕어빵에 필링 추가");
                Components component = col.GetComponent<Components>();
                if (component != null)
                {
                    currentIngredient = component.componentData;
                }

                FillBungeobbang(empty_Bungeobbang.gameObject, col.gameObject);
                break;
            }
        }
    }

    void FillBungeobbang(GameObject emptyBungeobbang, GameObject filling)
    {
        if (currentIngredient == null)
        {
            Debug.LogWarning("currentIngredient가 null");
            return;
        }
        if (currentIngredient.outputPrefab == null)
        {
            Debug.LogWarning("currentIngredient.outputPrefab이 null");
            return;
        }
        if (toolCuttingBoard == null)
        {
            Debug.LogWarning("toolCuttingBoard가 null");
            return;
        }

        GameObject output = Instantiate(currentIngredient.Bungeobbang);
        output.transform.position = toolCuttingBoard.transform.position;

        Destroy(emptyBungeobbang);
        Destroy(filling);
    }

}
