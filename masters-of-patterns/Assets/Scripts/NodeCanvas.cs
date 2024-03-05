using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeCanvas : MonoBehaviour
{
    //Interface canvas
    public Text textNode;
    public List<Text> textOptions;
    public List<GameObject> buttonOptions;
    public GameObject buttonSkip;

    public Node node;
    public bool skipping;
    public float delay;

    void Start(){
        GameObject n = Resources.Load<GameObject>("History/Node_"+1.ToString());
        node = n.GetComponent<Node>();
        RenderNode();
    }

    void Update(){
        if (buttonSkip != null) buttonSkip.SetActive(!skipping);
    }

    private void RenderNode(){
        CleanTextInCanvas();
        StartCoroutine(TypewriteText());
    }

    private void RenderNodeOptionsInCanvas(){
        for(int i = 0; i < 4; i++)
        {
            buttonOptions[i].SetActive(true);
        }
        foreach(Text text in textOptions){
            text.text = "";
        }

        int qtdOptions = node.nextNode.Count;
        for(int i = 0; i < qtdOptions; i++)
        {
            textOptions[i].text = node.option[i];
        }

        if(qtdOptions < 4){
            for(int i = qtdOptions; i < 4; i++)
            {
                buttonOptions[i].SetActive(false);
            }
        }
    }

    IEnumerator TypewriteText(){
        skipping = false;

        int iterarion = 0;
        foreach (string text in node.text){
            
            for(int i = 0; i < text.Length; i++)
            {
                textNode.text += text[i];
                yield return new WaitForSeconds(skipping ? 0 : delay);
            }

            if (iterarion != node.text.Count - 1){
                textNode.text += "\n\n";
            }

            iterarion++;
        }

        skipping = true;

        RenderNodeOptionsInCanvas();

    }

    private void CleanTextInCanvas(){
        textNode.text = "";
        for(int i = 0; i < 4; i++)
        {
            buttonOptions[i].SetActive(false);
        }
        foreach(Text text in textOptions){
            text.text = "";
        }
    }

    //Chamadas externas. Botões da interface
    public void SkipTyping()
    {
        skipping = true;
    }

    public void LoadNode(int btnId){
        int idNode = node.nextNode[btnId];
        GameObject n = Resources.Load<GameObject>("History/Node_"+idNode.ToString());
        node = n.GetComponent<Node>();
        RenderNode();
    }
}
