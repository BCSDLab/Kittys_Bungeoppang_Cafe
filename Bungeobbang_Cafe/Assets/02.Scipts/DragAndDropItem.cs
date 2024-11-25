using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    private DraggableItem draggableItem;
    private Camera mainCamera;
    private Vector2 startPosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 아이템 집기
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && (hit.collider.CompareTag("Ingredient") || hit.collider.CompareTag("Dough")))
            {
                draggableItem = hit.collider.GetComponent<DraggableItem>();
                if (draggableItem != null)
                {
                    startPosition = draggableItem.transform.position;
                    draggableItem.isDrag = true;
                }
            }
        }
        // 아이템 놓기
        else if (Input.GetMouseButtonUp(0))
        {
            if (draggableItem != null)
            {
                draggableItem.isDrag = false; 
                draggableItem.transform.position = startPosition;
                draggableItem = null;
            }
        }
    }
}
