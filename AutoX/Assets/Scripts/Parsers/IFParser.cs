using UnityEngine;
using System.Collections.Generic;
using System;

public class IFParser : Parser
{
   private List<IFBlockParser> ifBlocks;

    public IFParser()
    {
        ifBlocks = new List<IFBlockParser>();
    }
    

    public override int parse()
    {
        foreach(IFBlockParser block in ifBlocks)
        {
            if(block.parse() != 0)
            {
                break;
            }
        }

        return 1;
    }

    public override bool shouldParse()
    {
        foreach(IFBlockParser x in ifBlocks)
        {
            if (x.shouldParse() == false)
                return false;
        }
        return true;
    }

    public void AddBlock(IFBlockParser block)
    {
        ifBlocks.Add(block);
    }
    

    
}
