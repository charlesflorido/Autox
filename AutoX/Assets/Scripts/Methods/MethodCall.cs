using UnityEngine;
using System.Collections;

public class MethodCall {

    public string name { get; private set; }
    public int[] parameters;
    public int returnValue { get; set; }

    public MethodCall(string name)
    {
        this.name = name;
        this.parameters = new int[0];
        returnValue = 0;
    }

    public MethodCall(string name, params int[] values)
    {
        this.name = name;
        this.parameters = values;
        returnValue = 0;
    }

    public int execute()
    {
        GlobalMethods.ExecuteMethod(this);
        return returnValue;
    }

    public bool test()
    {
        return GlobalMethods.TestMethod(this);
    }

    public bool test(int length)
    {
        parameters = new int[length];
        return GlobalMethods.TestMethod(this);
    }

    public void printParameters()
    {
        
    }

    public void printError()
    {
        string str = "Method Error of method " + name + "";

        str += "    with parameters";
        foreach(int x in parameters)
        {
            str += "          " + x + " ,"; 
        }
        Debug.Log(str);
    }

}
