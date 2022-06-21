using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{

    private Animator anim;
    public float animationSpeed = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = animationSpeed;
    }
}
