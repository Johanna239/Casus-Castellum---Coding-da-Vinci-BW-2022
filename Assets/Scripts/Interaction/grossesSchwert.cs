using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grossesSchwert : MonoBehaviour
{
    public static bool schwertErhalten;
    SpriteRenderer spriteRendererSchwert;
    bool clickable;
    Animator anim;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRendererSchwert = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        schwertErhalten = false;
        clickable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (schwertErhalten)
        {
            clickable = true;
            spriteRendererSchwert.enabled = true;
            boxCollider.enabled = true;
        }
    }

    void OnMouseDown()
    {
        if (clickable)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sword");
            anim.SetTrigger("schwert_einsammeln");
            FMODUnity.RuntimeManager.PlayOneShot("event:/BUtton2paper");
            GameState.fulfilledRequirement("schwert");
            // spriteRendererSteckbrief.enabled = false;
            Destroy(gameObject, 2f);
        }
    }

    public void SwordClicked(bool clicked) {
        schwertErhalten = clicked;
    }
}
