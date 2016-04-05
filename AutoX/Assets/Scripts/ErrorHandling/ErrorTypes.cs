using UnityEngine;
using System.Collections;

public class ErrorTypes {

    public static readonly ErrorTypes CONDITION_ERROR = new ErrorTypes("Condition Error");
    public static readonly ErrorTypes RELATION_ERROR = new ErrorTypes("Relation Error");
    public static readonly ErrorTypes INTEGER_ERROR = new ErrorTypes("Not an integer");
    public static readonly ErrorTypes METHOD_ERROR = new ErrorTypes("Unkown Method or Invalid Arguments");
    public static readonly ErrorTypes UNKNOWN_METHOD = new ErrorTypes("Unknown method");
    public static readonly ErrorTypes INVALID_ARGUMENT = new ErrorTypes("Invalid argument");
    public static readonly ErrorTypes WHEN_ERROR = new ErrorTypes("When syntax error");
    public static readonly ErrorTypes RUNTIME_ERROR = new ErrorTypes("Runtime error");
    public static readonly ErrorTypes UNKNOWN_ERROR = new ErrorTypes("Unknown error");


    private string message;

    ErrorTypes(string message)
    {
        this.message = message;
    }

    public string printError()
    {
        Debug.Log(message);
        return message;
    }

    public string printError(int line)
    {
        DebugPanelController.instance.AddError(line, message);
        Debug.Log("Line " + line + ": "+ message);
        return message;
    }
}
