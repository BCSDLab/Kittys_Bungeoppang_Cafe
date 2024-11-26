using UnityEngine;

public class DragAndDropItem : MonoBehaviour
{
    private DraggableItem draggableItem;
    [SerializeField] MixerController mixerController;
    [SerializeField] OvenController ovenController;
    private Camera mainCamera;
    private Vector2 startPosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ������ ����
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

        // ������ ����
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

                draggableItem.transform.position = startPosition;
                draggableItem.isDrag = false;
                draggableItem = null;
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

