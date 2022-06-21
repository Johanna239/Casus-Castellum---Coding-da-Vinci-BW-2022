using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showSteckbriefIcon : MonoBehaviour
{
    SpriteRenderer steckbriefIconRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.steckbriefIconRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openHub.hubOpen)
        {
            if (openSteckbrief.steckbriefOffen)
            {
                // enable Sprite Renderer
                steckbriefIconRenderer.enabled = true;
            }
        }
        else
        {
            // disable Sprite Renderer
            steckbriefIconRenderer.enabled = false;
        }
    }
}
