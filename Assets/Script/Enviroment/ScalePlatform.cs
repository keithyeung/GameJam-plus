using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    [SerializeField] private int _side;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<LibraScale>().AddObject(_side);
        }
        else if (collision.tag == "PickUp")
        {
            GetComponentInParent<LibraScale>().AddObject(_side);
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<LibraScale>().RemoveObject(_side);
        }
        else if (collision.tag == "PickUp")
        {
            GetComponentInParent<LibraScale>().RemoveObject(_side);
        }

    }
}
