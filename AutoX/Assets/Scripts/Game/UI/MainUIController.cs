using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainUIController : MonoBehaviour {

    public InputField codeUI;
    public InputField debugUI;

	// Use this for initialization
	void Start () {
        selectCodeUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void selectCodeUI()
    {
        debugUI.gameObject.active = false;
        codeUI.gameObject.active = true;
    }

    public void selectDebugUI()
    {
        codeUI.gameObject.active = false;
        debugUI.gameObject.active = true;
    }

    public void selectWorld()
    {
        codeUI.gameObject.active = false;
        debugUI.gameObject.active = false;
    }
}
