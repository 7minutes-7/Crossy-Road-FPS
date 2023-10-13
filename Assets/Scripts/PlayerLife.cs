using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerLife : MonoBehaviour
{
    int CAR_LAYER = 8;
    [SerializeField] float player_range_x = 13f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Water") || collision.gameObject.layer == CAR_LAYER)
        {
            Die();
        }
    }

    private void Die()
    {
        // Make player invisible
        GetComponent<MeshRenderer>().enabled = false;

        // Disable physics
        GetComponent<Rigidbody>().isKinematic = true;

        // Stop player movement
        GetComponent<FirstPersonController>().enabled = false;


        // The method will be called after a certain amount of time
        Invoke(nameof(ReloadLevel), 1.3f);
    }

    private void ReloadLevel()
    {
        // Reolad at the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (transform.position.z > 0)
        {
            if (transform.position.x >= player_range_x || transform.position.x <= -player_range_x)
            {
                Die();
            }
        }
    }
}
