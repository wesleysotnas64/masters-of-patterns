using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizCanvasController : MonoBehaviour
{
    public TMP_Text textQuestion;
    public Image imgHealthPlayer;
    public Image imgHealthPlayerEffect;
    public List<GameObject> listOptionButton;
    public List<TMP_Text> listTextOption;
    public Button btnNext;

    public Question currentQuestion;
    public bool skipping;
    public float delay;
    public List<int> idsAlreadyDrawn;
    public int qtdQuestions;

    //Player
    public float playerHealth;
    public float playerHealthCurrent;
    public float playerDamage;

    //Monster
    public float monsterHealth;
    public float monsterHealthCurrent;
    public float monsterDamage;

    void Start()
    {
        //Limpar todos os campos
        //Selecionar inimigo
        //Selecionar questões para o buffer
        
    }

    void Update()
    {
        
    }
}
