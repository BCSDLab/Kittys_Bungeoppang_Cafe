using System.Collections;
using UnityEngine;
interface ICookingTools
{
    void CheckComponent(GameObject ingredient);
    IEnumerator ProcessCooking();
}