using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizClassRoomCanvas : MonoBehaviour
{
    public TMP_Text textQuestion;
    public Image imgMonster;
    public Image imgHealthMonster;
    public Image imgHealthMonsterEffect;
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
        btnNext.interactable = false;
        textQuestion.text = "";
        playerHealthCurrent = playerHealth;
        monsterHealthCurrent = monsterHealth;
        LoadQuestion();
    }

    void Update()
    {
        DecreseLifeEffect();
    }

    public void LoadQuestion()
    {
        int randomId;
        while(true)
        {
            if(idsAlreadyDrawn.Count >= 10) idsAlreadyDrawn.Clear();

            randomId = Random.Range(16, 26);

            if(!idsAlreadyDrawn.Contains(randomId))
            {
                idsAlreadyDrawn.Add(randomId);
                break;
            }
        }

        GameObject q = Resources.Load<GameObject>("QuizQuestions/QuizQuestion_"+randomId.ToString());
        currentQuestion = q.GetComponent<Question>();

        RenderQuestion();
    }

    private void RenderQuestion()
    {
        CleanTextInCanvas();
        StartCoroutine(TypewriteText());
    }

    IEnumerator TypewriteText()
    {
        skipping = false;

        int iteration = 0;
        foreach (string text in currentQuestion.text){
            
            for(int i = 0; i < text.Length; i++)
            {
                textQuestion.text += text[i];
                yield return new WaitForSeconds(skipping ? 0 : delay);
            }

            if (iteration != currentQuestion.text.Count - 1){
                textQuestion.text += "\n\n";
            }

            iteration++;
        }

        if(currentQuestion.option.Count == 4)
        {
            textQuestion.text += "\n\n";
            List<string> auxOptions = new List<string>();
            auxOptions.Add("A - "+currentQuestion.option[0]);
            auxOptions.Add("B - "+currentQuestion.option[1]);
            auxOptions.Add("C - "+currentQuestion.option[2]);
            auxOptions.Add("D - "+currentQuestion.option[3]);
            

            iteration = 0;
            foreach (string text in auxOptions){
                for(int i = 0; i < text.Length; i++)
                {
                    textQuestion.text += text[i];
                    yield return new WaitForSeconds(skipping ? 0 : delay);
                }

                if (iteration != currentQuestion.option.Count - 1){
                    textQuestion.text += "\n\n";
                }

                iteration++;
            }

        }

        skipping = true;

        RenderQuestionOptionsInCanvas();

    }

    private void RenderQuestionOptionsInCanvas()
    {
        for(int i = 0; i < 4; i++)
        {
            listOptionButton[i].SetActive(true);
        }
        foreach(TMP_Text text in listTextOption){
            text.text = "";
        }

        int qtdOptions = currentQuestion.isCorrect.Count;
        if (qtdOptions == 2)
        {
            listTextOption[0].text = "Verdadeiro";
            listTextOption[1].text = "Falso";

            listOptionButton[2].SetActive(false);
            listOptionButton[3].SetActive(false);
        }
        else
        {
            listTextOption[0].text = "A";
            listTextOption[1].text = "B";
            listTextOption[2].text = "C";
            listTextOption[3].text = "D";
        }
    }

    private void CleanTextInCanvas()
    {
        textQuestion.text = "";
        for(int i = 0; i < 4; i++)
        {
            listOptionButton[i].SetActive(false);
        }
        foreach(TMP_Text text in listTextOption){
            text.text = "";
        }
    }

    public void SkipTyping()
    {
        skipping = true;
    }

    public void SelectQuestion(int index)
    {
        if(currentQuestion.isCorrect[index])
        {
            listOptionButton[index].GetComponent<Image>().color = Color.HSVToRGB(120.0f/360.0f, 0.5f, 1.0f);

            //Causa dano no monstro
            monsterHealthCurrent -= playerDamage;
            if(monsterHealthCurrent < 0) monsterHealthCurrent = 0;
            imgHealthMonster.rectTransform.localScale = new Vector3(monsterHealthCurrent/monsterHealth, 1, 1);
            float hValue = 120.0f * monsterHealthCurrent/monsterHealth;
            imgHealthMonster.color = Color.HSVToRGB(hValue/360.0f, 0.5f, 1.0f);
        }
        else
        {
            listOptionButton[index].GetComponent<Image>().color = Color.HSVToRGB(0.0f/360.0f, 0.5f, 1.0f);
            for(int i = 0; i < 4; i++)
            {
                if(currentQuestion.isCorrect[i])
                {
                    listOptionButton[i].GetComponent<Image>().color = Color.HSVToRGB(120.0f/360.0f, 0.5f, 1.0f);
                }
            }

            //Causa dano no player
            playerHealthCurrent -= monsterDamage;
            if(playerHealthCurrent < 0) playerHealthCurrent = 0;
            imgHealthPlayer.rectTransform.localScale = new Vector3(playerHealthCurrent/playerHealth, 1, 1);
            float hValue = 120.0f * playerHealthCurrent/playerHealth;
            imgHealthPlayer.color = Color.HSVToRGB(hValue/360.0f, 0.5f, 1.0f);
        }

        btnNext.interactable = true;

        foreach(GameObject goBtn in listOptionButton)
        {
            goBtn.GetComponent<Button>().interactable = false;
        }
    }

    public void NextQuestion()
    {
        foreach(GameObject goBtn in listOptionButton)
        {
            goBtn.GetComponent<Image>().color = Color.white;
            goBtn.GetComponent<Button>().interactable = true;
        }

        LoadQuestion();
        btnNext.interactable = false;
    }

    private void DecreseLifeEffect()
    {
        Vector3 a = imgHealthPlayerEffect.rectTransform.localScale;
        Vector3 b = imgHealthPlayer.rectTransform.localScale;

        imgHealthPlayerEffect.rectTransform.localScale = Vector3.Lerp(a, b, Time.deltaTime * 2);

        a = imgHealthMonsterEffect.rectTransform.localScale;
        b = imgHealthMonster.rectTransform.localScale;

        imgHealthMonsterEffect.rectTransform.localScale = Vector3.Lerp(a, b, Time.deltaTime * 2);
        
    }
}
