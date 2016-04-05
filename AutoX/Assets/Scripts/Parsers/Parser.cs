using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Parser {

    public string parseString { get; set; }
    public int lineNumber { get; set; }

    public Parser()
    {
        parseString = "";
        lineNumber = 0;
    }

    public abstract bool shouldParse();
    public abstract int parse();

    
}
