using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GlobalVariables {

    private static List<VariableModel> variables = new List<VariableModel>();

    public static void AssignVariable(string name, int value)
    {
        bool varExist = false;
        name = name.Trim();

        foreach (VariableModel varModel in variables)
        {

            if(varModel.getName() == name)
            {
                varModel.Value = value;
                varExist = true;
                break;
            }
        }

        if (varExist == false)
        {
            VariableModel newVariable = new VariableModel(name, value);
            variables.Add(newVariable);
            Debug.Log("the variable " + name + " has been assigned to the value of " + value);
        }


    }

    public static bool IsVariableExist(string name)
    {
        bool ret = false;
        name = name.Trim();
        foreach(VariableModel varModel in variables)
        {
            if(varModel.getName().Equals(name))
            {
                ret = true;
                break;
            }
        }

        return ret;
    }

    public static int GetVariable(string name)
    {
        int value = -1;

        foreach (VariableModel varModel in variables)
        {
            if (varModel.getName() == name)
            {
                value = varModel.Value;
                break;
            }
        }

        return value;
    }

    public static void Reset()
    {
        variables = new List<VariableModel>();
    }

    public static void DeclareAllVariables(string str)
    {
        string[] statements = Regex.Split(str, "\n");

        foreach(string x in statements)
        {
            if(Regex.IsMatch(x, TokenType.ASSIGNMENT.getPattern()))
            {
                string varStr = Regex.Split(x, "=")[0].Trim();
                AssignVariable(varStr, 0);

            }
        }
    }


}
