using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Plant")
        {
            GameManager.instance.Checkpoint(_spawnPoint.position);
            collision.GetComponent<Plant>().Grow();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
