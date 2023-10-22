using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            GetComponentInParent<WormGround>().underPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            GetComponentInParent<WormGround>().underPlatform = false;
        }
    }
}
