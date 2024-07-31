using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Atributos")]
    
    public int healthPoints;
    public int maxHealthPoints;
    public float timeAttack;
    public float currentTimeAttack;
    public int damage;

    //Animator
    public bool attacking;
    public bool dying;
    public bool striking;
    public bool opportunity;
    

    [Header("Estados")]
    public bool ready;

    [Header("Componentes")]
    public Player player;
    public Animator anim;

    

    [Header("Controle de Ataque")]
    public bool chargingAttack;
    public bool preparing;
    public bool hitting;
    public bool recovering;
    public bool hit;
    public int blow; //[0] basic attack | [1] opportunity
    public List<Blow> blows;
    public float currentBlowTime;

    //Striked Controller
    public float strikingTime;
    public SpriteRenderer spriteRenderer;

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
        currentBlowTime = 0.0f;

        //Striked Controller
        striking = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {

        //Testando ataques
        if(Input.GetKeyDown("1")) Attack();
        if(Input.GetKeyDown("2")) AttackOpportunity();
        

        if(ready)
        {
            ChargeAttack();
            BlowControll();
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
            Death();
        }

        currentTimeAttack = 0.0f;

        //Coroutine para animar dano;
        StartCoroutine(StrikeCoroutine());
    }

    private void Death()
    {
        dying = true;
        attacking = false;
        striking = false;
        chargingAttack = false;
    }

    IEnumerator StrikeCoroutine()
    {
        float elapsed = strikingTime;

        striking = true;
        while(elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            elapsed = elapsed < 0 ? 0 : elapsed;

            //Anima a cor do player de vermelho para branco
            spriteRenderer.color = Color.HSVToRGB(0, elapsed/strikingTime, 1);

            yield return null;
        }
        striking = false;
    }

    private void HitPlayer()
    {
        if(hit)
        {
            if(opportunity)
            {
                blow = 1;
                player.Strike(damage/2);
            }
            else
            {
                blow = 0;
                player.Strike(damage);
            }

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
                Attack();
            }
        }
    }

    //Ataque básico
    public void Attack()
    {
        if(!attacking && !opportunity)
        {
            chargingAttack = false;
            attacking = true;
            currentBlowTime = 0.0f;
        }
    }

    //Ataque de oportunidade
    public void AttackOpportunity()
    {
        if(!attacking && !opportunity)
        {
            chargingAttack = false;
            opportunity = true;
            currentBlowTime = 0.0f;
        }
    }

    private void BlowControll()
    {
        if(attacking || opportunity)
        {
            blow = opportunity ? 1 : 0;
            currentBlowTime += Time.deltaTime;

            if(currentBlowTime <= blows[blow].initTime)
            {
                preparing = true;
                hitting = false;
                recovering = false;
            }
            if(currentBlowTime > blows[blow].initTime && currentBlowTime <= blows[blow].activeTime && hitting == false)
            {
                preparing = false;
                hitting = true;
                recovering = false;

                hit = true;
            }
            else if(currentBlowTime > blows[blow].activeTime && currentBlowTime < blows[blow].recoverTime)
            {
                preparing = false;
                hitting = false;
                recovering = true;
            }
            else if(currentBlowTime >= blows[blow].recoverTime)
            {
                preparing = false;
                hitting = false;
                recovering = false;
                
                currentBlowTime = 0;

                currentTimeAttack = opportunity ? currentTimeAttack/2 : 0.0f;

                attacking = false;
                chargingAttack = true;
                opportunity = false;
            }
        }
    }

    private void AnimControll()
    {
        anim.SetBool("Attacking", attacking);
        anim.SetBool("Opportunity", opportunity);
        anim.SetBool("Dying", dying);
        anim.SetBool("Striking", striking);
    }
}

