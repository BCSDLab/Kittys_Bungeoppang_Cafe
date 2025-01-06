using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public Components ingredientData; // ScriptableObject ����
    public bool isDrag = false;

    [SerializeField] private Camera frontCamera;

    private int originalLayerIndex;
    private int draggedItemLayerIndex;

    void Update()
    {
        if (isDrag && frontCamera != null)
        {
            Vector2 currentPosition = frontCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentPosition;
        }
    }

    // �巡�� ����
    public void StartDragging(int newLayer, Camera camera)
    {
        originalLayerIndex = gameObject.layer;
        draggedItemLayerIndex = newLayer;
        gameObject.layer = draggedItemLayerIndex;
        frontCamera = camera;
        isDrag = true;
    }

    // �巡�� ����
    public void EndDragging(int restoreLayer)
    {
        gameObject.layer = restoreLayer;
        isDrag = false;
        frontCamera = null;
    }
}


