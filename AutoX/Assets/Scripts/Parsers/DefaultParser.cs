using UnityEngine;
using System.Collections;
using System;

public class DefaultParser : Parser
{

    public DefaultParser()
    {
        parseString = "";
    }

    public DefaultParser(int x)
    {
        parseString = "";
        this.lineNumber = x;
    }

    public override int parse()
    {
        return 0;
    }

    public override bool shouldParse()
    {
        return false;
    }
}
