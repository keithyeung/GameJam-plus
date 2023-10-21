using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    [SerializeField] private int _side;
    private List<GameObject> _objects = new List<GameObject>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<LibraScale>().AddObject(_side);
            print("on " + collision.name);
        }
        else if (collision.tag == "PickUp" && collision.name != "Trigger")
        {
            GetComponentInParent<LibraScale>().AddObject(_side);
            print("on " + collision.name);
        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<LibraScale>().RemoveObject(_side);
            print("off " + collision.name);
        }
        else if (collision.tag == "PickUp" && collision.name != "Trigger")
        {
            GetComponentInParent<LibraScale>().RemoveObject(_side);
            print("off " + collision.name);
        }

    }
}
