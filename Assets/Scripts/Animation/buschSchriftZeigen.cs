using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buschSchriftZeigen : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    bool triggerValue;
    bool animationRunning;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        animationRunning = false;
    }

    void Update()
    {
        triggerValue = anim.GetBool("busch_schrift");
        if(animationRunning)
        {
            // anim.ResetTrigger("busch_schrift");
            animationRunning = false;
        }
    }

    void OnMouseUp()
    {
        if (!animationRunning)
        {
            anim.SetTrigger("busch_schrift");
            FMODUnity.RuntimeManager.PlayOneShot("event:/BUSCH");
            animationRunning = true;
        }
        
    }
}
