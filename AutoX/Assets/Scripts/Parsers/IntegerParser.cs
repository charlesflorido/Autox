using UnityEngine;
using System.Collections;
using System;

public class IntegerParser : Parser
{

    public IntegerParser(string str)
    {
        parseString = str;
    }

    public IntegerParser(string str, int lineNumber)
    {
        parseString = str;
        this.lineNumber = lineNumber;
    }

    public override int parse()
    {
        int ret = 0;

        if(Int32.TryParse(parseString, out ret)) { }
        else
        {
            ret = 0;
            ErrorTypes.INTEGER_ERROR.printError();
        }


        return ret;
    }

    public override bool shouldParse()
    {
        bool ret = false;
        int temp = 0;

        if(Int32.TryParse(parseString, out temp))
        {
            ret = true;
        }
        else
        {
            ErrorTypes.INTEGER_ERROR.printError();
        }

        return ret;
    }
}
