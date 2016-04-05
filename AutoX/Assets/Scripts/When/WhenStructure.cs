using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class WhenStructure {

    public string condition;
    public string statements;
    public int lineNumber;

    public WhenStructure(string condition)
    {
        this.condition = condition;
        this.statements = "";
        this.lineNumber = 0;
    }

    public WhenStructure(string condition, int line)
    {
        this.condition = condition;
        this.statements = "";
        this.lineNumber = line;
    }

    public void addStatement(string str)
    {
        this.statements = str;
    }

    public string printWhen()
    {
        string str = "";


        str += "< Condition >: " + condition + "\n";
        str += statements;


        return str;
    }

    public static string ExtractCondition(string str)
    {
        string temp = str;

        temp = temp.Trim();
        temp = temp.Remove(0, 4);
        temp = temp.Trim();

        return temp;
    }

}
