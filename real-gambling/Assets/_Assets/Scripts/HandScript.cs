using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public SkinnedMeshRenderer Renderer;
    public Material Transparent;
    public Image RedPanel;

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

    public void View()
    {
        animator.SetTrigger("View");
    }

    public void Unview()
    {
        animator.SetTrigger("Unview");
    }



    private int fingers = 5;

    public void Cut()
    {
        StartCoroutine(CutCoroutine());
    }

    public IEnumerator CutCoroutine()
    {
        animator.SetTrigger("View");

        yield return new WaitForSeconds(2);


        var materials = Renderer.materials;
        if (fingers == 5)
        {
            materials[5] = Transparent;
        }
        else if (fingers == 4)
        {
            materials[4] = Transparent;
        }
        else if (fingers == 3)
        {
            materials[3] = Transparent;
        }
        else if (fingers == 2)
        {
            materials[2] = Transparent;
        }
        else if (fingers == 1)
        {
            // Lose
            materials[1] = Transparent;
        }

        Renderer.materials = materials;

        fingers = fingers - 1;

        // Flash red
        Color color = RedPanel.color;
        color.a = 0.75f; // Fully visible
        RedPanel.color = color;
        var time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime / 1f; // Fade over one second
            color.a = Mathf.Lerp(0.75f, 0f, time);
            RedPanel.color = color;
            yield return null;
        }
        color.a = 0;
        RedPanel.color = color;

        yield return new WaitForSeconds(1);
        animator.SetTrigger("Unview");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
