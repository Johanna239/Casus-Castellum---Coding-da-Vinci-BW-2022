using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveBetweenPoints : MonoBehaviour

{

    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;
    [SerializeField] private float animationTime;
    [SerializeField] private UnityEvent onEndReachedEvent;
    private float position;

    public void LerpBetweenPoints()
    {
        startPoint = transform.position;
        StartCoroutine(LerpToPosition(startPoint, endPoint, animationTime));
    }

    public IEnumerator LerpToPosition(Vector3 start, Vector3 end, float animationLength)
    {
        float elapsedTime = 0;
        while (elapsedTime < animationLength)
        {
            transform.position = Vector3.Lerp(start, end, (elapsedTime / animationLength));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
        if (onEndReachedEvent != null)
        {
            onEndReachedEvent.Invoke();
        }
    }
}
