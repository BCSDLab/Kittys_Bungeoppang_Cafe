using UnityEngine;

public class BungeobbangController : MonoBehaviour
{
    private ComponentsSO currentBungeobbang;

    [SerializeField] public GameObject bungeobbang1;
    [SerializeField] public GameObject bungeobbang2;
    [SerializeField] public GameObject bungeobbang3;

    public static bool bungeobbang1State = false;
    public static bool bungeobbang2State = false;
    public static bool bungeobbang3State = false;

    // 도마 위의 붕어빵을 보이게 하기
    public void CreateOutput()
    {
        if (bungeobbang1State == false)
        {
            bungeobbang1.SetActive(true);
            bungeobbang1State = true;
        }
        else if (bungeobbang2State == false)
        {
            bungeobbang2.SetActive(true);
            bungeobbang2State = true;
        }
        else if (bungeobbang3State == false)
        {
            bungeobbang3.SetActive(true);
            bungeobbang3State = true;
        }
    }

    // 붕어빵에 필링 채우기
    public void FillBungeobbang(GameObject emptyBungeobbang, GameObject filling)
    {
        Debug.Log("FillBungeobbang");
        Components component = filling.GetComponent<Components>();
        currentBungeobbang = component.componentData;

        emptyBungeobbang.SetActive(false);

        if (emptyBungeobbang == bungeobbang1)
        {
            bungeobbang1State = false;
        }
        else if (emptyBungeobbang == bungeobbang2)
        {
            bungeobbang2State = false;
        }
        else if (emptyBungeobbang == bungeobbang3)
        {
            bungeobbang3State = false;
        }

        GameObject output = Instantiate(
            currentBungeobbang.Bungeobbang,
            emptyBungeobbang.transform.position,
            Quaternion.identity
        );

        Destroy(filling);
    }
}
