using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utilities
{

public class CameraShake : MonoBehaviour
{
    [Header("Shake")]
    public float duration;
    public float elapsed;
    public float magnitude;
    public bool shaking;
    private Vector3 originalPosition;

    void Start()
    {
        shaking = false;
        elapsed = 0.0f;
        originalPosition = transform.localPosition;
        StartCoroutine(ShakeCoroutine());
    }

    void Update()
    {
        if(Input.GetKeyDown("space")) Shake();
    }

    IEnumerator ShakeCoroutine()
    {
        while(true)
        {
            if(shaking)
            {   
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPosition.z);

                elapsed += Time.deltaTime;
                
                if(elapsed >= duration)
                {
                    shaking = false;
                    transform.localPosition = originalPosition;
                }

                yield return null;
            }
            else
            {
                yield return null;
            }
        }
        
    }

    public void Shake()
    {
        if(shaking == false)
        {
            shaking = true;
            elapsed = 0.0f;
        }
    }
}
}

