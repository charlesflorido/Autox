using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class TokenType {

    public static readonly TokenType INVALID = new TokenType(".*");
    public static readonly TokenType OP_GREATER = new TokenType("^\\s*(>)\\s*$");
    public static readonly TokenType OP_GREATER_E = new TokenType("^\\s*(>=)\\s*$");
    public static readonly TokenType OP_LESS = new TokenType("^\\s*(<)\\s*$");
    public static readonly TokenType OP_LESS_E = new TokenType("^\\s*(>=)\\s*$");
    public static readonly TokenType OP_EQUAL = new TokenType("^\\s*(==)\\s*$");
    public static readonly TokenType OP_NOT = new TokenType("^\\s*(!=)\\s*$");
    public static readonly TokenType OP_OR = new TokenType("^\\s*(\\|\\|)\\s*$");
    public static readonly TokenType OP_AND = new TokenType("^\\s*(&&)\\s*$");
    public static readonly TokenType METHOD = new TokenType("^\\s*[a-zA-Z][a-zA-Z0-9]*\\((.*)\\)\\s*$");
    public static readonly TokenType INTEGER = new TokenType("^\\s*[0-9][0-9]*\\s*$");

    public static readonly TokenType IF = new TokenType("^\\s*if(.*)");
    public static readonly TokenType ELIF = new TokenType("^\\s*elif(.*)");
    public static readonly TokenType ENDIF = new TokenType("^\\s*endif\\s*$");

    public static readonly TokenType VARIABLE = new TokenType("\\s*#[a-zA-z][a-zA-Z0-9]*\\s*");
    public static readonly TokenType ASSIGNMENT = new TokenType("^\\s*#[a-zA-z][a-zA-Z0-9]*\\s*=\\s*(.*)");

    private string pattern;

    TokenType(string pattern){
        this.pattern = pattern;
    }

    public bool isMatch(string str)
    {
        
        return Regex.IsMatch(str, pattern);
    }

    public static TokenType getTokenType(string str)
    {
        if (OP_GREATER.isMatch(str)) { return OP_GREATER; }
        else if (METHOD.isMatch(str)) { return METHOD; }
        else if (ASSIGNMENT.isMatch(str)) { return ASSIGNMENT; }
        else if (OP_GREATER_E.isMatch(str)) { return OP_GREATER_E; }
        else if (     OP_LESS.isMatch(str)) { return OP_LESS;      }
        else if (   OP_LESS_E.isMatch(str)) { return OP_LESS_E;    }
        else if (    OP_EQUAL.isMatch(str)) { return OP_EQUAL;     }
        else if (      OP_NOT.isMatch(str)) { return OP_NOT;       }
        else if (       OP_OR.isMatch(str)) { return OP_OR;        }
        else if (      OP_AND.isMatch(str)) { return OP_AND;       }
        else if (    VARIABLE.isMatch(str)) { return VARIABLE;     }
        else if (          IF.isMatch(str)) { return IF;           }
        else if (        ELIF.isMatch(str)) { return ELIF;         }
        else if (       ENDIF.isMatch(str)) { return ENDIF;        }
        else if (     INTEGER.isMatch(str)) { return INTEGER;      }
        
        else { return INVALID; }
    }

    public string getPattern()
    {
        return pattern;
    }

    public override string ToString()
    {
        return pattern;
    }
    
}
