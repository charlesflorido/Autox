using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class LineByLine {

    private string[] lines;
    private int current;

    public LineByLine(string str)
    {
        lines = Regex.Split(str, "\n");
        current = -1;
    }

    private bool IsIndexValid(int x)
    {
        return (x >= 0 && x < lines.Length) ? true : false;
    }

    public bool HasNext()
    {
        return (current < lines.Length - 1)? true : false;
    }

    public bool HasPrev()
    {
        return (current > 0) ? true : false;
    }

    public string Next()
    {
        string ret = "";

        current++;

        if (IsIndexValid(current))
        {
            ret =  lines[current];
        }

        return ret;
    }

    public string Prev()
    {
        string ret = "";

        current--;

        if (IsIndexValid(current))
        {
            ret = lines[current];
        }

        return ret;
    }

}
