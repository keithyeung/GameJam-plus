using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivationTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    private bool _triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_triggered == false)
        {
            if (collision.tag == "Player")
            {
                foreach (GameObject obj in _objects)
                {
                    obj.SetActive(false);
                    _triggered = true;
                }


                AudioManager.instance.Stop("SongMain");
                AudioManager.instance.Play("SongFinal");
            }
        }

    }
}
