using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class RelationParser : Parser
{
    Token[] tokens;

    public RelationParser(string str)
    {
        tokens = new Token[0];
        parseString = str;
    }

    public RelationParser(string str, int lineNumber)
    {
        tokens = new Token[0];
        parseString = str;
        this.lineNumber = lineNumber;
    }

    public override bool shouldParse()
    {
        bool ret = true;

        tokens = tokenize(parseString);

        if(tokens.Length == 1 || tokens.Length == 3){
            for(int i = 0; i < tokens.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (isInteger(tokens[i])) { tokens[i].parser = new IntegerParser(tokens[i].getToken(), lineNumber); }
                    else if (isMethod(tokens[i])) { tokens[i].parser = new MethodParser(tokens[i].getToken(), lineNumber); }
                    else if (tokens[i].getType() == TokenType.VARIABLE) { tokens[i].parser = new VariableParser(tokens[i].getToken(), lineNumber); }
                    else

                    {
                        tokens[i].parser = new DefaultParser(lineNumber);
                    }

                    if(tokens[i].parser.shouldParse() == false)
                    {
                        ret = false;
                        break;
                    }
                }
                else
                {
                    if (!isRelation(tokens[i]))
                    {
                        ret = false;
                        ErrorTypes.RELATION_ERROR.printError(lineNumber);
                        break;
                    }

                }
            }
        }
        else
        {
            ErrorTypes.RELATION_ERROR.printError(lineNumber);
            ret = false;
        }


        return ret;
    }

    public override int parse()
    {
        int ret = 0;

        if (tokens.Length == 1 || tokens.Length == 3)
        {

            if (tokens.Length == 1)
            {
                ret = tokens[0].parser.parse();
            }
            else
            {

                int temp = operate(tokens[0].parser.parse(), tokens[2].parser.parse(), tokens[1]);

                ret = temp;
            }


        }


        return ret;
    }

    private int operate(int x1, int x2, Token operation)
    {
        int ret = 0;

        int result = x1 - x2;
        bool retBool = false;

        if(operation.getType() == TokenType.OP_EQUAL){
            retBool = (result == 0) ? true : false;
        }
        else if (operation.getType() == TokenType.OP_GREATER){
            retBool = (result > 0) ? true : false;
        }
        else if (operation.getType() == TokenType.OP_GREATER_E){
            retBool = (result >= 0) ? true : false;
        }
        else if (operation.getType() == TokenType.OP_LESS){
            retBool = (result < 0) ? true : false;
        }
        else if (operation.getType() == TokenType.OP_LESS_E){
            retBool = (result <= 0) ? true : false;
        }
        else if (operation.getType() == TokenType.OP_NOT){
            retBool = (result != 0) ? true : false;
        }
        else{
            retBool = (result == 0) ? true : false;
        }

        if (retBool)
        {
            ret = 1;
        }

        return ret;
    }

    public string correctify(string oldStr)
    {
        string str = oldStr;

        str = str.Trim();
        str = Regex.Replace(str, ">", "$>$");
        str = Regex.Replace(str, ">=", "$>=$");
        str = Regex.Replace(str, "<", "$<$");
        str = Regex.Replace(str, "<=", "$<=$");
        str = Regex.Replace(str, "!=", "$!=$");
        str = Regex.Replace(str, "==", "$==$");
        str = Regex.Replace(str, "\\s\\s*", " ");
        str = str.Trim();

        return str;
    }

    private Token[] tokenize(string str)
    {
        Tokenizer tokenizer = new Tokenizer();

        str = correctify(str);
        string[] strArray = Regex.Split(str, "\\$");

        for (int i = 0; i < strArray.Length; ++i)
        {
            strArray[i] = strArray[i].Trim();
        }

        foreach (string x in strArray)
        {
            tokenizer.add(new Token(x));
        }


        return tokenizer.toArray();
    }

    private static bool isMethod(Token token)
    {
        return (token.getType() == TokenType.METHOD) ? true : false;
    }

    private static bool isRelation(Token token)
    {
        return (token.getType() == TokenType.OP_EQUAL || token.getType() == TokenType.OP_GREATER || token.getType() == TokenType.OP_GREATER_E || token.getType() == TokenType.OP_LESS || token.getType() == TokenType.OP_LESS_E || token.getType() == TokenType.OP_NOT) ? true : false;
    }

    private static bool isInteger(Token token)
    {
        return (token.getType() == TokenType.INTEGER) ? true : false;
    }
}
