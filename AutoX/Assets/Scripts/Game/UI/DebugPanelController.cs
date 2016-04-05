using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugPanelController : MonoBehaviour {

    public static DebugPanelController instance;

    private InputField input;
    
    void Start()
    {
        input = GetComponent<InputField>();
    }
   
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Reset()
    {
        GetComponent<InputField>().text = "";
    }

    public void AddError(int line, string message)
    {
        string x = "[ERROR IN LINE " + line + " ] " + message + "\n";
        GetComponent<InputField>().text += x;
    }

    public void PrintMessage(int line, string message)
    {
        string x = "[CONSOLE MESSAGE LINE " + line + " ] " + message + "\n";
        GetComponent<InputField>().text += x;
    }

    public void PrintMessage(string message)
    {
        string x = "[CONSOLE LINE] " + message + "\n";
        GetComponent<InputField>().text += x;
    }
}
