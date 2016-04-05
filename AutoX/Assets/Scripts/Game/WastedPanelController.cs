using UnityEngine;
using System.Collections;

public class WastedPanelController : MonoBehaviour {

    public static WastedPanelController instance;
    private Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Awake()
    {
        if (instance == null)
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

        if (isOpen)
        {
        }
    }
}
