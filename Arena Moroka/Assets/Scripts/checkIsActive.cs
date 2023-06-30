using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIsActive : MonoBehaviour
{
    private Animator animator;
    private bool isActive = false;
    private bool animationPlayed = false;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (gameObject.name == "Rifle")
        {
            Debug.Log("ACTIVE!!!!");
            animator.SetTrigger("pickup");
        }
        if (gameObject.name == "Pistol")
        {
            Debug.Log("ACTIVE!!!!");
            animator.SetTrigger("pickup");
        }
    }
    void Update()
    {

    }
}
