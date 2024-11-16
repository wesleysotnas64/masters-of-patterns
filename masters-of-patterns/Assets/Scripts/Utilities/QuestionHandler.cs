using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
public class QuestionHandler : MonoBehaviour
{

    public List<int> calledQuestionsId;
    public int maxQuestionsCalled;
    public List<string> subjects;
    public string currentSubject;

    //Provisório
    private int minId; // Inclusivo
    private int maxId; // Exclusivo

    void Start()
    {
        calledQuestionsId = new List<int>();
    }

    private void IdentfytMaxMinIds()
    {
        switch(currentSubject)
        {
            case "Metodologias":
                minId = 1;
                maxId = 15;
                break;

            case "PrincipiosProjeto":
                minId = 16;
                maxId = 25;
                break;

            case "Arquitetura":
                minId = 26;
                maxId = 43;
                break;
            
            case "SOLID":
                minId = 44;
                maxId = 50;
                break;

            case "PadroesGoF":
                minId = 51;
                maxId = 55;
                break;

            default:
                break;
        }
    }

    public Question GetRandomQuestioin()
    {
        RandomSubject();

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
        GameObject q = Resources.Load<GameObject>($"QuizQuestions/Quest{currentSubject}/QuizQuestion_{randomId}");
        return q.GetComponent<Question>();
    }

    public void RandomSubject()
    {
        int qtdSubjects = subjects.Count;
        int random = Random.Range(0, qtdSubjects);
        currentSubject = subjects[random];

        IdentfytMaxMinIds();
    }
}
}

