using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Grammar{

    private static bool compiled = false;
    private static WhenListParser whenListParser;

    public static bool compile()
    {
        GlobalVariables.Reset();
        DebugPanelController.instance.Reset();

        GlobalVariables.DeclareAllVariables(correctifyText(InputController.instance.getText()));
        
        whenListParser = new WhenListParser(correctifyText(InputController.instance.getText()));
        whenListParser.lineNumber = 0;

        compiled = whenListParser.shouldParse();

        return compiled;
    }
    
    public static string correctifyText(string s)
    {
        string x = s.ToLower();

        x = Regex.Replace(x, "true", "1");
        x = Regex.Replace(x, "false", "0");

        x = Regex.Replace(x, "gear//.neutral", "0");
        x = Regex.Replace(x, "gear//.1", "1");
        x = Regex.Replace(x, "gear//.2", "2");
        x = Regex.Replace(x, "gear//.3", "3");
        x = Regex.Replace(x, "gear//.4", "4");
        x = Regex.Replace(x, "gear//.reverse", "5");

        Debug.Log(x);
        return x;
    }


    public static void run()
    {
        if (compiled)
        {
            whenListParser.parse();
            Debug.Log("running");
        }
    }

    public static void compileAndRun()
    {
        if (compile())
        {
            compiled = true;
            run();
        }
        else
        {
            compiled = false;
        }
    }




}
