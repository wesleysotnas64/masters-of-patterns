using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public void ItemAction(Item item){
        if (item != null)
        {
            switch (item.id)
            {
                case 1: //Poção de cura
                    HealthPotion(5);
                    break;

                default:
                    break;
            }
        }
    }

    private void HealthPotion(int increase)
    {
        GameObject.Find("Player").GetComponent<Player>().healthPoints += increase;
    }
}
