using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] GameObject blinkGameObject;
    [SerializeField] private float frequency = 1.0f;
    private float timer = 0f;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5 * frequency)
        {
            blinkGameObject.SetActive(false);

        }

        if (timer >= frequency)
        {
            blinkGameObject.SetActive(true);
            timer = 0f;
        }
    }
}
