using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") )
        {
            // Make player child of the platform
            collision.gameObject.transform.SetParent(transform,true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unchild the player from the platform
            collision.gameObject.transform.SetParent(null,true);
            
        }
    }
}
