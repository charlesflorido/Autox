using UnityEngine;
using System.Collections;

public class Detectors : MonoBehaviour {

    public bool collide;
    public bool path;

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "block")
        {
            collide = true;
        }

        if (coll.gameObject.tag == "paths")
        {
            path = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "block")
        {
            collide = false;
        }

        if (coll.gameObject.tag == "paths")
        {
            path = false;
        }
    }
}
