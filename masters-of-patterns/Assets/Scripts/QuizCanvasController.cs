using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.Utilities;

public class QuizCanvasController : MonoBehaviour
{
    [Header("Canvas Components")]
    public TMP_Text textQuestion;
    public Image imgHealthPlayer;
    public Image imgHealthPlayerEffect;
    public Image imgHealthEnemy;
    public Image imgHealthEnemyEffect;
    public Image imgAttackEnemy;
    public List<GameObject> listOptionButton;
    public List<GameObject> listToolsButton;

    public Button btnNext;

    [Header("Canvas Attributes")]
    public Question currentQuestion;
    public bool skipping;
    public float delay;
    public QuestionHandler questionHandler;

    [Header("Player")]
    public Player player;
    public TMP_Text textPlayerHealth;

    [Header("Monster (Enemy)")]
    public Monster monster;

    void Start()
    {
        //inicializar Canvas
        InitCanvas();

        //Selecionar questão para o buffer
        // currentQuestion = questionHandler.GetRandomQuestioin();
        
    }

    void Update()
    {
        UpdateCanvas();
    }

    private void InitCanvas()
    {
        textQuestion.text = "";
        textPlayerHealth.text = player.healthPoints.ToString()+"/"+player.maxHealthPoints.ToString();

        foreach(GameObject btn in listOptionButton)
        {
            btn.GetComponent<Button>().interactable = false;
        }
        foreach(GameObject btn in listToolsButton)
        {
            btn.GetComponent<Button>().interactable = false;
        }

        btnNext.interactable = false;

        imgHealthPlayer.rectTransform.localScale = new Vector3((float)player.healthPoints/player.maxHealthPoints, 1, 1);
        imgHealthEnemy.rectTransform.localScale = new Vector3((float)monster.healthPoints/monster.maxHealthPoints, 1, 1);
        imgAttackEnemy.rectTransform.localScale = new Vector3(monster.currentTimeAttack/monster.timeAttack, 1, 1);
    }

    private void UpdateCanvas()
    {
        textPlayerHealth.text = player.healthPoints.ToString()+"/"+player.maxHealthPoints.ToString();

        imgHealthPlayer.rectTransform.localScale = new Vector3((float)player.healthPoints/player.maxHealthPoints, 1, 1);
        imgHealthEnemy.rectTransform.localScale = new Vector3((float)monster.healthPoints/monster.maxHealthPoints, 1, 1);
        imgAttackEnemy.rectTransform.localScale = new Vector3(monster.currentTimeAttack/monster.timeAttack, 1, 1);

        //LifeEffect
        imgHealthPlayerEffect.rectTransform.localScale = Vector3.Lerp(
            imgHealthPlayerEffect.rectTransform.localScale,
            imgHealthPlayer.rectTransform.localScale,
            Time.deltaTime * 5
        );
        imgHealthEnemyEffect.rectTransform.localScale = Vector3.Lerp(
            imgHealthEnemyEffect.rectTransform.localScale,
            imgHealthEnemy.rectTransform.localScale,
            Time.deltaTime * 5
        );
    }
}
