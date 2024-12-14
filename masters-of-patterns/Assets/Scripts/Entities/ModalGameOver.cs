using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModalGameOver : MonoBehaviour
{
    public TMP_Text mainText;
    public AudioClip soundWin;
    public AudioClip soundLose;
    public AudioSource audioSource;

    public void SetWinner()
    {
        GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
        try{
            GameObject.Find("BossFigthController").GetComponent<AudioSource>().Stop();
        }
        catch{
        }
        mainText.text = "Vitória!";
        audioSource.clip = soundWin;
        RenderModal();
    }

    public void SetLoser()
    {
        GameObject.Find("MainCamera").GetComponent<AudioSource>().Stop();
        try{
            GameObject.Find("BossFigthController").GetComponent<AudioSource>().Stop();
        }
        catch{
        }
        mainText.text = "Derrota!";
        audioSource.clip = soundLose;
        RenderModal();
    }

    private void RenderModal()
    {
        audioSource.Play();
        transform.position = new Vector3(0, 0, 0);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
