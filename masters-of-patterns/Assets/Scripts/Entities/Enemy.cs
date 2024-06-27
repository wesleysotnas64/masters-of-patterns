using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int healthPoints;
    public int maxHealthPoints;
    public float attackInterval;
    public float maxAttackInterval;
    public int damage;
    public Player player;


    void Start()
    {
        attackInterval = 0.0f;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        ChargeAttack();
    }

    public void Strike(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
        }
    }

    private void ChargeAttack()
    {
        attackInterval += Time.deltaTime;
        if(attackInterval > maxAttackInterval){
            attackInterval = 0.0f;
            Debug.Log(attackInterval);
            Debug.Log(maxAttackInterval);
            // player.Strike(damage);
        }
    }
}

