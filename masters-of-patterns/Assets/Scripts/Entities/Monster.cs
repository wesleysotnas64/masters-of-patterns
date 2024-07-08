using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool ready;
    public int healthPoints;
    public int maxHealthPoints;
    public float timeAttack;
    public float currentTimeAttack;
    public int damage;
    public Player player;

    //Animator
    public Animator anim;
    public bool attacking;
    public bool dying;

    //Attacking Controller
    public bool chargingAttack;
    public bool preparing;
    public bool hitting;
    public bool recovering;
    public bool hit;
    public bool isAttackOpportunity;
    public List<float> hitTime; //[0] initAttack | [1] activeAttack | [2] recoverAttack
    public float currentHitTime;

    void Start()
    {
        ready = false;
        currentTimeAttack = 0;
        player = GameObject.Find("Player").GetComponent<Player>();

        //Animator
        anim = GetComponent<Animator>();
        attacking = false;
        dying = false;
        
        //Attack Controller
        chargingAttack = true;
        preparing = false;
        hitting = false;
        recovering = false;
        hit = false;
        isAttackOpportunity = false;
        currentHitTime = 0.0f;

    }

    void Update()
    {
        if(ready)
        {
            ChargeAttack();
            HitControll();
            AnimControll();
            HitPlayer();
        }
    }

    public void Strike(int _damage)
    {
        healthPoints -= _damage;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
        }

        //Coroutine para animar dano;
    }

    private void HitPlayer()
    {
        if(hit)
        {
            if(isAttackOpportunity)
            {
                player.Strike(damage/2);
                hit = false;
            }
            else
            {
                player.Strike(damage);
                hit = false;
            }
        }
    }

    private void ChargeAttack()
    {
        if(chargingAttack)
        {
            currentTimeAttack += Time.deltaTime;
            if(currentTimeAttack > timeAttack)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        chargingAttack = false;
        attacking = true;
        currentHitTime = 0.0f;
    }

    //Ataque de oportunidade
    public void AttackOpportunity()
    {
        chargingAttack = false;
        attacking = true;
        isAttackOpportunity = true;
    }

    private void HitControll()
    {
        if(attacking)
        {
            currentHitTime += Time.deltaTime;
            if(currentHitTime <= hitTime[0])
            {
                preparing = true;
                hitting = false;
                recovering = false;
            }
            if(currentHitTime > hitTime[0] && currentHitTime <= hitTime[1] && hitting == false)
            {
                preparing = false;
                hitting = true;
                recovering = false;

                hit = true;
            }
            else if(currentHitTime > hitTime[1] && currentHitTime < hitTime[2])
            {
                preparing = false;
                hitting = false;
                recovering = true;
            }
            else if(currentHitTime >= hitTime[2])
            {
                preparing = false;
                hitting = false;
                recovering = false;
                
                currentHitTime = 0;

                currentTimeAttack = isAttackOpportunity ? currentTimeAttack/2 : 0.0f;

                attacking = false;
                chargingAttack = true;
                isAttackOpportunity = false;
            }
        }
    }

    private void AnimControll()
    {
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Dying", dying);
    }
}

