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

    public Node node;
    public bool isTyping;
    public float delay;

    void Start(){
        GameObject n = Resources.Load<GameObject>("Node_"+1.ToString());
        node = n.GetComponent<Node>();
        RenderNodeInCanvas();
    }

    private void RenderNodeInCanvas(){
        CleanTextInCanvas();
        StartCoroutine(TypewriteText());

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
        isTyping = true;

        foreach (string text in node.text){
            
            for(int i = 0; i < text.Length; i++)
            {
                textNode.text += text[i];
                yield return new WaitForSeconds(delay);
            }

            // Verifica se este é o último texto antes de adicionar a quebra de linha
            if (text != node.text[node.text.Count - 1])
            {
                textNode.text += "\n\n";
            }

        }

        isTyping = false;

    }

    public void SkipTyping()
    {
        if (isTyping)
        {
            StopCoroutine(TypewriteText());
            CleanTextInCanvas();
            int lastIndex = node.text.Count - 1;
            for (int i = 0; i < node.text.Count; i++)
            {
                textNode.text += node.text[i];
                
                if (i != lastIndex)
                {
                    textNode.text += "\n\n";
                }
            }
            isTyping = false;
        }
    }

    private void CleanTextInCanvas(){
        textNode.text = "";
        for(int i = 0; i < 4; i++)
        {
            buttonOptions[i].SetActive(true);
        }
        foreach(Text text in textOptions){
            text.text = "";
        }
    }

    public void LoadNode(int btnId){
        int idNode = node.nextNode[btnId];
        GameObject n = Resources.Load<GameObject>("Node_"+idNode.ToString());
        node = n.GetComponent<Node>();
        RenderNodeInCanvas();
    }
}
