using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public string itemName;
    public string description;
    public Sprite sprite;

    public ItemController itemController;

    void Start()
    {
        itemController = GetComponent<ItemController>();
    }

    public void Action()
    {
        itemController.ItemAction(GetComponent<Item>());
    }
}
