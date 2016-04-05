using UnityEngine;
using System.Collections;
using System;

public class IFBlockParser : Parser
{
    private ConditionParser condition;
    private StatementsParser statement;


    public void SetCondition(string condStr, TokenType type)
    {
        condStr = condStr.Trim();

        if(type == TokenType.IF)
        {
            condStr = condStr.Remove(0, 2);
        }
        else
        {
            condStr = condStr.Remove(0, 4);
        }

        condStr = condStr.Trim();
        condition = new ConditionParser(condStr, lineNumber);
        
    }

    public void SetStatements(string statementStr)
    {
        statement = new StatementsParser(statementStr, lineNumber);
    }


    public override int parse()
    {
        int ret = condition.parse();
        Debug.Log("Condition: " + ret);

        if (ret != 0)
        {
            statement.parse();
        }

        return ret;
    }

    public override bool shouldParse()
    {
        bool ret = condition.shouldParse() && statement.shouldParse();
        return ret;
    }

}
