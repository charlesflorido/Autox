using UnityEngine;
using System.Collections;

public class Token{

    private string token;
    private TokenType type;
    public Parser parser { get; set; }

    public Token(string token)
    {
        setToken(token);
        type = TokenType.getTokenType(token);
    }

    public void setToken(string token)
    {
        this.token = token;
    }

    public string getToken()
    {
        return token;
    }
    
    public TokenType getType()
    {
        return type;
    }

}
