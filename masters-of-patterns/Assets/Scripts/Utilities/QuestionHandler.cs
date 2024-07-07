using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
public class QuestionHandler : MonoBehaviour
{

    public List<int> calledQuestionsId;
    public int maxQuestionsCalled;
    public string subject;

    //Provisório
    private int minId; // Inclusivo
    private int maxId; // Exclusivo

    void Start()
    {
        calledQuestionsId = new List<int>();
        InitMaxMinIds();
    }

    private void InitMaxMinIds()
    {
        switch(subject)
        {
            case "Metodologias":
                minId = 1;
                maxId = 15;
                break;

            case "PrincipiosProjeto":
                minId = 16;
                maxId = 25;
                break;

            default:
                break;
        }
    }

    public Question GetRandomQuestioin()
    {
        int randomId;
        while(true)
        {

            if(calledQuestionsId.Count >= maxQuestionsCalled) calledQuestionsId.Clear();

            randomId = Random.Range(minId, maxId + 1);

            if(!calledQuestionsId.Contains(randomId))
            {
                calledQuestionsId.Add(randomId);
                break;
            }
        }
        GameObject q = Resources.Load<GameObject>($"QuizQuestions/Quest{subject}/QuizQuestion_{randomId}");
        return q.GetComponent<Question>();
    }
}
}

