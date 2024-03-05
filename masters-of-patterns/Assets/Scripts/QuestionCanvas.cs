using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    //Interface canvas
    public Text textQuestion;
    public List<Text> textOptions;
    public List<GameObject> buttonOptions;
    public GameObject buttonSkip;

    public Question question;
    public bool skipping;
    public float delay;

    void Start(){
        // GameObject n = Resources.Load<GameObject>("Questions/Question_"+1.ToString());
        // question = n.GetComponent<Question>();
        LoadQuestion();
        RenderQuestion();
    }

    void Update(){
        if (buttonSkip != null) buttonSkip.SetActive(!skipping);
    }

    private void RenderQuestion(){
        CleanTextInCanvas();
        StartCoroutine(TypewriteText());
    }

    private void RenderQuestionOptionsInCanvas(){
        for(int i = 0; i < 4; i++)
        {
            buttonOptions[i].SetActive(true);
        }
        foreach(Text text in textOptions){
            text.text = "";
        }

        int qtdOptions = question.isCorrect.Count;
        if (qtdOptions == 2)
        {
            textOptions[0].text = "Verdadeiro";
            textOptions[1].text = "Falso";

            buttonOptions[2].SetActive(false);
            buttonOptions[3].SetActive(false);
        }
        else
        {
            textOptions[0].text = "A";
            textOptions[1].text = "B";
            textOptions[2].text = "C";
            textOptions[3].text = "D";
        }
    }

    IEnumerator TypewriteText(){
        skipping = false;

        int iteration = 0;
        foreach (string text in question.text){
            
            for(int i = 0; i < text.Length; i++)
            {
                textQuestion.text += text[i];
                yield return new WaitForSeconds(skipping ? 0 : delay);
            }

            if (iteration != question.text.Count - 1){
                textQuestion.text += "\n\n";
            }

            iteration++;
        }

        if(question.option.Count == 4)
        {
            textQuestion.text += "\n\n";
            List<string> auxOptions = new List<string>();
            auxOptions.Add("A - "+question.option[0]);
            auxOptions.Add("B - "+question.option[1]);
            auxOptions.Add("C - "+question.option[2]);
            auxOptions.Add("D - "+question.option[3]);
            

            iteration = 0;
            foreach (string text in auxOptions){
                for(int i = 0; i < text.Length; i++)
                {
                    textQuestion.text += text[i];
                    yield return new WaitForSeconds(skipping ? 0 : delay);
                }

                if (iteration != question.option.Count - 1){
                    textQuestion.text += "\n\n";
                }

                iteration++;
            }

        }


        skipping = true;

        RenderQuestionOptionsInCanvas();

    }

    private void CleanTextInCanvas(){
        textQuestion.text = "";
        for(int i = 0; i < 4; i++)
        {
            buttonOptions[i].SetActive(false);
        }
        foreach(Text text in textOptions){
            text.text = "";
        }
    }

    public void LoadQuestion(){
        int randomId = Random.Range(1, 6);
        GameObject q = Resources.Load<GameObject>("Questions/Question_"+randomId.ToString());
        question = q.GetComponent<Question>();

        // RenderQuestion();
    }

    //Chamadas externas. Botões da interface
    public void SkipTyping()
    {
        skipping = true;
    }

    public void SelectQuestion(int op){
        if(question.isCorrect[op])
        {
            LoadQuestion();
            RenderQuestion();
        }
    }
}
