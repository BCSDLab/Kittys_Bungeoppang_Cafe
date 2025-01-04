using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    private DraggableItem draggableItem;
    
    [SerializeField] MixerController mixerController;
    [SerializeField] OvenController ovenController;
    [SerializeField] BungeoppangController bungeoppangController;
    [SerializeField] MainCat catController;

    private Camera mainCamera;
    private Vector2 startPosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        PickUpItem();
        PutDownItem();
    }

    // ������ ����
    void PickUpItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                draggableItem = hit.collider.GetComponent<DraggableItem>();

                if (draggableItem != null)
                {
                    Debug.Log("������ ����");
                    startPosition = draggableItem.transform.position;
                    draggableItem.isDrag = true;
                }
            }
        }
    }

    // ������ ����
    void PutDownItem()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (draggableItem != null)
            {
                // ��Ḧ �ͼ��⿡ ����
                if (mixerController != null && draggableItem.CompareTag("Ingredient") && IsOverlapping(draggableItem.gameObject, mixerController.gameObject))
                {
                    mixerController.CheckComponent(draggableItem.gameObject);
                }

                // ������ ���쿡 ����
                else if (ovenController != null && draggableItem.CompareTag("Dough") && IsOverlapping(draggableItem.gameObject, ovenController.gameObject))
                {
                    ovenController.CheckComponent(draggableItem.gameObject);
                }

                // �ʸ��� �ؾ�� ���
                else if (draggableItem.CompareTag("Filling"))
                {
                    if (bungeoppangController.bungeoppang1 != null && IsOverlapping(draggableItem.gameObject, bungeoppangController.bungeoppang1.gameObject))
                    {
                        bungeoppangController.FillBungeobbang(bungeoppangController.bungeoppang1.gameObject, draggableItem.gameObject);
                    }
                    else if (bungeoppangController.bungeoppang2 != null && IsOverlapping(draggableItem.gameObject, bungeoppangController.bungeoppang2.gameObject))
                    {
                        bungeoppangController.FillBungeobbang(bungeoppangController.bungeoppang2.gameObject, draggableItem.gameObject);
                    }
                    else if (bungeoppangController.bungeoppang3 != null && IsOverlapping(draggableItem.gameObject, bungeoppangController.bungeoppang3.gameObject))
                    {
                        bungeoppangController.FillBungeobbang(bungeoppangController.bungeoppang3.gameObject, draggableItem.gameObject);
                    }
                }
                
                else if (draggableItem.CompareTag("Cat"))
                {
                    if (catController != null)
                    {
                        Components components = draggableItem.GetComponent<Components>();
                        if (components != null && components.componentData != null)
                        {
                            catController.ReceiveBungeobbangData(components.componentData);
                            Debug.Log($"고양이에게 아이템 전달: {components.componentData.componentName}");
                        }
                        else
                        {
                            Debug.LogWarning("아이템에 유효한 데이터가 없습니다.");
                        }
                    }
                }

                // �巡�� �� ���� ��ġ��
                if (draggableItem != null)
                {
                    draggableItem.transform.position = startPosition;
                    draggableItem.isDrag = false;
                    draggableItem = null;
                }
            }
        }
    }

    // �����ִ��� Ȯ���ϱ�
    private bool IsOverlapping(GameObject obj1, GameObject obj2)
    {

        Collider2D collider1 = obj1.GetComponent<Collider2D>();
        Collider2D collider2 = obj2.GetComponent<Collider2D>();

        if (collider1 != null && collider2 != null)
        {
            return collider1.bounds.Intersects(collider2.bounds);
        }

        return false;
    }
}

