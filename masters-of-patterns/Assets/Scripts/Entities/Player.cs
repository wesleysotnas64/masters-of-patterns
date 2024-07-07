using System.Collections;
using UnityEngine;

using Assets.Scripts.Utilities;

public class Player : MonoBehaviour
{
    public int healthPoints;
    public int maxHealthPoints;
    public int damage;

    public CameraShake cameraShake;

    void Start()
    {
        cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
    }

    public void Strike(int _damage)
    {
        cameraShake.Shake();
        healthPoints -= _damage;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
        }

    }
}
