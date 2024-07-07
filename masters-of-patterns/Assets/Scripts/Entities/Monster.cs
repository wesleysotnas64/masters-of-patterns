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
    public List<float> hitTime; //[0] initAttack | [1] activeAttack | [2] recoverAttack
    public float currentHitTime;

    void Start()
    {
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
        currentHitTime = 0.0f;

    }

    void Update()
    {
        ChargeAttack();
        HitControll();
        AnimControll();
        HitPlayer();
    }

    private void HitPlayer()
    {
        if(hit)
        {
            player.Strike(damage);
            hit = false;
        }
    }

    private void ChargeAttack()
    {
        if(chargingAttack)
        {
            currentTimeAttack += Time.deltaTime;
            if(currentTimeAttack > timeAttack)
            {
                chargingAttack = false;
                attacking = true;
                currentHitTime = 0.0f;
            }
        }
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
                currentTimeAttack = 0.0f;

                attacking = false;
                chargingAttack = true;
            }
        }
    }

    private void AnimControll()
    {
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Dying", dying);
    }
}

