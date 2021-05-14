using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlatform : MonoBehaviour
{
    public GameObject platform;
    private Animator animator;
    void Start()
    {
        animator = platform.GetComponent<Animator>();
        animator.enabled = false;
        
        
    }
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        animator.enabled = true;
        animator.SetTrigger("Launch");
    }
}
