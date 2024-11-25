using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimIntroMove : MonoBehaviour
{
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float timeAnimation;
    
    void Start()
    {
        StartCoroutine(AnimCoroutine());
    }


    IEnumerator AnimCoroutine()
    {
        float elapsed = 0.0f;

        while(elapsed < timeAnimation)
        {
            yield return null;
            elapsed += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, elapsed/timeAnimation);
        }

    }
}
