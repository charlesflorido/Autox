using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputController : MonoBehaviour {

    public static InputController instance;

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

}
