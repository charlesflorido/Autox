using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tokenizer {

    private List<Token> tokens;
   
    public int currentIndx = 0;

    public Tokenizer()
    {
        tokens = new List<Token>();
    }

    public void add(Token token)
    {
        tokens.Add(token);
    }

    public Token next()
    {
        if (hasNext())
        {
            currentIndx++;
        }
        
        return tokens[currentIndx];
    }

    public Token prev()
    {
        if (hasPrev())
        {
            currentIndx--;
        }

        return tokens[currentIndx];
    }

    public bool hasNext()
    {
        return (currentIndx > tokens.Count - 1) ? false : true;
    }

    public bool hasPrev()
    {
        return (tokens.Count > 0 && currentIndx > 0) ? true : false;
    }

    public Token[] toArray()
    {
        return tokens.ToArray();
    }


}
