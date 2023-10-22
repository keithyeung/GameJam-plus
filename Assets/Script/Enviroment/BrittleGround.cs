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

        AudioManager.instance.Play("BrittleGroundBreaking");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            objects.Add(collision.gameObject);
            collision.gameObject.GetComponent<PlayerMovement>().OnBrittleGround(true);
        }
        else if (collision.tag == "PickUp")
        {
            objects.Add(collision.gameObject);
        }

        if (objects.Count >= 2)
        {
            Invoke("Crumble", 0.25f);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            objects.Remove(collision.gameObject);
            collision.gameObject.GetComponent<PlayerMovement>().OnBrittleGround(false);
        }
        else if (collision.tag == "PickUp")
        {
            objects.Remove(collision.gameObject);
        }
    }

}
