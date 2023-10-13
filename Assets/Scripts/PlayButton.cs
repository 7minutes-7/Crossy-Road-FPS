using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject titleText;

    public void onPlayButton()
    {
        player.GetComponent<FirstPersonController>().enabled = true; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        titleText.SetActive(false);
        gameObject.SetActive(false);
    }
}
