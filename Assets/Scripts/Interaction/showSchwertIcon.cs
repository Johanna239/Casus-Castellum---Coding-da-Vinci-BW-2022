using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showSchwertIcon : MonoBehaviour
{
    SpriteRenderer schwertIconRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.schwertIconRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openHub.hubOpen)
        {
            if (grossesSchwert.schwertErhalten)
            {
                // enable Sprite Renderer
                schwertIconRenderer.enabled = true;
            }
        }
        else
        {
            // disable Sprite Renderer
            schwertIconRenderer.enabled = false;
        }
    }
}
