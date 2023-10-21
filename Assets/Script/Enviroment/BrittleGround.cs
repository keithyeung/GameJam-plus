using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrittleGround : MonoBehaviour
{
    private List<GameObject> objects = new List<GameObject>();


    private void Crumble()
    {
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D col in colliders)
        {
            col.enabled = false;
        }

        GetComponent<SpriteRenderer>().enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            objects.Add(collision.gameObject);
        }
        else if (collision.tag == "PickUp")
        {
            objects.Add(collision.gameObject);
        }

        if (objects.Count >= 2)
        {
            Invoke("Crumble", 2);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            objects.Remove(collision.gameObject);
        }
        else if (collision.tag == "PickUp")
        {
            objects.Remove(collision.gameObject);
        }
    }

}
