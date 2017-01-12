using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] itemImages = new Image[numItemSlots];
    public Item[] items = new Item[numItemSlots];

    public const int numItemSlots = 4; //public so that the custom inventory editor can access it


    public void AddItem(Item toAdd)
    {
        for(int i = 0; i < items.Length; ++i)
        {
            if(items[i] == null)
            {
                items[i] = toAdd;
                itemImages[i].sprite = toAdd.sprite;
                itemImages[i].enabled = true;
                break;
            }
        }
    }

    public void RemoveItem(Item toRemove)
    {
        for(int i = 0; i<items.Length; ++i)
        {
            if(items[i] == toRemove)
            {
                items[i] = null;
                itemImages[i] = null;
                itemImages[i].enabled = false;
                break;
            }
        }
    }
}
