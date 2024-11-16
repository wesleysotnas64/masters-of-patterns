using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForm1 : MonoBehaviour
{
    public GameObject fireball;
    public GameObject currentFirball;
    public Monster monster;
    public float instantFireballTime;
    public bool instantiated;


    void Start()
    {
        monster = GetComponent<Monster>();
        instantiated = false;
    }

    void Update()
    {
        if(monster.currentBlowTime >= instantFireballTime)
        {
            if(!instantiated)
            {
                instantiated = true;
                StartCoroutine(CastFireball());
            }
        }
    }

    IEnumerator CastFireball()
    {
        if(monster.healthPoints > 0)
        {
            currentFirball = Instantiate(fireball);
            yield return new WaitForSeconds(2.0f/12.0f);
            GetComponent<AudioSource>().Play();
            currentFirball.GetComponent<Fireball>().active = true;
            yield return new WaitForSeconds(10.0f/12.0f);
            instantiated = false;

        }
    }
}
