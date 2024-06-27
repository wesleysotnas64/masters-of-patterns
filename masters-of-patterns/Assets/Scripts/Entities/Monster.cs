using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int healthPoints;
    public int maxHealthPoints;
    public float timeAttack;
    public float currentTimeAttack;
    public int damage;
    public Player player;

    void Start()
    {
        currentTimeAttack = 0;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        ChargeAttack();
    }

    private void ChargeAttack()
    {
        currentTimeAttack += Time.deltaTime;
        if(currentTimeAttack > timeAttack)
        {
            currentTimeAttack = 0;
            Debug.Log("Golpeia player!");
            player.Strike(damage);
            // GameObject.Find("MainCamera").GetComponent<CameraShake>().Shake();
        }
    }
}
