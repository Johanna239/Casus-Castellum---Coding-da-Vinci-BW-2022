using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipiBusch : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    bool triggerValue;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update() { 
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!triggerValue)
            {
                anim.SetTrigger("pipibuschParameter");
            }
        }
    }
}
