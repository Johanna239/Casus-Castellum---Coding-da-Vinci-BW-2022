using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuschTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    bool triggerValue;
    RaycastHit hit;
    public static bool schwertGefunden;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        schwertGefunden = false;
    }

    void Update()
    {
        triggerValue = anim.GetBool("Active");
    }

    void OnMouseUp()
    {
        if (!triggerValue)
        {
            anim.SetTrigger("Active");
            if (!schwertGefunden)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sprache/In diesem Gebuesch");
                schwertGefunden = true;
            }
        }
    }
}
