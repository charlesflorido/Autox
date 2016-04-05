using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBarController : MonoBehaviour {

    private Scrollbar scrollBar;
    public float time;

    private float originalTime;

    // Use this for initialization
	void Start () {
        scrollBar = GetComponent<Scrollbar>();
        originalTime = time;
        updateScrollBar();
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(originalTime <= 0.0f)
        {
            Application.LoadLevel(1);
        }
        else
        {
            originalTime -= Time.deltaTime;
            
        }

        updateScrollBar();

    }

    void updateScrollBar()
    {
        scrollBar.size = 1.0f - (originalTime / time);
    }
}
