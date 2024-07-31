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
    public Image imgHealthMonster;
    public Image imgHealthMonsterEffect;
    public Image imgAttackMonster;
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

    [Header("Monste")]
    public Monster monster;
    public bool firstCall;

    void Start()
    {
        //inicializar Canvas
        InitCanvas();

        //Selecionar questão para o buffer
        firstCall = true;
        currentQuestion = questionHandler.GetRandomQuestioin();
        StartCoroutine(TypewriteText());

    }

    void Update()
    {
        UpdateCanvas();
    }

    public void NextQuestion()
    {
        InitCanvas();
        currentQuestion = questionHandler.GetRandomQuestioin();
        StartCoroutine(TypewriteText());
    }

    public void SelectQuestion(int index)
    {
        if(currentQuestion.isCorrect[index])
        {
            listOptionButton[index].GetComponent<Image>().color = Color.HSVToRGB(125.0f/360.0f, 0.5f, 0.75f); //Cor do botão

            //Causa dano no monstro
            monster.Strike(player.damage);
        }
        else
        {
            listOptionButton[index].GetComponent<Image>().color = Color.HSVToRGB(0.0f/360.0f, 0.5f, 0.75f); //Cor do botão
            for(int i = 0; i < 4; i++)
            {
                if(currentQuestion.isCorrect[i])
                {
                    listOptionButton[i].GetComponent<Image>().color = Color.HSVToRGB(125.0f/360.0f, 0.5f, 0.75f);
                }
            }

            //Causa dano no player
            monster.AttackOpportunity();
        }

        DisableOptionButtons();
        btnNext.interactable = true;
    }

    private void InitCanvas()
    {
        textQuestion.text = "";
        textPlayerHealth.text = player.healthPoints.ToString()+"/"+player.maxHealthPoints.ToString();

        DisableOptionButtons();
        btnNext.interactable = false;

        imgHealthPlayer.rectTransform.localScale = new Vector3((float)player.healthPoints/player.maxHealthPoints, 1, 1);
        imgHealthMonster.rectTransform.localScale = new Vector3((float)monster.healthPoints/monster.maxHealthPoints, 1, 1);
        imgAttackMonster.rectTransform.localScale = new Vector3(monster.currentTimeAttack/monster.timeAttack, 1, 1);

        foreach(GameObject btn in listOptionButton)
        {
            btn.GetComponent<Image>().color = Color.white;
        }
    }

    private void UpdateCanvas()
    {
        textPlayerHealth.text = player.healthPoints.ToString()+"/"+player.maxHealthPoints.ToString();

        //Atualiza coloração da barra de vida
        float healthCondition = (float) player.healthPoints/player.maxHealthPoints;
        float hValue = 125.0f * healthCondition;
        imgHealthPlayer.color = Color.HSVToRGB(hValue/360.0f, 0.5f, 0.75f);

        healthCondition = (float) monster.healthPoints/monster.maxHealthPoints;
        hValue = 125.0f * healthCondition;
        imgHealthMonster.color = Color.HSVToRGB(hValue/360.0f, 0.5f, 0.75f);

        imgHealthPlayer.rectTransform.localScale = new Vector3((float)player.healthPoints/player.maxHealthPoints, 1, 1);
        imgHealthMonster.rectTransform.localScale = new Vector3((float)monster.healthPoints/monster.maxHealthPoints, 1, 1);
        imgAttackMonster.rectTransform.localScale = new Vector3(monster.currentTimeAttack/monster.timeAttack, 1, 1);

        //LifeEffect
        imgHealthPlayerEffect.rectTransform.localScale = Vector3.Lerp(
            imgHealthPlayerEffect.rectTransform.localScale,
            imgHealthPlayer.rectTransform.localScale,
            Time.deltaTime * 5
        );
        imgHealthMonsterEffect.rectTransform.localScale = Vector3.Lerp(
            imgHealthMonsterEffect.rectTransform.localScale,
            imgHealthMonster.rectTransform.localScale,
            Time.deltaTime * 5
        );
    }

    IEnumerator TypewriteText()
    {

        // skipping = false;

        int iteration = 0;
        foreach (string text in currentQuestion.text){
            
            for(int i = 0; i < text.Length; i++)
            {
                textQuestion.text += text[i];
                yield return null;
                // yield return new WaitForSeconds(skipping ? 0 : delay);
            }

            if (iteration != currentQuestion.text.Count - 1){
                textQuestion.text += "\n\n";
            }

            iteration++;
        }

        textQuestion.text += "\n\n";
        List<string> auxOptions = new List<string>
        {
            "A - " + currentQuestion.option[0],
            "B - " + currentQuestion.option[1],
            "C - " + currentQuestion.option[2],
            "D - " + currentQuestion.option[3]
        };

        iteration = 0;
        foreach (string text in auxOptions){
            for(int i = 0; i < text.Length; i++)
            {
                textQuestion.text += text[i];
                yield return null;
                // yield return new WaitForSeconds(skipping ? 0 : delay);
            }

            if (iteration != currentQuestion.option.Count - 1){
                textQuestion.text += "\n\n";
            }

            iteration++;
        }

        EnableOptionButtons();

        if(firstCall)
        {
            monster.ready = true;;
            firstCall = false;
        }
    }

    private void EnableToolsButtons()
    {
        foreach(GameObject btn in listToolsButton)
        {
            btn.GetComponent<Button>().interactable = true;
        }
    }

    private void DisableToolsButtons()
    {
        foreach(GameObject btn in listToolsButton)
        {
            btn.GetComponent<Button>().interactable = false;
        }
    }

    private void EnableOptionButtons()
    {
        foreach(GameObject btn in listOptionButton)
        {
            btn.GetComponent<Button>().interactable = true;
        }
    }

    private void DisableOptionButtons()
    {
        foreach(GameObject btn in listOptionButton)
        {
            btn.GetComponent<Button>().interactable = false;
        }
    }
}
