using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlanting : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Plant")
        {
            collision.GetComponent<Animator>().Play("TurnToTree");
        }
    }
}
