using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class ConditionParser : Parser
{
    Token[] tokens;

    public ConditionParser(string str)
    {
        tokens = new Token[0];
        parseString = str;
    }

    public ConditionParser(string str, int lineNumber)
    {
        tokens = new Token[0];
        parseString = str;
        this.lineNumber = lineNumber;
    }

    public override bool shouldParse()
    {
        bool ret = true;

        tokens = tokenize(parseString);

        if (tokens.Length > 0 && tokens.Length % 2 == 1)
        {
            for(int i = 0; i < tokens.Length; i++)
            {
                if(i%2 == 0)
                {
                    if (!isRelational(tokens[i]))
                    {
                        ret = false;
                        ErrorTypes.CONDITION_ERROR.printError(lineNumber);
                        break;
                    }
                    else
                    {

                        tokens[i].parser = new RelationParser(tokens[i].getToken(), lineNumber);
                        if(tokens[i].parser.shouldParse() == false)
                        {
                            ret = false;
                            Debug.Log("Cannot parse: " + tokens[i].getToken());
                            break;
                        }
                    }
                }
                else
                {
                    if (!isConditional(tokens[i]))
                    {
                        ret = false;
                        ErrorTypes.CONDITION_ERROR.printError(lineNumber);
                        break;
                    }
                   
                }
            }
        }
        else
        {
            ErrorTypes.CONDITION_ERROR.printError(lineNumber);
            ret = false;
        }


        if(ret == false)
        {
           
            tokens = new Token[0];
        }

        

        return ret;
    }


    //WARNING! call shouldParse() first before calling this
    public override int parse()
    {

        int ret = 0;

        shouldParse();
    

        if(tokens.Length > 0 && tokens.Length%2 == 1)
        {

            if (tokens.Length == 1)
            {
                ret = tokens[0].parser.parse();
            }
            else
            {
                Token[] conds = takeOddTokens(true, tokens);
                Token[] stats = takeOddTokens(false, tokens);

                int temp = operate(stats[0].parser.parse(), stats[1].parser.parse(), conds[0]);

                int indxConds = 1;
                int indxStats = 2;

                while(indxConds > conds.Length && indxStats > stats.Length)
                {
                    temp = operate(temp, stats[indxStats].parser.parse(), conds[indxConds]);
                    ++indxConds;
                    ++indxStats;
                }

                ret = temp;
            }


        }


        return ret;
    }

    private int operate(int x1, int x2, Token operation)
    {
        int ret = 0;

        if(operation.getType() == TokenType.OP_AND)
        {
            ret = x1 * x2;
        }
        else
        {
            ret = x1 + x2;
        }

        ret = (ret != 0) ? 1 : 0;

        return ret;
    }

    private Token[] tokenize(string str)
    {
        Tokenizer tokenizer = new Tokenizer();

        str = correctify(str);
        string[] strArray = Regex.Split(str, "\\$");

        for(int i = 0; i < strArray.Length; ++i)
        {
            strArray[i] = strArray[i].Trim();
        }

        foreach(string x in strArray)
        {
            tokenizer.add(new Token(x));
        }


        return tokenizer.toArray();
    }

    private Token[] takeOddTokens(bool isOdd, Token[] tokens)
    {
        List<Token> tokenList = new List<Token>();
        int start = (isOdd) ? 1 : 0;

        for(;start < tokens.Length; start += 2)
        {
            tokenList.Add(tokens[start]);
        }


        return tokenList.ToArray();
    }

    private string correctify(string oldStr)
    {
        string str = oldStr;

        str = str.Trim();
        str = Regex.Replace(str, "&&", "$ && $");
        str = Regex.Replace(str, "\\|\\|", "$||$");
        str = Regex.Replace(str, "\\s\\s*", " ");
        str = str.Trim();
        return str;
    }

    private static bool isConditional(Token token)
    {
        return (token.getType() == TokenType.OP_AND || token.getType() == TokenType.OP_OR) ? true : false;
    }

    private static bool isRelational(Token token)
    {
        return (token.getType() == TokenType.METHOD || token.getType() == TokenType.INTEGER || token.getType() == TokenType.INVALID) ? true : false;
    }
}
