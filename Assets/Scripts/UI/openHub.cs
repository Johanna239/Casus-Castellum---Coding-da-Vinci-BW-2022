using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openHub : MonoBehaviour
{
    public static bool hubOpen;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        hubOpen = false;
        this.spriteRenderer = this.transform
            .Find("hub_withIcons_background")
            .GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() { }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (spriteRenderer.enabled)
            {
                spriteRenderer.enabled = false;
                hubOpen = false;
            }
            else
            {
                spriteRenderer.enabled = true;
                hubOpen = true;
            }
        }
    }
}
