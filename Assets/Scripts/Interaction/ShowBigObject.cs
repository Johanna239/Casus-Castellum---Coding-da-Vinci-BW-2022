using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowBigObject : MonoBehaviour
{
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;
    [SerializeField] private float animationTime;
    [SerializeField] private UnityEvent onEndReachedEvent;
    private BoxCollider2D boxCol2D;
    private Renderer spriteRenderer;
    private float position;
    private bool big = false;


    void Start()
    {
        if (TryGetComponent(out BoxCollider2D col))
        {
            this.boxCol2D = col;
        }
        if (TryGetComponent(out Renderer rend)) {
            this.spriteRenderer = rend;
        }
    }

    void OnMouseUp() {
        if(big == false) {
            StartCoroutine(LerpToScale(startPoint, endPoint, animationTime));
            if(spriteRenderer) { spriteRenderer.sortingOrder = 50; }
        } else {
            StartCoroutine(LerpToScale(endPoint, startPoint, animationTime));
            if(spriteRenderer) { spriteRenderer.sortingOrder = 10; }
        }
    }

    public IEnumerator LerpToScale(Vector3 start, Vector3 end, float animationLength)
    {
        if (boxCol2D)
        {
            boxCol2D.enabled = !boxCol2D.enabled;
        }

        float elapsedTime = 0;
        while (elapsedTime < animationLength)
        {
            transform.localScale = Vector3.Lerp(start, end, (elapsedTime / animationLength));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = end;
        big = !big;

        if (onEndReachedEvent != null)
        {
            onEndReachedEvent.Invoke();
        }

        if (boxCol2D)
        {
            boxCol2D.enabled = !boxCol2D.enabled;
        }
    }
}
