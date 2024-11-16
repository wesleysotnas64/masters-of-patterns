using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using Unity.VisualScripting;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public GameObject bossForm1;
    public GameObject bossForm2;
    public QuizCanvasController quizCanvasController;


    public AudioSource audioSource;
    public AudioClip audioClipBossF1Intro;
    public AudioClip audioClipBossF1Gameplay;
    public AudioClip audioClipBossF2Intro;
    public AudioClip audioClipBossF2Gameplay;
    public AudioClip audioclipBossF2Presentation;
    public AudioClip audioClipBossRoarDeathForm1;
    public AudioClip audioClipBossRoarDeathForm2;
    public AudioClip audioClipConcludeBattle;
    public AudioClip audioClipThunder;

    public bool bossForm1Dead;
    public bool bossForm2Dead;

    void Start()
    {
        bossForm1Dead = false;
        bossForm2Dead = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        quizCanvasController.monster = bossForm1.GetComponent<Monster>();
        StartCoroutine(BossPresentationForm1());
    }

    void Update()
    {
        if(bossForm1.GetComponent<Monster>().healthPoints <= 0 && bossForm1Dead == false)
        {
            bossForm1Dead = true;
            StartCoroutine(VerifyF1Dead());
        }
        if(bossForm2.GetComponent<Monster>().healthPoints <= 0 && bossForm2Dead == false)
        {
            bossForm2Dead = true;
            StartCoroutine(VerifyF2Dead());
        }
    }

    //Ato 1
    IEnumerator BossPresentationForm1()
    {
        //Instanciar o Boss forma 1
        bossForm1.SetActive(true);

        //Tocar a música da intro forma 1
        audioSource.clip = audioClipBossF1Intro;
        audioSource.Play();
        StartCoroutine(BossF1Surgindo());
        
        yield return new WaitForSeconds(10.344f);

        quizCanvasController.ready = true;
        quizCanvasController.NextQuestion();

        bossForm1.GetComponent<Monster>().ready = true;

        audioSource.clip = audioClipBossF1Gameplay;
        audioSource.loop = true;
        audioSource.Play();
    }

    IEnumerator BossF1Surgindo()
    {
        float elapsed = 0.0f;
        float time = 5.0f;
        while(elapsed < time)
        {
            bossForm1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,elapsed/time);
            GameObject.Find("Tail").GetComponent<SpriteRenderer>().color = new Color(1,1,1,elapsed/time);
            if (elapsed < time) elapsed += Time.deltaTime;
            else elapsed = time;

            yield return null;

        }
    }

    IEnumerator BossF1Morrendo()
    {
        float elapsed = 5.0f;
        float time = 5.0f;
        while(elapsed > 0)
        {
            GameObject.Find("MainCamera").GetComponent<CameraShake>().Shake(1);
            bossForm1.GetComponent<SpriteRenderer>().color = new Color(1,1,1,elapsed/time);
            GameObject.Find("Tail").GetComponent<SpriteRenderer>().color = new Color(1,1,1,elapsed/time);
            elapsed -= Time.deltaTime;
            if(elapsed < 0) elapsed = 0;
            yield return null;
        }
    }

    IEnumerator BossF2Morrendo()
    {
        float elapsed = 5.0f;
        while(elapsed > 0)
        {
            GameObject.Find("MainCamera").GetComponent<CameraShake>().Shake(1);
            elapsed -= Time.deltaTime;
            if(elapsed < 0) elapsed = 0;
            yield return null;
        }
    }

    IEnumerator BossPresentationForm2()
    {

        audioSource.clip = audioclipBossF2Presentation;
        audioSource.loop = false;
        audioSource.Play();

        yield return new WaitForSeconds(5);

        bossForm2.SetActive(true);

        audioSource.clip = audioClipBossF2Intro;
        audioSource.loop = false;
        audioSource.Play();

        quizCanvasController.monster = bossForm2.GetComponent<Monster>();

        StartCoroutine(bossForm2.GetComponent<BossForm2>().Presentation());
        yield return new WaitForSeconds(8.986f);

        quizCanvasController.ready = true;
        quizCanvasController.NextQuestion();

        bossForm2.GetComponent<Monster>().ready = true;

        audioSource.clip = audioClipBossF2Gameplay;
        audioSource.loop = true;
        audioSource.Play();

    }    

    IEnumerator VerifyF1Dead()
    {
        quizCanvasController.DisableOptionButtons();

        audioSource.Stop();

        //Toca audio do monstro morrendo

        audioSource.clip = audioClipBossRoarDeathForm1;
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.Play();
        // bossForm1.GetComponent<Monster>().ready = false;
        StartCoroutine(BossF1Morrendo());
        yield return new WaitForSeconds(5);

        audioSource.clip = audioClipThunder;
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.Play();

        yield return new WaitForSeconds(5);
        audioSource.volume = 1.0f;
        bossForm1.SetActive(false);
        StartCoroutine(BossPresentationForm2());
    }

    IEnumerator VerifyF2Dead()
    {
        quizCanvasController.DisableOptionButtons();

        audioSource.Stop();

        audioSource.clip = audioClipBossRoarDeathForm2;
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.Play();
        StartCoroutine(BossF2Morrendo());
        yield return new WaitForSeconds(10);
        audioSource.volume = 1.0f;
        bossForm2.SetActive(false);

        audioSource.clip = audioClipConcludeBattle;
        audioSource.Play();
        // StartCoroutine(BossPresentationForm2());
    }

}
