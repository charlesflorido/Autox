using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OutController : MonoBehaviour {

    public static OutController instance;

    private InputField inputField;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this);
        }

    }

    void Start()
    {
        inputField = GetComponent<InputField>();
        inputField.text = "";
    }

    public string getText()
    {
        return inputField.text;
    }

    public void setText(string str)
    {
        inputField.text = str;
    }
}
