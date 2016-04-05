using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class AssignmentParser : Parser
{
    VariableParser variableParser;
    Parser valueParser;

    public AssignmentParser(string parseString)
    {
        this.parseString = parseString;
    }

    public AssignmentParser(string parseString, int lineNumber)
    {
        this.parseString = parseString;
        this.lineNumber = lineNumber;
    }

    public override int parse()
    {
        int x = valueParser.parse();
        GlobalVariables.AssignVariable(variableParser.parseString, x);

        return x;
    }

    public override bool shouldParse()
    {
        bool ret = false;

        if(Regex.IsMatch(parseString, TokenType.ASSIGNMENT.getPattern()))
        {
            string[] matches = Regex.Split(parseString, "=");
            string varName = matches[0].Trim();
            string varValue = matches[1].Trim();

            variableParser = new VariableParser(varName);

            if(varValue != "")
            {
                Token token = new Token(varValue);

                if(token.getType() == TokenType.METHOD)
                {
                    valueParser = new MethodParser(varValue);
                }
                else if(token.getType() == TokenType.VARIABLE)
                {
                    valueParser = new VariableParser(varValue);
                }
                else if(token.getType() == TokenType.INTEGER)
                {
                    valueParser = new IntegerParser(varValue);
                }
                else
                {
                    valueParser = new DefaultParser();
                }

                ret = valueParser.shouldParse();


                if (ret)
                {
                    GlobalVariables.AssignVariable(variableParser.parseString, 0);
                }
            }
        }

        return ret;
    }
}
