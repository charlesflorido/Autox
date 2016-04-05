using UnityEngine;
using System.Collections.Generic;

public class WhenCollection {

    private List<WhenStructure> whens;
    private bool status;

    public WhenCollection()
    {
        whens = new List<WhenStructure>();
        status = true;
    }

    public void setStatus(bool status)
    {
        this.status = status;
    }

    public bool getStatus()
    {
        return status;
    }

    public void Add(WhenStructure when)
    {
        whens.Add(when);
    }

    public WhenStructure[] toArray()
    {
        return whens.ToArray();
    }
}
