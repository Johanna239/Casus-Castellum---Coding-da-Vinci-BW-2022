using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private float animationSpeed = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator> ();
    }

    public void setAnimationSpeed(float speed) {
        this.animationSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        anim.speed = animationSpeed;
    }
}
