using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ChangeAnimationRuntime : MonoBehaviour
{
    public List<Animator> anims;

    public void ChangeAnimationToIdle()
    {
        if (anims[0].GetBool("walk"))
        {
            foreach (Animator anim in anims)
            {
                if (anim.gameObject.activeSelf)
                {
                    anim.SetBool("walk", false);
                }
            }
        }
    }
    public void ChangeAnimationToWalk()
    {
        if (!anims[0].GetBool("walk"))
        {
            foreach (Animator anim in anims)
            {
                if (anim.gameObject.activeSelf)
                {
                    anim.SetBool("walk", true);
                }
            }
        }
    }
}
