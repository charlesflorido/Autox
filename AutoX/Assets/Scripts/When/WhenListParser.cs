using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class WhenListParser : Parser
{
    WhenParser[] whenParsers;
    
    public WhenListParser(string parseString)
    {
        this.parseString = parseString;
        whenParsers = new WhenParser[0];
    }

    public WhenStructure[] getWhenStructures()
    {
        WhenCollection structures = new WhenCollection();

        string tempString = parseString;
        string[] stringArr = Regex.Split(tempString, "\n");


        structures.setStatus(true);

        for (int i = 0; i < stringArr.Length && structures.getStatus(); i++)
        {
            string temp = stringArr[i];
            temp = temp.Trim();

            if (temp.Equals(string.Empty))
            {

            }
            else if (Regex.IsMatch(temp, "^when(.*)"))
            {
                WhenStructure whenStruct = new WhenStructure(WhenStructure.ExtractCondition(temp), lineNumber + i);
                string statements = "";
                i++;

                while (i < stringArr.Length)
                {
                    if (Regex.IsMatch(stringArr[i], "^when(.*)"))
                    {
                        break;
                    }
                    else
                    {
                        statements += stringArr[i] + "\n";
                        i++;
                    }
                }
                whenStruct.addStatement(statements);
                structures.Add(whenStruct);
                i--;
            }
            else
            {
                structures.setStatus(false);
            }
        }


        return structures.toArray();
    }

    public override int parse()
    {

        foreach(WhenParser whenParser in whenParsers)
        {
            whenParser.parse();
        }


        return 0;
    }

    public override bool shouldParse()
    {
        WhenStructure[] whenStructs = getWhenStructures();
        whenParsers = new WhenParser[whenStructs.Length];
        bool ret = true;

        for (int i = 0; i < whenStructs.Length && ret; ++i)
        {
            whenParsers[i] = new WhenParser(whenStructs[i]);
            ret = whenParsers[i].shouldParse();
        }

        if(ret == false)
        {
            whenParsers = new WhenParser[0];
        }

        return ret;
    }
}
