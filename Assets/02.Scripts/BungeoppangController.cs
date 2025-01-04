using UnityEngine;

public class BungeoppangController : MonoBehaviour
{
    private ComponentsSO currentBungeobbang;

    [SerializeField] public GameObject bungeoppang1;
    [SerializeField] public GameObject bungeoppang2;
    [SerializeField] public GameObject bungeoppang3;
    
    // 붕어빵이 보이는지 나타내는 상태 값
    public bool isBungeoppang1Visible  = false;
    public bool isBungeoppang2Visible = false;
    public bool isBungeoppang3Visible = false;

    // 특별한 붕어빵인지 나타내는 상태 값
    public bool isBungeoppang1Special = false;
    public bool isBungeoppang2Special = false;
    public bool isBungeoppang3Special = false;

    // 도마 위에 붕어빵을 만들기
    public void CreateBungeobbang()
    {
        // 안 보이는 붕어빵 보이게 하기
        if (isBungeoppang1Visible == false && isBungeoppang1Special == false)
        {
            bungeoppang1.SetActive(true);
            isBungeoppang1Visible = true;
        }
        else if (isBungeoppang2Visible == false && isBungeoppang2Special == false)
        {
            bungeoppang2.SetActive(true);
            isBungeoppang2Visible = true;
        }
        else if (isBungeoppang3Visible == false && isBungeoppang3Special == false)
        {
            bungeoppang3.SetActive(true);
            isBungeoppang3Visible = true;
        }
    }

    // 붕어빵에 필링 채우기
    public void FillBungeobbang(GameObject emptyBungeobbang, GameObject filling)
    {
        Components component = filling.GetComponent<Components>();
        currentBungeobbang = component.componentData;

        emptyBungeobbang.SetActive(false);

        // 빈 붕어빵이 있으면 특별한 붕어빵으로 만들기
        if (emptyBungeobbang == bungeoppang1 && isBungeoppang1Visible == true && isBungeoppang1Special == false)
        {
            MakeSpecialBungeobbang(emptyBungeobbang.transform.position, 1, filling);
        }
        else if (emptyBungeobbang == bungeoppang2 && isBungeoppang2Visible == true && isBungeoppang2Special == false)
        {
            MakeSpecialBungeobbang(emptyBungeobbang.transform.position, 2, filling);
        }
        else if (emptyBungeobbang == bungeoppang3 && isBungeoppang3Visible == true && isBungeoppang3Special == false)
        {
            MakeSpecialBungeobbang(emptyBungeobbang.transform.position, 3, filling);
        }
    }

    // 특별한 붕어빵 만들기
    void MakeSpecialBungeobbang(Vector3 bungeobbangPos, int bungeobbangIndex, GameObject filling)
    {
        GameObject output = Instantiate(
        currentBungeobbang.Bungeobbang,
        bungeobbangPos,
        Quaternion.identity
        );

        if (bungeobbangIndex == 1)
        {
            isBungeoppang1Visible = false;
            isBungeoppang1Special = true;
        }
        else if (bungeobbangIndex == 2)
        {
            isBungeoppang2Visible = false;
            isBungeoppang2Special = true;
        }
        else if(bungeobbangIndex == 3)
        {
            isBungeoppang3Visible = false;
            isBungeoppang3Special = true;
        }

        Destroy(filling);
    }
}
