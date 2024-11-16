using System.Collections;
using UnityEngine;

using Assets.Scripts.Utilities;

public class Player : MonoBehaviour
{
    public int healthPoints;
    public int maxHealthPoints;
    public int damage;

    public CameraShake cameraShake;
    public AudioSource audioSource;

    void Start()
    {
        cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Strike(int _damage)
    {
        cameraShake.Shake(_damage);
        healthPoints -= _damage;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
            GameObject.Find("ModalGameOver").GetComponent<ModalGameOver>().SetLoser();
        }

        //Play sound blow
        audioSource.Play();
    }
}
