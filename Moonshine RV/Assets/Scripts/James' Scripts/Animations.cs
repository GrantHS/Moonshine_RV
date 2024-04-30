using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAnimation()
    { 
    animator.Play("Soccer");
    }
}
