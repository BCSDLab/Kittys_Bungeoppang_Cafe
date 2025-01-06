using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public Components ingredientData; // ScriptableObject 연결
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

    // 드래그 시작
    public void StartDragging(int newLayer, Camera camera)
    {
        originalLayerIndex = gameObject.layer;
        draggedItemLayerIndex = newLayer;
        gameObject.layer = draggedItemLayerIndex;
        frontCamera = camera;
        isDrag = true;
    }

    // 드래그 종료
    public void EndDragging(int restoreLayer)
    {
        gameObject.layer = restoreLayer;
        isDrag = false;
        frontCamera = null;
    }
}


