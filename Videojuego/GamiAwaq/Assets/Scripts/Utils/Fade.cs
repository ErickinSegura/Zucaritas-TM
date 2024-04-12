using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    static public Fade Instance;
    private Animator animator;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

}
