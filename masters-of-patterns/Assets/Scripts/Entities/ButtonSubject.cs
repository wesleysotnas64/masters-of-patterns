using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSubject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Coroutine growCoroutine;
    public float maxScale;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    IEnumerator Grow(float targetScale)
    {
        float duration = 0.1f;
        float elapsed = 0.0f;
        Vector3 initialScale = rectTransform.localScale;
        Vector3 finalScale = new Vector3(targetScale, targetScale, targetScale);

        while(elapsed < duration)
        {
            rectTransform.localScale = Vector3.Lerp(
                initialScale, finalScale, elapsed/duration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = finalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(growCoroutine != null)
        {
            StopCoroutine(growCoroutine);
        }
        StartCoroutine(Grow(maxScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(growCoroutine != null)
        {
            StopCoroutine(growCoroutine);
        }
        StartCoroutine(Grow(1.0f));
    }
}
