using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class VariableParser : Parser
{

    public VariableParser(string parseString)
    {
        this.parseString = parseString.Trim();
    }

    public VariableParser(string parseString, int lineNumber)
    {
        this.parseString = parseString.Trim();
        this.lineNumber = lineNumber;
    }

    public override int parse()
    {
        return GlobalVariables.GetVariable(parseString);
    }

    public override bool shouldParse()
    {
        bool ret = false;

        if (Regex.IsMatch(parseString, TokenType.VARIABLE.getPattern()))
        {
            ret = GlobalVariables.IsVariableExist(parseString);
           
        }
        
        if(ret == false)
        {
            DebugPanelController.instance.AddError(lineNumber, " The variable " + parseString + " was not declared");
        }


        
        return ret;
    }
}
