using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    private DraggableItem draggableItem;
    
    [SerializeField] MixerController mixerController;
    [SerializeField] OvenController ovenController;
    [SerializeField] BungeobbangController bungeobbangController;

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

    // 아이템 집기
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
                    Debug.Log("아이템 집기");
                    startPosition = draggableItem.transform.position;
                    draggableItem.isDrag = true;
                }
            }
        }
    }

    // 아이템 놓기
    void PutDownItem()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (draggableItem != null)
            {
                // 재료를 믹서기에 전달
                if (mixerController != null && draggableItem.CompareTag("Ingredient") && IsOverlapping(draggableItem.gameObject, mixerController.gameObject))
                {
                    mixerController.CheckComponent(draggableItem.gameObject);
                }

                // 반죽을 오븐에 전달
                else if (ovenController != null && draggableItem.CompareTag("Dough") && IsOverlapping(draggableItem.gameObject, ovenController.gameObject))
                {
                    ovenController.CheckComponent(draggableItem.gameObject);
                }

                // 필링을 붕어빵에 드롭
                else if (draggableItem.CompareTag("Filling"))
                {
                    if (bungeobbangController.bungeobbang1 != null && IsOverlapping(draggableItem.gameObject, bungeobbangController.bungeobbang1.gameObject))
                    {
                        bungeobbangController.FillBungeobbang(bungeobbangController.bungeobbang1.gameObject, draggableItem.gameObject);
                    }
                    else if (bungeobbangController.bungeobbang2 != null && IsOverlapping(draggableItem.gameObject, bungeobbangController.bungeobbang2.gameObject))
                    {
                        bungeobbangController.FillBungeobbang(bungeobbangController.bungeobbang2.gameObject, draggableItem.gameObject);
                    }
                    else if (bungeobbangController.bungeobbang3 != null && IsOverlapping(draggableItem.gameObject, bungeobbangController.bungeobbang3.gameObject))
                    {
                        bungeobbangController.FillBungeobbang(bungeobbangController.bungeobbang3.gameObject, draggableItem.gameObject);
                    }
                }

                // 드래그 후 원래 위치로
                if (draggableItem != null)
                {
                    draggableItem.transform.position = startPosition;
                    draggableItem.isDrag = false;
                    draggableItem = null;
                }
            }
        }
    }

    // 겹쳐있는지 확인하기
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

