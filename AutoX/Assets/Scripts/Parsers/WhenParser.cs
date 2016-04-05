using UnityEngine;
using System.Collections.Generic;
using System;

public class WhenParser : Parser
{
    ConditionParser condition;
    StatementsParser statements;

    public WhenParser(WhenStructure when)
    {
        condition = new ConditionParser(when.condition, lineNumber);
        statements = new StatementsParser(when.statements);
    }

    public override int parse()
    {
        if(condition.parse() != 0)
        {
            statements.parse();
        }
      
        return 0;
    }

    public override bool shouldParse()
    {
        return condition.shouldParse() && statements.shouldParse();
    }
}
