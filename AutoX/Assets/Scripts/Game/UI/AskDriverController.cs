using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AskDriverController : MonoBehaviour {

    public static AskDriverController instance;

    public Button btnUp;
    public Button btnLeft;
    public Button btnRight;

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

    public void Open(bool isOpen)
    {
        anim.SetBool("open", isOpen);
    }

    public void setButtonUp(int n)
    {
        bool active = false;

        if (n != 0)
        {
            active = true;
        }
        btnUp.gameObject.SetActive(active);
    }

    public void setButtonRight(int n)
    {
        bool active = false;

        if (n != 0)
        {
            active = true;
        }
        btnRight.gameObject.SetActive(active);
    }

    public void setButtonLeft(int n)
    {
        bool active = false;

        if (n != 0)
        {
            active = true;
        }
        btnLeft.gameObject.SetActive(active);
    }
}
