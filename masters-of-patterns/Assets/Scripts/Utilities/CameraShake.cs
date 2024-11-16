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

    public void Shake(int _magnitude)
    {
        SelectMagnitude(_magnitude);
        if(shaking == false)
        {
            shaking = true;
            elapsed = 0.0f;
        }
    }

    private void SelectMagnitude(int _magnitude)
    {
        switch(_magnitude)
        {
            case 1:
                magnitude = 0.05f;
                break;
            
            case 2:
                magnitude = 0.1f;
                break;

            case 3:
                magnitude = 0.2f;
                break;

            case 4:
                magnitude = 0.3f;
                break;
            
            default:
                magnitude = 0.4f;
                break;
        }
    }
}
}

