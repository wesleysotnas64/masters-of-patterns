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

    void Start()
    {
        calledQuestionsId = new List<int>();
    }

    public Question GetRandomQuestioin()
    {
        int randomId;
        while(true)
        {

            if(calledQuestionsId.Count >= maxQuestionsCalled) calledQuestionsId.Clear();

            randomId = Random.Range(0, maxQuestionsCalled + 1);

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

