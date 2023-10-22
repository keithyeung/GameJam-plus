using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [SerializeField] private Platform _platform;
    private bool _isTriggered = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered == false)
        {
            if (collision.tag == "Player")
            {
                _platform.StartMoving();
                _isTriggered = true;
                GetComponentInParent<Animator>().Play("ButtonAnim");
            }
            else if (collision.tag == "PickUp")
            {
                _platform.StartMoving();
                _isTriggered = true;
                GetComponentInParent<Animator>().Play("ButtonAnim");
            }
        }
    }

}
