using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CompileController : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData)
    {
        Grammar.compileAndRun();
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
