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

    void Start(){
        GameObject n = Resources.Load<GameObject>("Node_"+1.ToString());
        node = n.GetComponent<Node>();
        RenderNodeInCanvas();
    }

    private void RenderNodeInCanvas(){
        CleanTextInCanvas();
        foreach (string text in node.text){
            textNode.text += text + "\n";
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
