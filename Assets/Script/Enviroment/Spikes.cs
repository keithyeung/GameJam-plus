using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.instance.Play("Death");
            GameManager.instance.Restart();
        }
        else if (collision.name == "Plant")
        {
            AudioManager.instance.Play("PlantDeath");
            GameManager.instance.Restart();
        }
    }
}
