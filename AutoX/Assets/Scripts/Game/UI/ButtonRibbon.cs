using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ButtonRibbon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    private Animator anim;
    public bool clicked;
    public bool independent = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (independent)
        {
            anim.SetBool("click", true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        anim.SetBool("hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("hover", false);
    }

    public void deselect()
    {

        anim.SetBool("click", false);
    }

    public void select()
    {
        anim.SetBool("click", true);
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("hover", clicked);
        anim.SetBool("click", clicked);
    }
	
	// Update is called once per frame
	void Update () {
        
    }


}
