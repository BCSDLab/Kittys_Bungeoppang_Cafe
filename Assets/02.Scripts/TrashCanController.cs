using UnityEngine;

public class TrashCanController : MonoBehaviour
{
    [SerializeField] GameObject trashCanCover;

    Vector3 originalPos;
    Vector3 openingPos;
    Vector3 openingOffset = new Vector3(0, 0.3f, 0);

    bool isOpen = false;

    void Start()
    {
        originalPos = trashCanCover.transform.position;
        openingPos = originalPos + openingOffset;
    }

    void Update()
    {
        if (isOpen)
        {
            trashCanCover.transform.position = openingPos;
        }
        else
        {
            trashCanCover.transform.position = originalPos;
        }
    }

    // trashCanHole¿¡ ¸¶¿ì½º°¡ µé¾î°¡ ÀÖÀ¸¸é ¶Ñ²± ¿­¸®±â
    public void OnMouseOver()
    {
        isOpen = true;
        CancelInvoke("CloseCover");
    }

    // ¸¶¿ì½º¸¦ ¶¼¸é ¶Ñ²± ´ÝÈ÷±â
    public void OnMouseExit()
    {
        Invoke("CloseCover", 0.3f);
    }

    private void CloseCover()
    {
        isOpen = false;
    }

}
