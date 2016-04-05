using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class MethodParser : Parser
{
    string methodName;
    Token[] arguments = new Token[0];
    bool parsable;

    public MethodParser(string str)
    {
        parsable = false;

        parseString = str.Trim();
        methodName = getName();
        
        if (parsable)
        {
            parsable = shouldParseArgs();
        }

        
    }

    public MethodParser(string str, int lineNumber)
    {
        parsable = false;

        parseString = str.Trim();
        methodName = getName();

        if (parsable)
        {
            parsable = shouldParseArgs();
        }

        this.lineNumber = lineNumber;
    }

    public override int parse()
    {
        int ret = 0;
        List<int> paramList = new List<int>();

        for(int i = 0; i < arguments.Length; ++i)
        {
            paramList.Add(arguments[i].parser.parse());
        }

        MethodCall call = new MethodCall(methodName, paramList.ToArray());
        ret = call.execute();

        return ret;
    }

    public override bool shouldParse()
    {
        MethodCall call = new MethodCall(methodName);
        parsable = call.test(arguments.Length);

        if(parsable == false)
        {
            call.printError();
            ErrorTypes.METHOD_ERROR.printError(lineNumber);
        }

        return parsable;
    }

    private string getName()
    {
        string str = "";

        Match match = Regex.Match(parseString, "^[a-zA-Z][a-zA-Z0-9]*");

        if (match.Success){
            str = match.Value;
        }

        str = str.Trim();
        if(str != "")
        {
            parsable = true;
        }

        return str;
    }

    private string getArgs()
    {
        string str = "";

        Match match = Regex.Match(parseString, "\\(.*\\)");

        if (match.Success)
        {
            str = match.Value;
        }

  
        return str;
    }

    


    private string[] toStringArgs(string str)
    {
        List<string> list = new List<string>();
        str = str.Trim();

        while (str != "")
        {
            Match match = Regex.Match(str, "(([^\\(]|\\([^\\)]*\\))*?,)*");
            string tempStr = "";
            string newStr = "";
            string token = "";

            if (match.Success)
            {
                if (match.Value.Trim() != "")
                {
                    newStr = match.Value;
                    tempStr = str;
                    token = str.Substring(newStr.Length);

                    list.Add(token.Trim());
                    try
                    {
                        str = newStr.Substring(0, newStr.Length - 1);
                    }
                    catch (System.ArgumentOutOfRangeException ex)
                    {

                    }
                }
                else
                {
                    list.Add(str.Trim());
                    str = "";
                }
                Console.WriteLine("str: [" + str + "]");
            }
            else { str = ""; }

            str = str.Trim();
        }

        list.Reverse();
        return list.ToArray();
    }

    private string correctifyArgs(string oldStr)
    {
        string str = oldStr;

        if (str.Length > 0)
        {
            str = str.Trim();
            str = str.Substring(1, str.Length - 2);

        }
        str = str.Trim();
        return str;
    }

    private bool isArgValid(string str)
    {
        bool ret = true;

        str = str.Trim();

        if (str.Length > 0)
        {
            if (str[0] == ',' || str[str.Length - 1] == ',')
                ret = false;
        }



        return ret;
    }

    private bool shouldParseArgs()
    {
        bool ret = true;

        string args = getArgs();
        args = correctifyArgs(args);

        if (isArgValid(args))
        {
            string[] margs = toStringArgs(args);
            List<Token> tokenList = new List<Token>();

            foreach (string x in margs)
            {
                tokenList.Add(new Token(x));
            }

            Token[] temp = tokenList.ToArray();

            for(int i = 0; i < temp.Length; ++i)
            {
                if(temp[i].getType() == TokenType.METHOD){
                    temp[i].parser = new MethodParser(temp[i].getToken().Trim(), lineNumber);
                }
                else if(temp[i].getType() == TokenType.INTEGER){
                    temp[i].parser = new IntegerParser(temp[i].getToken().Trim(), lineNumber);
                }
                else if(temp[i].getType() == TokenType.VARIABLE)
                {
                    temp[i].parser = new VariableParser(temp[i].getToken().Trim(), lineNumber);
                }
                else
                {
                    temp[i].parser = new DefaultParser(lineNumber);
                   
                }

                Debug.Log("Parameter[" + i + "] -- " + temp[i].getToken() + " --- " + temp[i].getType().ToString() );

                if (temp[i].parser.shouldParse() == false){
                    ret = false;
                    break;
                }
                else{
                    arguments = temp;
                }
                
            }
        }
        else
        {
            ret = false;
        }

        return ret;
    }
}
