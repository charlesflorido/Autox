using UnityEngine;
using System.Collections;

public abstract class Executable : MonoBehaviour {

    public int lineNumber { get; set; }

    public abstract void execute();
}
