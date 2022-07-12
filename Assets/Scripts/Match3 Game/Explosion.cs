using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Vector3 homePosition = Vector3.left * 10;
    private Animator animator;
   
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = Vector3.left * 10;
    }

    public void PlayAnimationBlowUp(Vector3 position)
    {
        transform.position = position;
        animator.SetTrigger("Enable");
    }

    public void OnEndBlowUp()
    {
        transform.position = homePosition;
    }
}
