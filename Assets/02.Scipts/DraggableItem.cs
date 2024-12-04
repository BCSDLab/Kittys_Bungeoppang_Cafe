using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    public Components ingredientData; // ScriptableObject ¿¬°á
    public bool isDrag = false;

    void Update()
    {
        if (isDrag)
        {
            Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = currentPosition;
        }
    }
}


