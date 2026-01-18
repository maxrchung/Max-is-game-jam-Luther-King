using UnityEngine;

public class HandScript : MonoBehaviour
{
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Pull()
    {
        animator.SetTrigger("Pull");
    }

    public void Return()
    {
        animator.SetTrigger("Return");
    }


    private int fingers = 5;

    public void Cut()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
}
