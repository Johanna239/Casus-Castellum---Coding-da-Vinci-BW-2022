using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grosserSteckbrief : MonoBehaviour
{
    SpriteRenderer spriteRendererSteckbrief;
    Animator anim;
    bool clickable;

    void Start()
    {
        this.spriteRendererSteckbrief = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (openSteckbrief.steckbriefOffen)
            {
                spriteRendererSteckbrief.enabled = true;
                clickable = true;
            }
        }
    }

    void OnMouseDown()
    {
        if (clickable)
        {
            anim.SetTrigger("moveSteckbrief");
              openSteckbrief.steckbriefStimme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            openSteckbrief.steckbriefStimme.release();

            FMODUnity.RuntimeManager.PlayOneShot("event:/BUtton2paper");
            // spriteRendererSteckbrief.enabled = false;
            Destroy(gameObject, 2f);
        }
    }
}
