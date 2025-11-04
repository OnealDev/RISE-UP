using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float bounceHeight = 0.3f;
    public float bounceDuration = 0.4f;
    public int bounceCount = 2;

    public void StartBounce()
    {
        StartCoroutine(BounceHandler());
    }

    private IEnumerator BounceHandler()
    {
        Vector3 startPosition = transform.position;
        float localHeight = bounceHeight;
        float localDuration = bounceDuration;

        for(int i = 0; i < bounceCount; i++)
        {
            yield return Bounce(startPosition, localHeight, localDuration / 2);
            localHeight *= 0.5f;
            localDuration *= 0.8f;
        }

        transform.position = startPosition;
    }

    private IEnumerator Bounce(Vector3 start, float height, float duration)
    {
        Vector3 peak = start + Vector3.up * height;
        float elasped = 0f;

        //Move upwards
        while(elasped < duration)
        {
            transform.position = Vector3.Lerp(start, peak, elasped / duration);
            elasped += Time.deltaTime;
            yield return null;
        }

        elasped = 0f;

        //Move Downwards
        while (elasped < duration)
        {
            transform.position = Vector3.Lerp(peak, start, elasped / duration);
            elasped += Time.deltaTime;
            yield return null;
        }
    }
}
