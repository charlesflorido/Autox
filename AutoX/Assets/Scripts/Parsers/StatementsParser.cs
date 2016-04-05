using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class StatementsParser : Parser
{
    private static string match = "";
    private Parser[] parsers;

    private bool canParse;

    public StatementsParser(string parseString)
    {
        this.parseString = parseString;
        this.canParse = false;
        parsers = getParsers();
    }

    public StatementsParser(string parseString, int lineNumber)
    {
        this.parseString = parseString;
        this.canParse = false;
        parsers = getParsers();
        this.lineNumber = lineNumber;
    }


    public override int parse()
    {
        foreach(Parser x in parsers)
        {
            x.parse();
        }

        return 0;
    }

    public override bool shouldParse()
    {
        bool ret = true;

        if (canParse)
        {
            for(int i = 0; i < parsers.Length && ret; ++i)
            {
                ret = parsers[i].shouldParse();
            }
        }
        else
        {
            ret = false;
        }

        return ret;
    }

    private Parser[] getParsers()
    {

        List<Parser> parsers = new List<Parser>();

        LineByLine lines = new LineByLine(parseString);
        canParse = true;

        int count = 0;

        while (lines.HasNext() && canParse)
        {
            count++;
            Token token = new Token(lines.Next());

            Debug.Log(":: " + token.getToken());
                       
            if(token.getType() == TokenType.IF)
            {
                Debug.Log("IF [statement] : " + token.getToken());

                IFParser partialIFParser = new IFParser();
                IFBlockParser tempIFBlock = new IFBlockParser();
                int level = 1;
                string statements = "";

                partialIFParser.lineNumber = lineNumber + count;
                tempIFBlock.lineNumber = lineNumber + count;

                tempIFBlock.SetCondition(token.getToken(), TokenType.IF);

                while(lines.HasNext() && level > 0)
                {
                    token = new Token(lines.Next());
                    
                    if(token.getType() == TokenType.IF)
                    {
                        level++;
                        statements += token.getToken() + "\n";
                    }
                    else if(token.getType() == TokenType.ELIF)
                    {
                        if(level == 1)
                        {
                            tempIFBlock.SetStatements(statements);
                            partialIFParser.AddBlock(tempIFBlock);

                            tempIFBlock = new IFBlockParser();
                            tempIFBlock.lineNumber = lineNumber + count;
                            tempIFBlock.SetCondition(token.getToken(), TokenType.ELIF);
                            statements = "";
                        }
                        else
                        {
                            statements += token.getToken() + "\n";
                        }
                    }
                    else if(token.getType() == TokenType.ENDIF)
                    {
                        level--;

                        if(level <= 0)
                        {
                            Debug.Log("[statements]: " + statements);
                            tempIFBlock.SetStatements(statements);
                            partialIFParser.AddBlock(tempIFBlock);
                            statements = "";
                        }
                        else
                        {
                            statements += token.getToken() + "\n";
                        }
                    }
                    else
                    {
                        statements += token.getToken() + "\n";
                    }
                }

                if(level != 0)
                {
                    canParse = false;
                }
                else
                {
                    parsers.Add(partialIFParser);
                }
            }
            else if (token.getType() == TokenType.METHOD)
            {
                parsers.Add(new MethodParser(token.getToken(), lineNumber + count));
                Debug.Log("METHOD [statement] : " + token.getToken());
            }
            else if (token.getType() == TokenType.ASSIGNMENT)
            {
                parsers.Add(new AssignmentParser(token.getToken(), lineNumber + count));
            }
            else if (token.getToken().Trim() == "") { Debug.Log("EMPTY [statement] : " + token.getToken()); }
            else { DebugPanelController.instance.AddError(lineNumber + count, "Unkown Statement"); canParse = false; }
        }


        return parsers.ToArray();
    }

    private static bool isMethod(Token token)
    {
        return (token.getType() == TokenType.METHOD) ? true : false;
    }
}
