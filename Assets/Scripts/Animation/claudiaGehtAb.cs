using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claudiaGehtAb : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickOnSchwert.schwertGeklickt)
        {
            anim.SetTrigger("click_on_schwert");

            FMODUnity.RuntimeManager.PlayOneShot("event:/Sprache/Hey!Ist das");

            clickOnSchwert.schwertGeklickt = false;
        }
        if (grossesSchwert.schwertErhalten)
        {
            anim.SetTrigger("schwert_erhalten");
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sword");
            Destroy(gameObject, 2f);
        }
    }

    void PlayFootStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Footsteps");
    }

}
