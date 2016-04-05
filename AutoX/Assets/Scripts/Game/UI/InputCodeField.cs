using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
public class InputCodeField : MonoBehaviour {

    private InputField codeField;
    public InputField numberField;
    private int numlines = 0;

	// Use this for initialization
	void Start () {
        codeField = GetComponent<InputField>();


        codeField.text = "";
        numberField.text = "0";
	}
	
	public void inputChanged()
    {

        string test = codeField.text;
        int currentNumLines = test.Where(x => x == '\n').Count();

        if (currentNumLines != numlines)
        {
            numlines = currentNumLines;
            updateNumLines();
            
        }

    }

    private void updateNumLines()
    {
        string text = "";

        for (int i = 0; i <= numlines; i++)
        {
            text += i + "\n";
        }

        numberField.text = text;
        numberField.caretPosition = text.Length;
    }

    
}
