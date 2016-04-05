using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorPanelController : MonoBehaviour {

    public static ErrorPanelController instance;

    public Text text;
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetMessage(string text)
    {
        this.text.text = text;
    }

    public void Open(bool isOpen)
    {
        anim.SetBool("open", isOpen);
    }

}
