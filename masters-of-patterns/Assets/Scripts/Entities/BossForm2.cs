using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BossForm2 : MonoBehaviour
{
    public GameObject fireball;
    public List<GameObject> fireballsAttack;
    public Animator headAnimator;
    public Animator rightArmAnimator;
    public Monster monster;

    public Vector2 idlePosition;
    public Vector2 attackPrimaryPosition;
    public Vector2 initialPresentationPosition;

    public float speedTranslatePosition;

    public Transform mounthPosition;
    public Transform handUpPosition;
    public Transform handDownPosition;

    void Start()
    {
        transform.position = idlePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("o")) StartCoroutine(AttackOpportunity());
        if(Input.GetKeyDown("p")) StartCoroutine(AttackPrimary());
        if(Input.GetKeyDown("l")) StartCoroutine(Hitted());

        if(GetComponent<Monster>().healthPoints <= 0)
        {
            headAnimator.SetBool("Dead", true);
            transform.Translate(0, -1.2f*Time.deltaTime, 0);
        }
    }

    public IEnumerator Hitted()
    {
        headAnimator.SetBool("Hitting", true);
        headAnimator.SetBool("Idle", false);

        yield return new WaitForSeconds(0.5f);

        headAnimator.SetBool("Hitting", false);
        headAnimator.SetBool("Idle", true);

        if(GetComponent<Monster>().healthPoints <= 0) headAnimator.SetBool("Dead", true);
    }

    IEnumerator TranslateToAttackPosition()
    {
        float count = 0.0f;

        while(count < 1)
        {
            transform.position = Vector2.Lerp(idlePosition, attackPrimaryPosition, count);
            count += Time.deltaTime*speedTranslatePosition;
            if(count >= 1) count = 1;
            yield return null; 
        }
    }

    IEnumerator TranslateToIdlePosition()
    {
        float count = 0.0f;

        while(count < 1)
        {
            transform.position = Vector2.Lerp(attackPrimaryPosition, idlePosition, count);
            count += Time.deltaTime*speedTranslatePosition;
            if(count >= 1) count = 1;
            yield return null; 
        }
    }


    //Ataques
    public IEnumerator AttackPrimary()
    {
        StartCoroutine(TranslateToAttackPosition());
        yield return new WaitForSeconds(0.75f);
        headAnimator.SetBool("Attacking", true);
        headAnimator.SetBool("Idle", false);
        headAnimator.SetBool("Hitting", false);

        //Instanciar bolas de fogo
        InstantiateFireballs();

        yield return new WaitForSeconds(0.5f);

        //Ativar bolas de fogo
        StartCoroutine(ActiveFireballs());
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.75f);

        headAnimator.SetBool("Attacking", false);
        headAnimator.SetBool("Idle", true);
        headAnimator.SetBool("Hitting", false);
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(TranslateToIdlePosition());

        fireballsAttack.Clear();
    }

    private void InstantiateFireballs()
    {
        for(int i = 0; i < 12; i++)
        {
            fireballsAttack.Add(Instantiate(fireball));
            fireballsAttack[i].GetComponent<Transform>().position = mounthPosition.position;

            if(i == 0 || i == 4 || i == 8) fireballsAttack.Last().GetComponent<Fireball>().finalPoint = new Vector2(-3, -2);
            if(i == 1 || i == 5 || i == 9) fireballsAttack.Last().GetComponent<Fireball>().finalPoint = new Vector2(-1, -2);
            if(i == 2 || i == 6 || i == 10) fireballsAttack.Last().GetComponent<Fireball>().finalPoint = new Vector2(1, -2);
            if(i == 3 || i == 7 || i == 11) fireballsAttack.Last().GetComponent<Fireball>().finalPoint = new Vector2(3, -2);

            fireballsAttack[i].GetComponent<Fireball>().CalcDirection();
            fireballsAttack[i].GetComponent<Fireball>().speed = 20;
        }
    }

    IEnumerator ActiveFireballs()
    {
        fireballsAttack[0].GetComponent<Fireball>().active = true;
        fireballsAttack[1].GetComponent<Fireball>().active = true;
        fireballsAttack[2].GetComponent<Fireball>().active = true;
        fireballsAttack[3].GetComponent<Fireball>().active = true;

        yield return new WaitForSeconds(0.2f);

        fireballsAttack[4].GetComponent<Fireball>().active = true;
        fireballsAttack[5].GetComponent<Fireball>().active = true;
        fireballsAttack[6].GetComponent<Fireball>().active = true;
        fireballsAttack[7].GetComponent<Fireball>().active = true;

        yield return new WaitForSeconds(0.2f);

        fireballsAttack[8].GetComponent<Fireball>().active = true;
        fireballsAttack[9].GetComponent<Fireball>().active = true;
        fireballsAttack[10].GetComponent<Fireball>().active = true;
        fireballsAttack[11].GetComponent<Fireball>().active = true;

    }

    public IEnumerator AttackOpportunity()
    {
        Preparing();
        fireballsAttack.Add(Instantiate(fireball));
        fireballsAttack[0].GetComponent<Transform>().position = handUpPosition.position;
        yield return new WaitForSeconds(0.4f);

        Attacking();
        fireballsAttack[0].GetComponent<Transform>().position = handDownPosition.position;
        yield return new WaitForSeconds(0.2f);
        fireballsAttack[0].GetComponent<Fireball>().finalPoint = new Vector2(0, -2);
        fireballsAttack[0].GetComponent<Fireball>().CalcDirection();
        fireballsAttack[0].GetComponent<Fireball>().active = true;
        yield return new WaitForSeconds(0.4f);

        Idle();
        yield return new WaitForSeconds(0.5f);
        fireballsAttack.Clear();
    }


    //Controlando estado da animação
    public void Preparing()
    {
        rightArmAnimator.SetBool("Preparing", true);
        rightArmAnimator.SetBool("Idle", false);
        rightArmAnimator.SetBool("Attacking", false);
    }
    public void Attacking()
    {
        rightArmAnimator.SetBool("Preparing", false);
        rightArmAnimator.SetBool("Idle", false);
        rightArmAnimator.SetBool("Attacking", true);
    }
    public void Idle()
    {
        rightArmAnimator.SetBool("Preparing", false);
        rightArmAnimator.SetBool("Idle", true);
        rightArmAnimator.SetBool("Attacking", false);
    }

    public IEnumerator Presentation()
    {
        float elapsed = 0.0f;
        float time = 5.0f;
        while(elapsed < time)
        {
            transform.position = Vector2.Lerp(
            initialPresentationPosition,
            idlePosition,
            elapsed/time
            );
            elapsed += Time.deltaTime;
            if(elapsed >= time ) elapsed = time;
            yield return null;
        }
    }

    public IEnumerator TranslateToDeath()
    {
        float elapsed = 5.0f;
        float time = 5.0f;

        while(elapsed > 0)
        {
            headAnimator.SetBool("Dead", true);
    
            transform.position = Vector2.Lerp(
            idlePosition,
            initialPresentationPosition,
            elapsed/time
            );
            elapsed -= Time.deltaTime;
            if(elapsed < 0 ) elapsed = 0;
            yield return null;
        }
    }

}
