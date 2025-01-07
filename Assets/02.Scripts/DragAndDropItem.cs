using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    private DraggableItem draggableItem;
    
    [SerializeField] MixerController mixerController;
    [SerializeField] OvenController ovenController;
    [SerializeField] BungeoppangController bungeoppangController;
    [SerializeField] MainCat catController;
    [SerializeField] TrashCanController trashCanController;

    [SerializeField] Camera frontCamera;
    private Camera mainCamera;

    private Vector2 startPosition;

    int originalLayerIndex;
    int draggedItemLayerIndex;

    void Start()
    {
        mainCamera = Camera.main;

        // 레이어 정보 저장
        originalLayerIndex = this.gameObject.layer;
        draggedItemLayerIndex = LayerMask.NameToLayer("DraggedItem");
    }

    void Update()
    {
        if (draggableItem == null)
        {
            PickUpItem();
        }
        else
        {
            PutDownItem();
        }
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
                    // 레이어 바꿔서 화면 제일 위에 렌더링하기
                    draggableItem.StartDragging(draggedItemLayerIndex, frontCamera);
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

            // 원래 레이어로 바꾸기
            draggableItem.EndDragging(originalLayerIndex);

            if (draggableItem != null)
            {
                HandleDrop();

                // 아이템 원래 자리로 돌아가기
                draggableItem.transform.position = startPosition;
                draggableItem.isDrag = false;
                draggableItem = null;
            }
        }
    }

    // 드랍 로직
    void HandleDrop()
    {
        // 재료를 믹서기에 넣기
        if (mixerController != null && draggableItem.CompareTag("Ingredient") && IsOverlapping(draggableItem.gameObject, mixerController.gameObject))
        {
            mixerController.CheckComponent(draggableItem.gameObject);
        }

        // 밀가루를 오븐에 넣기
        else if (ovenController != null && draggableItem.CompareTag("Dough") && IsOverlapping(draggableItem.gameObject, ovenController.gameObject))
        {
            ovenController.CheckComponent(draggableItem.gameObject);
        }

        // 필링을 빈 붕어빵에 넣기
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

        else if (draggableItem.CompareTag("Bungeobbang"))
        {
            catController.CheckComponent(draggableItem.gameObject);
            Destroy(draggableItem.gameObject);
        }
        
        // 쓰레기통에 버리기
        if (trashCanController != null && IsOverlapping(draggableItem.gameObject, trashCanController.gameObject))
        {

            if (draggableItem.CompareTag("Filling") || draggableItem.CompareTag("Bungeobbang") || draggableItem.CompareTag("Empty_Bungeobbang"))
            {
                Destroy(draggableItem.gameObject);
                Debug.Log("쓰레기통에 버렸습니다.");
            }
        }

        
    }

    // 닿아 있는지 확인하기
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

