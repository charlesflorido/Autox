using UnityEngine;
using System.Collections;

[System.Serializable]
public class VariableModel{

    private string variableName;
    public int Value { get; set; }

    public VariableModel(string variableName, int value)
    {
        this.variableName = variableName;
        this.Value = value;
    }

    public string getName()
    {
        return variableName;
    }
}
